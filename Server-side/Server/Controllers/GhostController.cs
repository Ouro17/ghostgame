using Business;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GhostController : ControllerBase
    {
        private ILogic<string> logic;

        public GhostController(ILogic<string> logic)
        {
            this.logic = logic;
        }

        [HttpGet("play")]
        public ActionResult<ResultContainer<string>> Play(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return BadRequest("Need a word to play");
            }

            return Ok(logic.Play(word));
        }
    }
}
