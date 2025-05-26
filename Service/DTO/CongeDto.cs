using AutoMapper;
using Core.Model;
using Core.Model;
using Service.Common.Mappings;

public partial class CongeDto : IMapFrom<Conge>
{
    public int Id { get; set; }
    public int PersonnelId { get; set; }
    public DateOnly DateDebut { get; set; } 
    public DateOnly DateFin { get; set; }   
    public string TypeConge { get; set; }
    public string Statut { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Conge, CongeDto>()
            .ReverseMap();
    }
}

//public void Mapping(Profile profile)
//        {
//            profile.CreateMap<Conge, CongeDto>()
//                .ForMember(dest => dest.DateDebut,
//                    opt => opt.MapFrom(src => src.DateDebut.ToDateTime(TimeOnly.MinValue)))
//                 .ForMember(dest => dest.DateFin,
//                    opt => opt.MapFrom(src => src.DateFin.ToDateTime(TimeOnly.MinValue)))
//                .ReverseMap();
//        }
//    }
//}