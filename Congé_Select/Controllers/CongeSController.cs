using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using Service.IService;

namespace Conge_Select.Controllers
{
    [Route("CongeS")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class CongeSController : ControllerBase
    {
        private readonly ICongeService _CongeService;
        //private readonly IServiceAsync<Conge, CongeDto> _service;
        private readonly Serilog.ILogger _logger;

        public CongeSController(ICongeService CongeService, Serilog.ILogger logger)
        {
            _CongeService = CongeService;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetConges")]
        [HttpGet]
        public async Task<ActionResult<List<CongeDto>>> Conges()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = _CongeService.GetConges();

                return new OkObjectResult(ajt);

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetConges <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConge"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CongeDto>>> Get(int id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _CongeService.GetConge(id);
                if (ajt != null)
                {
                    return new OkObjectResult(ajt);
                }
                else
                {
                    var showmessage = "Conge Innexistant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }



            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetConge <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


    }
}
