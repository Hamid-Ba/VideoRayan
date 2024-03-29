﻿using Framework.Domain;

namespace VideoRayan.Domain.MeetingAgg.Repositories
{
    public interface IAudienceMeetingRepository : IRepository<AudienceMeeting>
    {
        Task<IEnumerable<AudienceMeeting>> GetAllBy(Guid meetingId);
    }
}