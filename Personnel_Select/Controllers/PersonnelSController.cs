using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using Service.IService;

namespace Personnel_Select.Controllers
{
    [Route("PersonnelS")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class PersonnelSController : ControllerBase
    {
        private readonly IPersonnelService _PersonnelService;
        //private readonly IServiceAsync<Personnel, PersonnelDto> _service;
        private readonly Serilog.ILogger _logger;

        public PersonnelSController(IPersonnelService PersonnelService, Serilog.ILogger logger)
        {
            _PersonnelService = PersonnelService;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetPersonnels")]
        [HttpGet]
        public async Task<ActionResult<List<PersonnelDto>>> Personnels()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _PersonnelService.GetAllPersonnel();

                return new OkObjectResult(ajt);

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetPersonnels <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonnel"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PersonnelDto>>> Get(int id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _PersonnelService.GetPersonnel(id);
                if (ajt != null)
                {
                    return new OkObjectResult(ajt);
                }
                else
                {
                    var showmessage = "Personnel Innexistant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }



            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetPersonnel <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


    }
}
