using System;
using Geoloc.Models;

namespace Geoloc.Services.Abstract
{
    public interface IMeetingService
    {
        MeetingModel GetById(Guid id);

        bool AddMeeting(MeetingModel model);
    }
}
