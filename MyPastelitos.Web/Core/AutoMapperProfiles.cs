using AutoMapper;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Account;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.DTOs.User;

namespace MyPastelitos.Web.Core
{
    public class AutoMapperProfiles : Profile 
    {
         public AutoMapperProfiles()




        {
            CreateMap<Section, CreateSectionDTO>().ReverseMap();

            CreateMap<Section, UpdateSectionDTO>().ReverseMap();

            CreateMap<Section, SectionDTO>().ForMember(dto => dto.Name, entity => entity.MapFrom(s => s.Name))
                                            .ReverseMap();


            CreateMap<Recipe, CreateRecipeDTO>().ReverseMap();

            CreateMap<Recipe, UpdateRecipeDTO>().ReverseMap();

            CreateMap<Recipe, RecipeDTO>().ReverseMap();

            CreateMap<User, AccountUserDTO>().ForMember(dto => dto.Phone, entity => entity.MapFrom(u => u.PhoneNumber))
                                             .ForMember(dto => dto.Id, entity => entity.MapFrom(e => Guid.Parse(e.Id)))
                                             .ForMember(dto => dto.RoleName, entity => entity.MapFrom(u => u.PrivateRecipeRole.Name));

            CreateMap<User, UserDTO>().ForMember(dto => dto.Phone, entity => entity.MapFrom(u => u.PhoneNumber))
                                             .ForMember(dto => dto.Id, entity => entity.MapFrom(e => Guid.Parse(e.Id)));

            CreateMap<User,CreateUserDTO>().ForMember(dto => dto.Phone, entity => entity.MapFrom(u => u.PhoneNumber))
                                             .ReverseMap();

            CreateMap<User,UpdateUserDTO>().ForMember(dto => dto.Phone, entity => entity.MapFrom(u => u.PhoneNumber))
                                             .ForMember(dto => dto.Id, entity => entity.MapFrom(e => Guid.Parse(e.Id)))
                                             .ReverseMap();


        }

        
    }
}
