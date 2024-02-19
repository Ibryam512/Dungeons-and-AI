using AutoMapper;
using DungeonsAndArtificialIntelligenceAPI.BLL.Helpers;
using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;

namespace DungeonsAndArtificialIntelligenceAPI.BLL.Profiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<RegisterBindingModel, User>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Hasher.HashPassword(src.Password)));

            CreateMap<StoryBindingModel, Story>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Story, StoryViewModel>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));
        }
    }
}
