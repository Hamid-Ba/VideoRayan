﻿using Framework.Application;
using Framework.Application.Enums;

namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class MeetingDto : DtoBase
    {
        public Guid UserId { get; set; }
        public Guid HostId { get; set; }
        public string? Title { get; set; }
        public string? HostName { get; set; }
        public bool IsLive { get; set; }
        public bool IsMute { get; set; }
        public bool IsRecord { get; set; }
        public bool CanTalk { get; set; }
        public int Duration { get; set; }
        public string? UserPinCode { get; set; }
        public string? MasterPinCode { get; set; }
        public bool IsInteractiveBoard { get; set; }
        public int AudienceCount { get; set; }
        public string? Description { get; set; }
        public MeetingType Type { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? PersianCreationDate { get; set; }
        public string? PersianStartDate { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
    }

    public class CreateMeetingDto
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public bool IsLive { get; set; }
        public bool IsMute { get; set; }
        public bool IsRecord { get; set; }
        public bool CanTalk { get; set; }
        public int Duration { get; set; }
        public bool IsInteractiveBoard { get; set; }
        public string? Description { get; set; }
        public MeetingType Type { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
    }

    public class EditMeetingDto : CreateMeetingDto
    {
        public Guid Id { get; set; }
    }

    public class FilterMeeting : BaseFilterParam
    {
        public Guid CustomerId { get; set; }
    }

    public class CheckMeetingPinCodeDto
    {
        public Guid Id { get; set; }
        public string? PinCode { get; set; }
    }

    public class GetAllMeetingDto : BaseFilter<MeetingDto, FilterMeeting> { }
}