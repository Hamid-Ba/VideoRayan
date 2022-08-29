using Framework.Application;
using Framework.Application.Sms;
using System.Reflection;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Domain.CustomerAgg.Contracts;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Application
{
    public class MeetingApplication : IMeetingApplication
    {
        private readonly ISmsService _smsService;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAudienceRepository _audienceRepository;

        public MeetingApplication(ISmsService smsService, IMeetingRepository meetingRepository, IAudienceRepository audienceRepository)
        {
            _smsService = smsService;
            _meetingRepository = meetingRepository;
            _audienceRepository = audienceRepository;
        }

        public async Task<(OperationResult, MeetingDto)> Create(CreateMeetingDto command)
        {
            OperationResult result = new();

            var hour = int.Parse(command.StartTime!.Split(':')[0]);
            var minute = int.Parse(command.StartTime.Split(':')[1]);
            var compliteDate = command.StartDate!.GetCompliteDate(hour, minute);

            if (_meetingRepository.Exists(m => m.StartDateTime == compliteDate))
                return (result.Failed(ApplicationMessage.DuplicatedMeetingTime), default)!;

            var userPinCode = Guid.NewGuid().ToString().Substring(0, 6);
            var masterPinCode = Guid.NewGuid().ToString().Substring(0, 6);

            var meeting = new Meeting(command.UserId, command.Title!, command.IsLive, command.IsMute, command.IsRecord,
                command.CanTalk, command.Duration, userPinCode, masterPinCode, command.IsInteractiveBoard,
                command.Description!, command.Type, compliteDate);

            await _meetingRepository.AddEntityAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(meeting.Id));
        }

        public async Task<(OperationResult, MeetingDto)> Delete(Guid customerId, Guid id)
        {
            OperationResult result = new();

            var meeting = await _meetingRepository.GetEntityByIdAsync(id);

            if (meeting is null) return (result.Failed(ApplicationMessage.NotExist), default)!;
            if (meeting.UserId != customerId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            var meetingDto = await GetBy(id);
            meeting.Delete();
            await _meetingRepository.SaveChangesAsync();

            return (result.Succeeded(), meetingDto);
        }

        public async Task<(OperationResult, MeetingDto)> Edit(EditMeetingDto command)
        {
            OperationResult result = new();

            var meeting = await _meetingRepository.GetEntityByIdAsync(command.Id);

            if (meeting is null) return (result.Failed(ApplicationMessage.NotExist), default)!;

            var hour = int.Parse(command.StartTime!.Split(':')[0]);
            var minute = int.Parse(command.StartTime.Split(':')[1]);
            var compliteDate = command.StartDate!.GetCompliteDate(hour, minute);

            if (_meetingRepository.Exists(m => m.StartDateTime == compliteDate && m.UserId == command.UserId && m.Id != command.Id))
                return (result.Failed(ApplicationMessage.DuplicatedMeetingTime), default)!;

            if (meeting.UserId != command.UserId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            meeting.Edit(command.Title!, command.IsLive, command.IsMute, command.IsRecord, command.CanTalk, command.Duration, command.IsInteractiveBoard, command.Description!,
                command.Type, compliteDate);
            await _meetingRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(meeting.Id));
        }

        public async Task<IEnumerable<MeetingDto>> GetAll(Guid customeriId) => await _meetingRepository.GetAll(customeriId);

        public async Task<IEnumerable<AudienceDto>> GetAllBy(Guid id) => await _audienceRepository.GetAllBy(meetingId: id);

        public async Task<GetAllMeetingDto> GetAllMeetingPaginated(FilterMeeting filter) => await _meetingRepository.GetAllMeetingPaginated(filter);

        public async Task<MeetingDto> GetBy(Guid id) => await _meetingRepository.GetBy(id);

        public async Task<EditMeetingDto> GetDetailForEditBy(Guid id) => await _meetingRepository.GetDetailForEditBy(id);

        public OperationResult IsAudienceBelongToMeeting(CheckMeetingPinCodeDto command)
        {
            OperationResult result = new();

            if (_meetingRepository.Exists(m => m.Id == command.Id && (m.MasterPinCode == command.PinCode || m.UserPinCode == command.PinCode)))
                return result.Succeeded();
            //else if (_meetingRepository.Exists(m => m.Id == command.Id && m.UserPinCode == command.PinCode)) return result.Succeeded();

            return result.Failed("کد جلسه اشتباه هست");
        }

        public async Task<OperationResult> SendConfirmMeetingSms(Guid id, string template)
        {
            OperationResult result = new();

            var meeting = await _audienceRepository.GetForSendingSms(id, isMeeting: true);
            if (meeting is null) return result.Failed(ApplicationMessage.UserNotExist);

            //ToDo : Send Confirm Meeting Code
            foreach (var mobile in meeting.AudienceMobile!)
                await _smsService.SendConfrimMeetingSms(mobile, template, new string[] { meeting.Title!, meeting.StartDate!, meeting.StartTime!, meeting.URLOrAddress!, meeting.PinCode! });

            await _smsService.SendConfrimMeetingSms(meeting.HostMobile!, template, new string[] { meeting.Title!, meeting.StartDate!, meeting.StartTime!, meeting.URLOrAddress!, meeting.MasterPinCode! });

            return result.Succeeded();
        }

        public async Task<OperationResult> SendDisConfirmMeetingSms(Guid id, string template)
        {
            OperationResult result = new();

            var meeting = await _audienceRepository.GetForSendingSms(id, isMeeting: true);
            if (meeting is null) return result.Failed(ApplicationMessage.UserNotExist);

            //ToDo : Send Confirm Meeting Code
            foreach (var mobile in meeting.AudienceMobile!)
                await _smsService.SendDisConfrimMeetingSms(mobile, template, new string[] { meeting.Title!, meeting.StartDate!, meeting.StartTime! });

            await _smsService.SendDisConfrimMeetingSms(meeting.HostMobile!, template, new string[] { meeting.Title!, meeting.StartDate!, meeting.StartTime! });

            return result.Succeeded();
        }

        public async Task<OperationResult> SetHost(Guid id, Guid hostId)
        {
            OperationResult result = new();

            var meeting = await _meetingRepository.GetEntityByIdAsync(id);
            if (meeting is null) return result.Failed(ApplicationMessage.NotExist);

            meeting.SetHost(hostId);
            await _meetingRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}