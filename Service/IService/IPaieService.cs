using Core.Model;
using Service.DTO;

namespace Service.IService
{
    public  interface IPaieService : IServiceAsync<Paye, PaieDto>
    {
        IQueryable<PaieDto> GetPayes();
        Task<PaieDto> GetPaye(int idPaye);

        
		

        /// Operation de MAJ        
        Task<bool> AddPaye(PaieDto Paye);
        Task<bool> UpdatePaye(PaieDto Paye);
        Task<bool> DeletePaye(int PayeId);

         

    }
}
