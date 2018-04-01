using System;
using System.Collections.Generic;
using Geoloc.Models;

namespace Geoloc.Services.Abstract
{
    public interface IMeetingService
    {
        MeetingModel GetById(Guid id);
        bool AddMeeting(MeetingModel model);
        bool JoinMeetingAsUser(Guid userId, Guid meetingId);
        IEnumerable<MeetingModel> GetAllMeetings();
    }
}
