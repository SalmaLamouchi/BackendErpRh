using AutoMapper;
using Core.Model;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.IService;

namespace Service.Service
{
    public class CongeService : ServiceAsync<Conge, CongeDto>, ICongeService
    {
        private readonly IRepositoryAsync<Conge> _congeRepository;
        private readonly IServiceAsync<Conge, CongeDto> _congeService;
        private readonly IMapper _mapper;

        public CongeService(
            IRepositoryAsync<Conge> congeRepository,
            IServiceAsync<Conge, CongeDto> congeService,
            IMapper mapper)
            : base(congeRepository, mapper)
        {
            this._congeRepository = congeRepository;
            this._congeService = congeService;
            this._mapper = mapper;
        }

        public IQueryable<CongeDto> GetConges()
        {
            return (IQueryable<CongeDto>)_congeService.GetAll();
        }

        public async Task<bool> AddConge(CongeDto congeDto)
        {
            await _congeService.Add(congeDto);
            return true;
        }

        public async Task<CongeDto> GetConge(int id)
        {
            var conge = await _congeService.GetFirstOrDefault(
                predicate: i => i.Id == id,
                orderBy: u => u.OrderBy(j => j.Id),
                include: l => l.Include(k => k.Personnel),
                disableTracking: true
            );
            return conge;
        }

        public async Task<bool> UpdateConge(CongeDto congeDto)
        {
            var conge = await _congeService.GetById(congeDto.Id);
            if (conge != null)
            {
                conge.PersonnelId = congeDto.PersonnelId;
                conge.DateDebut = congeDto.DateDebut;
                conge.DateFin = congeDto.DateFin;
                conge.TypeConge = congeDto.TypeConge;
                conge.Statut = congeDto.Statut;

                await _congeService.Update(conge);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteConge(int id)
        {
            var conge = await _congeService.GetFirstOrDefault(
                predicate: u => u.Id == id,
                disableTracking: true
            );

            if (conge != null)
            {
                await _congeService.Delete(id);
                return true;
            }
            return false;
        }

        // Méthode supplémentaire spécifique aux congés
        public async Task<bool> ChangerStatutConge(int id, string nouveauStatut)
        {
            var conge = await _congeService.GetById(id);
            if (conge != null)
            {
                conge.Statut = nouveauStatut;
                await _congeService.Update(conge);
                return true;
            }
            return false;
        }
    }
}