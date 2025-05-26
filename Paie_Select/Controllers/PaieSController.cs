using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using Service.IService;

namespace Paie_Select.Controllers
{
    [Route("PaieS")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class PaieSController : ControllerBase
    {
        private readonly IPaieService _PaieService;
        //private readonly IServiceAsync<Paie, PaieDto> _service;
        private readonly Serilog.ILogger _logger;

        public PaieSController(IPaieService PaieService, Serilog.ILogger logger)
        {
            _PaieService = PaieService;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetPaies")]
        [HttpGet]
        public async Task<ActionResult<List<PaieDto>>> Paies()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = _PaieService.GetPayes();

                return new OkObjectResult(ajt);

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetPaies <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPaie"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PaieDto>>> Get(int id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _PaieService.GetPaye(id);
                if (ajt != null)
                {
                    return new OkObjectResult(ajt);
                }
                else
                {
                    var showmessage = "Paie Innexistant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }



            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetPaie <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


    }
}
