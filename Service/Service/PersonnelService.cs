using AutoMapper;
using Core.Model;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.IService;

namespace Service.Service
{
    public class PersonnelService : ServiceAsync<Personnel, PersonnelDto>, IPersonnelService
    {
        private readonly IRepositoryAsync<Personnel> _personnelRepository;
        private readonly IServiceAsync<Personnel, PersonnelDto> _personnelService;
        private readonly IMapper _mapper;

        public PersonnelService(
            IRepositoryAsync<Personnel> personnelRepository,
            IServiceAsync<Personnel, PersonnelDto> personnelService,
            IMapper mapper)
            : base(personnelRepository, mapper)
        {
            this._personnelRepository = personnelRepository;
            this._personnelService = personnelService;
            this._mapper = mapper;
        }

        public IQueryable<PersonnelDto> GetPersonnels()
        {
            return _personnelService.GetAll();
        }
        public async Task<List<PersonnelDto>> GetAllPersonnel()
        {
            var personnelEntity = await _personnelService.GetMuliple(
                orderBy: u => u.OrderBy(j => j.Id),
                include: l => l
                    .Include(k => k.Conges)
                    .Include(k => k.Payes),

                disableTracking: true
            );

            return personnelEntity.ToList();
        }
        //public async Task<List<PersonnelDto>> GetAllPersonnel()
        //{
        //    var personnelEntity = await _personnelService.GetMuliple(
        //        orderBy: u => u.OrderBy(j => j.Id),
        //        include: l => l
        //            .Include(k => k.Conges)
        //            .Include(k => k.Payes),
        //        disableTracking: true
        //    );

        //    // Projection manuelle vers DTO (en ne sélectionnant que les propriétés voulues)
        //    var result = personnelEntity.Select(p => new PersonnelDto
        //    {
        //        Id = p.Id,
        //        Nom = p.Nom,
        //        Prenom = p.Prenom,

        //    }).ToList();

        //    return result;
        //}

        public async Task<bool> AddPersonnel(PersonnelDto personnelDto)
        {
            await _personnelService.Add(personnelDto);
            return true;
        }

        public async Task<PersonnelDto> GetPersonnel(int id)
        {
            var personnel = await _personnelService.GetFirstOrDefault(
                predicate: i => i.Id == id,
                orderBy: u => u.OrderBy(j => j.Id),
                include: l => l.Include(k => k.Conges)
                              .Include(k => k.Payes),
                disableTracking: true
            );
            return personnel;
        }

        public async Task<bool> UpdatePersonnel(PersonnelDto personnelDto)
        {
            var personnel = await _personnelService.GetById(personnelDto.Id);
            if (personnel != null)
            {
                personnel.Nom = personnelDto.Nom;
                personnel.Prenom = personnelDto.Prenom;
                personnel.Email = personnelDto.Email;
                personnel.Poste = personnelDto.Poste;
                personnel.Salaire = personnelDto.Salaire;
                personnel.DateEmbauche = personnelDto.DateEmbauche;

                await _personnelService.Update(personnel);
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePersonnel(int id)
        {
            var personnel = await _personnelService.GetFirstOrDefault(
                predicate: u => u.Id == id,
                disableTracking: true
            );

            if (personnel != null)
            {
                await _personnelService.Delete(id);
                return true;
            }
            return false;
        }




    }
}