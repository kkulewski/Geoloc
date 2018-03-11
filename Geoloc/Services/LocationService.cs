using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Data.Repositories.Abstract;
using Geoloc.Models;
using Geoloc.Services.Abstract;

namespace Geoloc.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocationRepository _locationRepository;

        public LocationService(IUnitOfWork unitOfWork, ILocationRepository locationRepository)
        {
            _unitOfWork = unitOfWork;
            _locationRepository = locationRepository;
        }
        
        public void AddLocation(LocationModel model)
        {
            try
            {
                var location = Mapper.Map<Location>(model);
                _locationRepository.Add(location);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                // TODO: error handling
            }
        }

        public IEnumerable<LocationModel> GetLocationByUserId(Guid userId)
        {
            try
            {
                var locations = _locationRepository.GetLocationsByUser(userId).ToList();
                var result = Mapper.Map<IEnumerable<LocationModel>>(locations);
                return result;
            }
            catch (Exception)
            {
                return new List<LocationModel>();
            }
            
        }

        public IEnumerable<LocationModel> GetLastKnownLocations()
        {
            try
            {
                var locations = _locationRepository.GetAllLocations()
                    .GroupBy(e => e.AppUser.Id)
                    .Select(x => x.OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefault());
                var result = Mapper.Map<IEnumerable<LocationModel>>(locations);
                return result;
            }
            catch (Exception)
            {
                return new List<LocationModel>();
            }
        }
    }
}