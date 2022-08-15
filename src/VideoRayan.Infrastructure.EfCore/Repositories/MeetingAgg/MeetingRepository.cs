using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;
using Framework.Application.Enums;

namespace VideoRayan.Infrastructure.EfCore.Repositories.MeetingAgg
{
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        private readonly VideoRayanContext _context;

        public MeetingRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<MeetingDto>> GetAll(Guid customerId)
        {
            var result = await _context.Meetings.Where(m => m.UserId == customerId)
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
                 StartDate = m.StartDateTime.ToFarsi(),
                 StartDateTime = m.StartDateTime,
                 Title = m.Title,
                 Type = m.Type,
                 AudienceCount = m.Audiences!.Count,
                 StartTime = m.StartDateTime.GetTimeRightFormat(),
             }).AsNoTracking().ToListAsync();

            result.ForEach(m => m.PersianStartDate = $"{m.StartDate} - {m.StartTime}");
            result.ForEach(m => m.Status = (m.StartDateTime > DateTime.Now) ? MeetingStatus.HasNotArrived : (m.StartDateTime.AddDays(1) <= DateTime.Now) ? MeetingStatus.Done : MeetingStatus.HasArrived);

            return result;
        }

        public async Task<MeetingDto> GetBy(Guid id)
        {
            var result = await _context.Meetings.Select(m => new MeetingDto
            {
                Id = m.Id,
                CanTalk = m.CanTalk,
                UserId = m.UserId,
                IsInteractiveBoard = m.IsInteractiveBoard,
                IsLive = m.IsLive,
                IsMute = m.IsMute,
                IsRecord = m.IsRecord,
                PersianCreationDate = m.CreationDate.ToFarsi(),
                StartDateTime = m.StartDateTime,
                PersianStartDate = m.StartDateTime.ToFarsi() + m.StartDateTime.GetTime(),
                Title = m.Title,
                Type = m.Type,
                StartTime = m.StartDateTime.GetTimeRightFormat(),
                StartDate = m.StartDateTime.ToFarsi()
            }).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id)!;

            result!.Status = (result.StartDateTime > DateTime.Now) ? MeetingStatus.HasNotArrived : (result.StartDateTime.AddDays(1) <= DateTime.Now) ? MeetingStatus.Done : MeetingStatus.HasArrived;

            return result;
        }

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