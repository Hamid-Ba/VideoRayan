using Framework.Application;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Application
{
    public class MeetingApplication : IMeetingApplication
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingApplication(IMeetingRepository meetingRepository) => _meetingRepository = meetingRepository;

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

        public async Task<OperationResult> Delete(Guid id)
        {
            OperationResult result = new();

            var meeting = await _meetingRepository.GetEntityByIdAsync(id);
            if (meeting is null) return result.Failed(ApplicationMessage.NotExist);

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

            meeting.Edit(command.Title!,command.IsLive,command.IsMute,command.IsRecord,command.CanTalk,command.IsInteractiveBoard,
                command.Type, compliteDate);
            await _meetingRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<MeetingDto>> GetAll(Guid customeriId) => await _meetingRepository.GetAll(customeriId);

        public async Task<MeetingDto> GetBy(Guid id) => await _meetingRepository.GetBy(id);

        public async Task<EditMeetingDto> GetDetailForEditBy(Guid id) => await _meetingRepository.GetDetailForEditBy(id);
    }
}