using Core.Model;
using Service.DTO;

namespace Service.IService
{
    public  interface IPersonnelService : IServiceAsync<Personnel, PersonnelDto>
    {
        IQueryable<PersonnelDto> GetPersonnels();
        Task<PersonnelDto> GetPersonnel(int idPersonnel);

        Task<List<PersonnelDto>> GetAllPersonnel();
        /// Operation de MAJ        
        Task<bool> AddPersonnel(PersonnelDto Personnel);
        Task<bool> UpdatePersonnel(PersonnelDto Personnel);
        Task<bool> DeletePersonnel(int PersonnelId);

         

    }
}
