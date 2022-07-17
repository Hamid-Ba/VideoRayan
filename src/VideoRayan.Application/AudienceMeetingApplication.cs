using System;
using Framework.Application;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Domain;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Application
{
	public class AudienceMeetingApplication : IAudienceMeetingApplication
	{
        private readonly IAudienceMeetingRepository _audienceMeetingRepository;

        public AudienceMeetingApplication(IAudienceMeetingRepository audienceMeetingRepository)
        {
            _audienceMeetingRepository = audienceMeetingRepository;
        }

        public async Task<OperationResult> AddAudiencesToMeeting(AudienceMeetingDto command)
        {
            OperationResult result = new();

            //Remove Old Ones
            var oldAudienceMeetings = await _audienceMeetingRepository.GetAllBy(command.MeetingId);
            if (oldAudienceMeetings != null)  _audienceMeetingRepository.DeleteRangeOfEntities(oldAudienceMeetings);


            //Add New Ones
            var audienceMeetings = new List<AudienceMeeting>();
            command.AudiencesId!.ForEach(a => audienceMeetings.Add(new AudienceMeeting(command.MeetingId, a)));

            await _audienceMeetingRepository.AddRangeOfEntitiesAsync(audienceMeetings);
            await _audienceMeetingRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}