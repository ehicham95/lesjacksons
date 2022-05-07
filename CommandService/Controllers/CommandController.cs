using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/platforms")]
    [ApiController]
    public class CommandController : Controller
    {

        public CommandController()
        {
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
             Console.WriteLine("--> Inbound Post # Command service");
             return Ok();
        }
    }
}