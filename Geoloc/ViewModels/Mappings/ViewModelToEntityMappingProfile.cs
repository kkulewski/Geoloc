using AutoMapper;
using Geoloc.Data.Entities;

namespace Geoloc.ViewModels.Mappings
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