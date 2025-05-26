using Core.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using Service.IService;
using Service.Service;

namespace Conge_MAJ.Controllers
{
    [Route("CongesMaj")]
    [ApiController]
    [EnableCors("CORSPolicy")]
    public class CongeMajController : ControllerBase
    {
        private readonly ICongeService _CongeService;
        private readonly Serilog.ILogger _logger;

        public CongeMajController(ICongeService CongeService, Serilog.ILogger logger)
        {
            _CongeService = CongeService;
            _logger = logger;
        }



        // POST: api/AddConge
        [Route("AddConge")]
        [HttpPost]
        public async Task<IActionResult> AddConge([FromBody] CongeDto CongeDto)
        {
            var result = await _CongeService.AddConge(CongeDto);
            return Ok(result); // Retourne l'objet créé avec son ID
        }

        [Route("DeleteConge")]
        [HttpDelete]
        public async Task<ActionResult> DeleteConge(int IdConge)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _CongeService.Delete(IdConge).ConfigureAwait(false);

                var showmessage = "Conge supprimé avec succès";
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

        // PUT: api/UpdateConge/5
        //[HttpPut("{id}")]
        [Route("UpdConge")]
        [HttpPut]
        public async Task<ActionResult> UpdateConge(CongeDto Conge)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ret = await _CongeService.UpdateConge(Conge).ConfigureAwait(false);
                if (ret)
                {
                    var showmessage = "Modification effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);
                }
                else
                {
                    var showmessage = "Conge Inexixtant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }


            }
            catch (Exception ex)
            {
                _logger.Error("Erreur UpdateConge <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// Suppression du Conge.
        /// </summary>
        /// <param name="IdConge">Identifiant du Conge à supprimer</param>
        /// <returns>Un message de succès ou d'erreur</returns>
        [Route("DelConge/{IdConge}")]
        [HttpDelete]
        public async Task<ActionResult> delConge(int IdConge)
        {
            var dict = new Dictionary<string, string>();
            try
            {
                bool isDeleted = await _CongeService.DeleteConge(IdConge).ConfigureAwait(false);

                if (isDeleted)
                {
                    dict.Add("Message", "Conge supprimé avec succès");
                    return Ok(dict);
                }
                else
                {
                    dict.Add("Message", $"Conge avec l'ID {IdConge} introuvable.");
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur delConge <==> " + ex.ToString());
                dict.Add("Message", "Erreur: " + ex.Message);
                return BadRequest(dict);
            }
        }



    }
}
