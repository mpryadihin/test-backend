using AutoMapper;
using ThesesApi.Models;

namespace ThesesApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Маппинг из ThesisForm в Thesis
        CreateMap<ThesisForm, Thesis>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.Updated, opt => opt.Ignore())
            .ForMember(dest => dest.MainAuthor, opt => opt.MapFrom(src => src.MainAuthor))
            .ForMember(dest => dest.OtherAuthors, opt => opt.MapFrom(src => src.OtherAuthors.Select(author => new ThesisOtherAuthors
            {
                Author = new Person
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    MiddleName = author.MiddleName,
                    Workplace = author.Workplace
                }
            })));

        // Маппинг из Thesis в ThesisResource
        CreateMap<Thesis, ThesisResource>()
            .ForMember(dest => dest.MainAuthor, opt => opt.MapFrom(src => src.MainAuthor))
            .ForMember(dest => dest.OtherAuthors, opt => opt.MapFrom(src => src.OtherAuthors.Select(oa => oa.Author)));

        // Маппинг из Person в PersonResource и обратно
        CreateMap<Person, PersonResource>().ReverseMap();

        // Маппинг из Thesis в ThesisTableItemResource
        CreateMap<Thesis, ThesisTableItemResource>()
            .ForMember(dest => dest.MainAuthor, opt => opt.MapFrom(src => $"{src.MainAuthor.FirstName} {src.MainAuthor.LastName} {src.MainAuthor.MiddleName}"));
        
    }
}
