using Framework.Application;
using Framework.Application.Sms;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Domain.CustomerAgg.Contracts;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Application
{
    public class FaceToFaceApplication : IFaceToFaceApplication
    {
        private readonly ISmsService _smsService;
        private readonly IAudienceRepository _audienceRepository;
        private readonly IFaceToFaceRepository _faceToFaceRepository;

        public FaceToFaceApplication(ISmsService smsService, IAudienceRepository audienceRepository, IFaceToFaceRepository faceToFaceRepository)
        {
            _smsService = smsService;
            _audienceRepository = audienceRepository;
            _faceToFaceRepository = faceToFaceRepository;
        }

        public async Task<(OperationResult, FaceToFaceDto)> Create(CreateFaceToFaceDto command)
        {
            OperationResult result = new();

            var hour = int.Parse(command.StartTime!.Split(':')[0]);
            var minute = int.Parse(command.StartTime.Split(':')[1]);
            var compliteDate = command.StartDate!.GetCompliteDate(hour, minute);

            if (_faceToFaceRepository.Exists(m => m.StartDateTime == compliteDate))
                return (result.Failed(ApplicationMessage.DuplicatedMeetingTime), default)!;

            var faceToFace = new FaceToFace(command.UserId, command.Title!, command.Address!, command.Type, compliteDate);

            await _faceToFaceRepository.AddEntityAsync(faceToFace);
            await _faceToFaceRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(faceToFace.Id));
        }

        public async Task<(OperationResult, FaceToFaceDto)> Delete(Guid customerId, Guid id)
        {
            OperationResult result = new();

            var faceToFace = await _faceToFaceRepository.GetEntityByIdAsync(id);

            if (faceToFace is null) return (result.Failed(ApplicationMessage.NotExist), default)!;
            if (faceToFace.UserId != customerId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            var faceToFaceDto = await GetBy(id);
            faceToFace.Delete();
            await _faceToFaceRepository.SaveChangesAsync();

            return (result.Succeeded(), faceToFaceDto);
        }

        public async Task<(OperationResult, FaceToFaceDto)> Edit(EditFaceToFaceDto command)
        {
            OperationResult result = new();

            var faceToFace = await _faceToFaceRepository.GetEntityByIdAsync(command.Id);

            if (faceToFace is null) return (result.Failed(ApplicationMessage.NotExist), default)!;

            var hour = int.Parse(command.StartTime!.Split(':')[0]);
            var minute = int.Parse(command.StartTime.Split(':')[1]);
            var compliteDate = command.StartDate!.GetCompliteDate(hour, minute);

            if (_faceToFaceRepository.Exists(m => m.StartDateTime == compliteDate && m.UserId == command.UserId && m.Id != command.Id))
                return (result.Failed(ApplicationMessage.DuplicatedMeetingTime), default)!;

            if (faceToFace.UserId != command.UserId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            faceToFace.Edit(command.Title!, command.Address!, command.Type, compliteDate);
            await _faceToFaceRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(faceToFace.Id));
        }

        public async Task<IEnumerable<FaceToFaceDto>> GetAll(Guid customerId) => await _faceToFaceRepository.GetAll(customerId);

        public async Task<IEnumerable<AudienceDto>> GetAllBy(Guid id) => await _audienceRepository.GetAllByFaceToFace(id);

        public async Task<GetAllFaceToFaceDto> GetAllFaceToFacePaginated(FilterFaceToFace filter) => await _faceToFaceRepository.GetAllFaceToFacePaginated(filter);

        public async Task<FaceToFaceDto> GetBy(Guid id) => await _faceToFaceRepository.GetBy(id);

        public async Task<EditFaceToFaceDto> GetDetailForEditBy(Guid id) => await _faceToFaceRepository.GetDetailForEditBy(id);

        public async Task<OperationResult> SendConfirmMeetingSms(Guid id, string template)
        {
            OperationResult result = new();

            var meeting = await _audienceRepository.GetForSendingSms(id, isMeeting: false);
            if (meeting is null) return result.Failed(ApplicationMessage.UserNotExist);

            //ToDo : Send Confirm Meeting Code
            foreach (var mobile in meeting.AudienceMobile!)
                await _smsService.SendConfrimMeetingSms(mobile, template, new string[] { meeting.Title!, meeting.StartDate!, meeting.StartTime!, meeting.URLOrAddress! });

            return result.Succeeded();
        }

        public async Task<OperationResult> SendDisConfirmMeetingSms(Guid id, string template)
        {
            OperationResult result = new();

            var meeting = await _audienceRepository.GetForSendingSms(id, isMeeting: false);
            if (meeting is null) return result.Failed(ApplicationMessage.UserNotExist);

            //ToDo : Send Confirm Meeting Code
            foreach (var mobile in meeting.AudienceMobile!)
                await _smsService.SendDisConfrimMeetingSms(mobile, template, new string[] { meeting.Title!, meeting.StartDate!, meeting.StartTime! });

            return result.Succeeded();
        }

        public async Task<OperationResult> SetHost(Guid id, Guid hostId)
        {
            OperationResult result = new();

            var meeting = await _faceToFaceRepository.GetEntityByIdAsync(id);
            if (meeting is null) return result.Failed(ApplicationMessage.NotExist);

            meeting.SetHost(hostId);
            await _faceToFaceRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}