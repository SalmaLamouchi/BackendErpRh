using AutoMapper;
using Core.Model;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public partial class PersonnelDto : IMapFrom<Personnel>
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string Poste { get; set; }

        public decimal Salaire { get; set; }

        public DateTime DateEmbauche { get; set; }

        public virtual ICollection<CongeDto> Conges { get; set; } = new List<CongeDto>();

        public virtual ICollection<PaieDto> Payes { get; set; } = new List<PaieDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Personnel, PersonnelDto>()
         .ForMember(dest => dest.DateEmbauche,
                    opt => opt.MapFrom(src => src.DateEmbauche.ToDateTime(TimeOnly.MinValue)))
         .ReverseMap()
         .ForMember(dest => dest.DateEmbauche,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateEmbauche)));
        }
    }
}
