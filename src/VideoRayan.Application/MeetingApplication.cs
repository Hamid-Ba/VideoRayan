using Framework.Application;
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
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAudienceRepository _audienceRepository;

        public MeetingApplication(IMeetingRepository meetingRepository, IAudienceRepository audienceRepository)
        {
            _meetingRepository = meetingRepository;
            _audienceRepository = audienceRepository;
        }

        public async Task<OperationResult> Create(CreateMeetingDto command)
        {
            OperationResult result = new();

            var hour = int.Parse(command.StartTime!.Split(':')[0]);
            var minute = int.Parse(command.StartTime.Split(':')[1]);
            var compliteDate = command.StartDate!.GetCompliteDate(hour, minute);

            if (_meetingRepository.Exists(m => m.StartDateTime == compliteDate))
                return result.Failed(ApplicationMessage.DuplicatedMeetingTime);

            var meeting = new Meeting(command.UserId, command.Title!,command.IsLive,command.IsMute,command.IsRecord,
                command.CanTalk,command.IsInteractiveBoard,command.Type, compliteDate);

            await _meetingRepository.AddEntityAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(Guid customerId,Guid id)
        {
            OperationResult result = new();

            var meeting = await _meetingRepository.GetEntityByIdAsync(id);
            
            if (meeting is null) return result.Failed(ApplicationMessage.NotExist);
            if (meeting.UserId != customerId) return result.Failed(ApplicationMessage.DoNotAccessToOtherAccount);

            meeting.Delete();
            await _meetingRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditMeetingDto command)
        {
            OperationResult result = new();

            var meeting = await _meetingRepository.GetEntityByIdAsync(command.Id);

            if (meeting is null) return result.Failed(ApplicationMessage.NotExist);

            var hour = int.Parse(command.StartTime!.Split(':')[0]);
            var minute = int.Parse(command.StartTime.Split(':')[1]);
            var compliteDate = command.StartDate!.GetCompliteDate(hour, minute);

            if (_meetingRepository.Exists(m => m.StartDateTime == compliteDate && m.UserId == command.UserId && m.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedMeetingTime);

            if (meeting.UserId != command.UserId) return result.Failed(ApplicationMessage.DoNotAccessToOtherAccount);

            meeting.Edit(command.Title!,command.IsLive,command.IsMute,command.IsRecord,command.CanTalk,command.IsInteractiveBoard,
                command.Type, compliteDate);
            await _meetingRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<MeetingDto>> GetAll(Guid customeriId) => await _meetingRepository.GetAll(customeriId);

        public async Task<IEnumerable<AudienceDto>> GetAllBy(Guid id) => await _audienceRepository.GetAllBy(meetingId : id);

        public async Task<MeetingDto> GetBy(Guid id) => await _meetingRepository.GetBy(id);

        public async Task<EditMeetingDto> GetDetailForEditBy(Guid id) => await _meetingRepository.GetDetailForEditBy(id);
    }
}