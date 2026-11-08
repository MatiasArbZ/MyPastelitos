using AutoMapper;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;

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
        }
    }
}
