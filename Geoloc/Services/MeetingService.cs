using System;
using System.Collections.Generic;
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
            try
            {
                var relation = _meetingRepository.Get(id);
                var result = Mapper.Map<MeetingModel>(relation);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddMeeting(MeetingModel model)
        {
            try
            {
                var meeting = Mapper.Map<Meeting>(model);
                meeting.Id = Guid.NewGuid();
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

        public IEnumerable<MeetingModel> GetAllMeetings()
        {
            var meetings = _meetingRepository.GetAll();
            var result = Mapper.Map<IEnumerable<MeetingModel>>(meetings);
            return result;
        }
    }
}
