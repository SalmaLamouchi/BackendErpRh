using AutoMapper;
using Core.Model;
using Service.Common.Mappings;
using System;

namespace Service.DTO
{
    public class PaieDto : IMapFrom<Paye>
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public DateTime MoisPaye { get; set; } // Changé de DateOnly à DateTime
        public decimal Montant { get; set; }
        public string Statut { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Paye, PaieDto>()
                .ForMember(dest => dest.MoisPaye,
                           opt => opt.MapFrom(src => src.MoisPaye.ToDateTime(TimeOnly.MinValue)))
                .ReverseMap()
                .ForMember(dest => dest.MoisPaye,
                           opt => opt.MapFrom(src => DateOnly.FromDateTime(src.MoisPaye)));
        }
    }
}