using Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Domain;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Application
{
    public class AudienceFaceToFaceApplication : IAudienceFaceToFaceApplication
    {
        private readonly IAudienceFaceToFaceRepository _audienceFaceToFaceRepository;

        public AudienceFaceToFaceApplication(IAudienceFaceToFaceRepository audienceFaceToFaceRepository) => _audienceFaceToFaceRepository = audienceFaceToFaceRepository;

        public async Task<OperationResult> AddAudiencesToMeeting(AudienceFaceToFaceDto command)
        {
            OperationResult result = new();

            //Remove Old Ones
            var oldAudienceMeetings = await _audienceFaceToFaceRepository.GetAllBy(command.FaceToFaceId);
            if (oldAudienceMeetings != null) _audienceFaceToFaceRepository.DeleteRangeOfEntities(oldAudienceMeetings);

            //Add New Ones
            var audienceMeetings = new List<AudienceFaceToFace>();
            command.AudiencesId!.ForEach(a => audienceMeetings.Add(new AudienceFaceToFace(command.FaceToFaceId, a)));

            await _audienceFaceToFaceRepository.AddRangeOfEntitiesAsync(audienceMeetings);
            await _audienceFaceToFaceRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}