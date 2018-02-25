using AutoMapper;
using Geoloc.Data.Entities;
using Geoloc.Models;
using Geoloc.Models.WebModels;

namespace Geoloc.Infrastructure
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<LocationWebModel, LocationModel>()
                .ForMember(x => x.Timestamp, o => o.Ignore());

            CreateMap<LocationModel, Location>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.AppUser, o => o.Ignore())
                .ForMember(x => x.AppUserId, o => o.MapFrom(s => s.UserId))
                .ForMember(x => x.CreatedOn, o => o.MapFrom(s => s.Timestamp));

            CreateMap<LocationModel, LocationWebModel>();

            CreateMap<Location, LocationModel>()
                .ForMember(x => x.UserId, o => o.MapFrom(s => s.AppUserId))
                .ForMember(x => x.Timestamp, o => o.MapFrom(s => s.CreatedOn))
                .ForMember(x => x.Username, o => o.MapFrom(s => s.AppUser.UserName));
            
            CreateMap<RegisterWebModel, AppUser>()
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.Email));
        }
    }
}