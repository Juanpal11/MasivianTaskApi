using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReoulleteApi.Contracts.Roullete;
using ReoulleteApi.Domain.Dtos;

namespace ReoulleteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoulleteController : Controller
    {
        private readonly IRoulleteServices roulleteServices;

        public RoulleteController(IRoulleteServices roulleteServices)
        {
            this.roulleteServices = roulleteServices;
        }

        [HttpGet]
        public string EntryPoint()
        {
            string firstPage = ("Welcome to Roullete Online, enter one of  the following urls");
            return firstPage;
        }

        [Route("create")]
        [HttpGet]
        public string CreateRoullete()
        {
            return this.roulleteServices.GenerateID();
        }

        [Route("{roullete_Id}/open")]
        [HttpGet]
        public string OpenRoullete(string roullete_Id)
        {
            return this.roulleteServices.AllowDenyAccess(roullete_Id);
        }

        [Route("{roullete_id}/bet")]
        [HttpPost]
        public string Betroullete([FromHeader(Name = "user-id")] string UserId, string roullete_id, [FromBody] BetDto request)
        {
            return this.roulleteServices.Bet(UserId, roullete_id, request);
        }

        [Route("{roullete_id}/close")]
        [HttpPost]
        public string CloseRoullete(string roullete_id)
        {
            return this.roulleteServices.Close(roullete_id);
        }

        [Route("list")]
        [HttpGet]
        public string ListRoullete()
        {
            return this.roulleteServices.ListRoulletes();
        }
    }
}
