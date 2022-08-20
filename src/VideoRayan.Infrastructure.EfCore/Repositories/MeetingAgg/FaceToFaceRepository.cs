using Framework.Application;
using Framework.Application.Enums;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Infrastructure.EfCore.Repositories.MeetingAgg
{
    public class FaceToFaceRepository : Repository<FaceToFace>, IFaceToFaceRepository
    {
        private readonly VideoRayanContext _context;

        public FaceToFaceRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<FaceToFaceDto>> GetAll(Guid customerId)
        {
            var result = await _context.FaceToFaces.Where(m => m.UserId == customerId)
             .Select(m => new FaceToFaceDto
             {
                 Id = m.Id,
                 UserId = m.UserId,
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

        public async Task<GetAllFaceToFaceDto> GetAllFaceToFacePaginated(FilterFaceToFace filter)
        {
            var data = await _context.FaceToFaces.Where(m => m.UserId == filter.CustomerId)
             .Select(m => new FaceToFaceDto
             {
                 Id = m.Id,
                 UserId = m.UserId,
                 PersianCreationDate = m.CreationDate.ToFarsi(),
                 StartDate = m.StartDateTime.ToFarsi(),
                 StartDateTime = m.StartDateTime,
                 Title = m.Title,
                 Type = m.Type,
                 AudienceCount = m.Audiences!.Count,
                 StartTime = m.StartDateTime.GetTimeRightFormat(),
             }).AsNoTracking().ToListAsync();

            data.ForEach(m => m.PersianStartDate = $"{m.StartDate} - {m.StartTime}");
            data.ForEach(m => m.Status = (m.StartDateTime > DateTime.Now) ? MeetingStatus.HasNotArrived : (m.StartDateTime.AddDays(1) <= DateTime.Now) ? MeetingStatus.Done : MeetingStatus.HasArrived);

            var result = new GetAllFaceToFaceDto()
            {
                FilterParams = filter,
                Data = data.Skip((filter.PageId - 1) * filter.Take).Take(filter.Take).ToList()
            };
            result.GeneratePaging(data.AsQueryable(),filter.Take,filter.PageId);

            return result;
        }

        public async Task<FaceToFaceDto> GetBy(Guid id)
        {
            var result = await _context.FaceToFaces.Select(m => new FaceToFaceDto
            {
                Id = m.Id,
                UserId = m.UserId,
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

        public async Task<EditFaceToFaceDto> GetDetailForEditBy(Guid id) => (await _context.FaceToFaces.Select(m => new EditFaceToFaceDto
        {
            Id = m.Id,
            UserId = m.UserId,
            Title = m.Title,
            Type = m.Type,
            StartTime = m.StartDateTime.GetTime(),
            StartDate = m.StartDateTime.ToFarsi()
        }).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id))!;

    }
}