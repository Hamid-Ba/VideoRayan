using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Infrastructure.EfCore.Repositories.MeetingAgg
{
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        private readonly VideoRayanContext _context;

        public MeetingRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<MeetingDto>> GetAll(Guid customerId) => await _context.Meetings.Where(m => m.UserId == customerId)
            .Select(m => new MeetingDto
            {
                Id = m.Id,
                CanTalk = m.CanTalk,
                UserId = m.UserId,
                IsInteractiveBoard = m.IsInteractiveBoard,
                IsLive = m.IsLive,
                IsMute = m.IsMute,
                IsRecord = m.IsRecord,
                PersianCreationDate = m.CreationDate.ToFarsi(),
                PersianStartDate = m.StartDateTime.ToFarsi(),
                Title = m.Title,
                Type = m.Type,
                AudienceCount = m.Audiences!.Count
            }).AsNoTracking().ToListAsync();


        public async Task<MeetingDto> GetBy(Guid id) => (await _context.Meetings.Select(m => new MeetingDto
        {
            Id = m.Id,
            CanTalk = m.CanTalk,
            UserId = m.UserId,
            IsInteractiveBoard = m.IsInteractiveBoard,
            IsLive = m.IsLive,
            IsMute = m.IsMute,
            IsRecord = m.IsRecord,
            PersianCreationDate = m.CreationDate.ToFarsi(),
            PersianStartDate = m.StartDateTime.ToFarsi(),
            Title = m.Title,
            Type = m.Type,
            StartTime = m.StartDateTime.GetTime(),
            StartDate = m.StartDateTime.ToFarsi()
        }).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id))!;

        public async Task<EditMeetingDto> GetDetailForEditBy(Guid id) => (await _context.Meetings.Select(m => new EditMeetingDto
        {
            Id = m.Id,
            CanTalk = m.CanTalk,
            UserId = m.UserId,
            IsInteractiveBoard = m.IsInteractiveBoard,
            IsLive = m.IsLive,
            IsMute = m.IsMute,
            IsRecord = m.IsRecord,
            Title = m.Title,
            Type = m.Type,
            StartTime = m.StartDateTime.GetTime(),
            StartDate = m.StartDateTime.ToFarsi()
        }).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id))!;

    }
}