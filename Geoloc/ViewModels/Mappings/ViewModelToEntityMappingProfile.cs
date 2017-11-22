using AutoMapper;
using Geoloc.ViewModels;
using Geoloc.Models.Entities;

namespace Geoloc.Models.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}