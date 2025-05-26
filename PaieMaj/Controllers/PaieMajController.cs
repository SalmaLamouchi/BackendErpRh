using Core.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using Service.IService;
using Service.Service;

namespace Paie_MAJ.Controllers
{
    [Route("PaiesMaj")]
    [ApiController]
    [EnableCors("CORSPolicy")]
    public class PaieMajController : ControllerBase
    {
        private readonly IPaieService _PaieService;
        private readonly Serilog.ILogger _logger;

        public PaieMajController(IPaieService PaieService, Serilog.ILogger logger)
        {
            _PaieService = PaieService;
            _logger = logger;
        }



        // POST: api/AddPaie
        [Route("AddPaie")]
        [HttpPost]
        public async Task<IActionResult> AddPaie([FromBody] PaieDto PaieDto)
        {
            var result = await _PaieService.AddPaye(PaieDto);
            return Ok(result); // Retourne l'objet créé avec son ID
        }

        [Route("DelPaie")]
        [HttpDelete]
        public async Task<ActionResult> DeletePaie(int IdPaie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _PaieService.Delete(IdPaie).ConfigureAwait(false);

                var showmessage = "Paie supprimé avec succès";
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

        // PUT: api/UpdatePaie/5
        //[HttpPut("{id}")]
        [Route("UpdPaie")]
        [HttpPut]
        public async Task<ActionResult> UpdatePaie(PaieDto Paie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ret = await _PaieService.UpdatePaye(Paie).ConfigureAwait(false);
                if (ret)
                {
                    var showmessage = "Modification effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);
                }
                else
                {
                    var showmessage = "Paie Inexixtant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }


            }
            catch (Exception ex)
            {
                _logger.Error("Erreur UpdatePaie <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// Suppression du Paie.
        /// </summary>
        /// <param name="IdPaie">Identifiant du Paie à supprimer</param>
        /// <returns>Un message de succès ou d'erreur</returns>
        [Route("DelPaie/{IdPaie}")]
        [HttpDelete]
        public async Task<ActionResult> delPaie(int IdPaie)
        {
            var dict = new Dictionary<string, string>();
            try
            {
                bool isDeleted = await _PaieService.DeletePaye(IdPaie).ConfigureAwait(false);

                if (isDeleted)
                {
                    dict.Add("Message", "Paie supprimé avec succès");
                    return Ok(dict);
                }
                else
                {
                    dict.Add("Message", $"Paie avec l'ID {IdPaie} introuvable.");
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur delPaie <==> " + ex.ToString());
                dict.Add("Message", "Erreur: " + ex.Message);
                return BadRequest(dict);
            }
        }



    }
}
