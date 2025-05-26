using Core.Model;
using Service.DTO;

namespace Service.IService
{
    public  interface ICongeService : IServiceAsync<Conge, CongeDto>
    {
        IQueryable<CongeDto> GetConges();
        Task<CongeDto> GetConge(int idConge);

       


        /// Operation de MAJ        
        Task<bool> AddConge(CongeDto Conge);
        Task<bool> UpdateConge(CongeDto Conge);
        Task<bool> DeleteConge(int CongeId);

         

    }
}
