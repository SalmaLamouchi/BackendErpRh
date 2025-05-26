using Core.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using Service.IService;
using Service.Service;

namespace Personnel_MAJ.Controllers
{
    [Route("PersonnelsMaj")]
    [ApiController]
    [EnableCors("CORSPolicy")]
    public class PersonnelMajController : ControllerBase
    {
        private readonly IPersonnelService _PersonnelService;
        private readonly Serilog.ILogger _logger;

        public PersonnelMajController(IPersonnelService PersonnelService, Serilog.ILogger logger)
        {
            _PersonnelService = PersonnelService;
            _logger = logger;
        }



        // POST: api/AddPersonnel
        [Route("AddC")]
        [HttpPost]
        public async Task<IActionResult> AddPersonnel([FromBody] PersonnelDto personnelDto)
        {
            var result = await _PersonnelService.AddPersonnel(personnelDto);
            return Ok(result); // Retourne l'objet créé avec son ID
        }

        [Route("DeleteC")]
        [HttpDelete]
        public async Task<ActionResult> DeletePersonnel(int IdPersonnel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _PersonnelService.Delete(IdPersonnel).ConfigureAwait(false);

                var showmessage = "Personnel supprimé avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur Deletagence <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        // PUT: api/UpdatePersonnel/5
        //[HttpPut("{id}")]
        [Route("UpdC")]
        [HttpPut]
        public async Task<ActionResult> UpdatePersonnel(PersonnelDto Personnel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ret = await _PersonnelService.UpdatePersonnel(Personnel).ConfigureAwait(false);
                if (ret)
                {
                    var showmessage = "Modification effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);
                }
                else
                {
                    var showmessage = "Personnel Inexixtant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }

               
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur UpdatePersonnel <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// Suppression du Personnel.
        /// </summary>
        /// <param name="IdPersonnel">Identifiant du Personnel à supprimer</param>
        /// <returns>Un message de succès ou d'erreur</returns>
        [Route("DelClt/{IdPersonnel}")]  
        [HttpDelete]
        public async Task<ActionResult> delPersonnel(int IdPersonnel)
        {
            var dict = new Dictionary<string, string>();
            try
            {
                bool isDeleted = await _PersonnelService.DeletePersonnel(IdPersonnel).ConfigureAwait(false);

                if (isDeleted)
                {
                    dict.Add("Message", "Personnel supprimé avec succès");
                    return Ok(dict);
                }
                else
                {
                    dict.Add("Message", $"Personnel avec l'ID {IdPersonnel} introuvable.");
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur delPersonnel <==> " + ex.ToString());
                dict.Add("Message", "Erreur: " + ex.Message);
                return BadRequest(dict);
            }
        }



    }
}
