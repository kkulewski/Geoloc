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
            #region REGISTER

            CreateMap<RegisterWebModel, AppUser>()
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.Email))
                .ForAllOtherMembers(o => o.Ignore());

            #endregion

            #region LOCATION

            CreateMap<LocationWebModel, LocationModel>()
                .ForMember(x => x.Timestamp, o => o.Ignore());

            CreateMap<LocationModel, Location>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.AppUser, o => o.Ignore())
                .ForMember(x => x.AppUserId, o => o.MapFrom(s => s.User.Id))
                .ForMember(x => x.CreatedOn, o => o.MapFrom(s => s.Timestamp));

            CreateMap<LocationModel, LocationWebModel>();

            CreateMap<Location, LocationModel>()
                .ForMember(x => x.User, o => o.MapFrom(s => s.AppUser))
                .ForMember(x => x.Timestamp, o => o.MapFrom(s => s.CreatedOn));

            #endregion

            #region USER_RELATION

            CreateMap<UserRelationWebModel, UserRelationModel>();

            CreateMap<UserRelation, UserRelationModel>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.InvitingUserId, o => o.MapFrom(s => s.InvitingUser.Id))
                .ForMember(x => x.InvitedUserId, o => o.MapFrom(s => s.InvitedUser.Id))
                .ForMember(x => x.UserRelationStatus, o => o.MapFrom(s => s.UserRelationStatus))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<UserRelationModel, UserRelationWebModel>();

            #endregion
        }
    }
}