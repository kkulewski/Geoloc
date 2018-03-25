using System;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models;
using Geoloc.Services.Abstract;

namespace Geoloc.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IUnitOfWork unitOfWork, IMeetingRepository meetingRepository)
        {
            _unitOfWork = unitOfWork;
            _meetingRepository = meetingRepository;
        }

        public MeetingModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool AddMeeting(MeetingModel model)
        {
            try
            {
                var meeting = Mapper.Map<Meeting>(model);
                _meetingRepository.Add(meeting);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                // TODO: error handling
                return false;
            }
        }
    }
}
