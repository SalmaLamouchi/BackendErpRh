using AutoMapper;
using Core.Model;
using DAL.IRepository;
using Service.DTO;
using Service.IService;
using Service.Service;

public class PaieService : ServiceAsync<Paye, PaieDto>, IPaieService
{
    private readonly IRepositoryAsync<Paye> _payeRepository;
    private readonly IServiceAsync<Paye, PaieDto> _payeService;
    private readonly IMapper _mapper;

    public PaieService(
        IRepositoryAsync<Paye> payeRepository,
        IServiceAsync<Paye, PaieDto> payeService,
        IMapper mapper)
        : base(payeRepository, mapper)
    {
        _payeRepository = payeRepository;
        _payeService = payeService;
        _mapper = mapper;
    }

    public IQueryable<PaieDto> GetPayes()
    {
        return _payeService.GetAll();
    }

    public async Task<PaieDto> GetPaye(int idPaye)
    {
        return await _payeService.GetFirstOrDefault(
            predicate: i => i.Id == idPaye,
            orderBy: u => u.OrderBy(j => j.Id),
           
            disableTracking: true
        );
    }

    public async Task<bool> AddPaye(PaieDto paye)
    {
        await _payeService.Add(paye);
        return true;
    }

    public async Task<bool> UpdatePaye(PaieDto payeDto)
    {
        var paye = await _payeService.GetById(payeDto.Id);
        if (paye != null)
        {
            paye.Statut = payeDto.Statut;
            paye.PersonnelId = payeDto.PersonnelId;
            paye.Montant = payeDto.Montant;
            paye.MoisPaye = payeDto.MoisPaye;

            await _payeService.Update(paye);
            return true;
        }
        return false;
    }

    public async Task<bool> DeletePaye(int id)
    {
        var paye = await _payeService.GetById(id);
        if (paye != null)
        {
            await _payeService.Delete(id);
            return true;
        }
        return false;
    }

    public async Task<bool> ChangerStatutpaye(int id, string nouveauStatut)
    {
        var paye = await _payeService.GetById(id);
        if (paye != null)
        {
            paye.Statut = nouveauStatut;
            await _payeService.Update(paye);
            return true;
        }
        return false;
    }
}
