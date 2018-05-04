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
            #region User

            CreateMap<RegisterWebModel, AppUser>()
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.Email))
                .ForAllOtherMembers(o => o.Ignore());

            #endregion

            #region UserRelation

            CreateMap<UserRelation, UserRelationModel>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.InvitingUser, o => o.MapFrom(s => s.InvitingUser))
                .ForMember(x => x.InvitedUser, o => o.MapFrom(s => s.InvitedUser))
                .ForMember(x => x.UserRelationStatus, o => o.MapFrom(s => s.UserRelationStatus))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<UserRelationModel, UserRelationWebModel>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.InvitingUserId, o => o.MapFrom(s => s.InvitingUser.Id))
                .ForMember(x => x.InvitedUserId, o => o.MapFrom(s => s.InvitedUser.Id))
                .ForMember(x => x.UserRelationStatus, o => o.MapFrom(s => s.UserRelationStatus))
                .ForMember(x => x.InvitingUserName, o => o.MapFrom(s => s.InvitingUser.UserName))
                .ForMember(x => x.InvitedUserName, o => o.MapFrom(s => s.InvitedUser.UserName));

            #endregion

            #region Meeting

            CreateMap<MeetingModel, Meeting>()
                .ForMember(x => x.MeetingHostId, o => o.MapFrom(s => s.HostId))
                .ForMember(x => x.AppUsersInMeeting, o => o.MapFrom(s => s.Participants));


            CreateMap<Meeting, MeetingModel>()
                .ForMember(x => x.Participants, o => o.MapFrom(s => s.AppUsersInMeeting))
                .ForMember(x => x.HostId, o => o.MapFrom(s => s.MeetingHostId));

            CreateMap<MeetingModel, MeetingWebModel>()
                .ForMember(x => x.Time, o => o.MapFrom(s => s.StartTime.TimeOfDay))
                .ForMember(x => x.Date, o => o.MapFrom(s => s.StartTime.Date));

            CreateMap<MeetingWebModel, MeetingModel>()
                .ForMember(x => x.StartTime, o => o.MapFrom(s => s.Date.Date.Add(s.Time.TimeOfDay)));

            #endregion

            #region UserModel

            CreateMap<UserModel, AppUser>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<AppUser, UserModel>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForAllOtherMembers(o => o.Ignore());

            CreateMap<UserModel, AppUserInMeeting>()
                .ForMember(x => x.AppUser, o => o.MapFrom(s => s))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<AppUserInMeeting, UserModel>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.AppUser.Id))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.AppUser.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.AppUser.LastName))
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.AppUser.UserName));

            #endregion

            #region UserWebModel

            CreateMap<UserWebModel, UserModel>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<UserModel, UserWebModel>()
                .ForMember(x => x.Email, o => o.MapFrom(s => s.UserName));

            #endregion

            #region AppUserInMeeting

            CreateMap<AppUserInMeeting, AppUser>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.AppUser.Id))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.AppUser.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.AppUser.LastName))
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.AppUser.Email))
                .ForAllOtherMembers(x => x.Ignore());

            #endregion
        }
    }
}
