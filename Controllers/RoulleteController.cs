//-----------------------------------------------------------------------
// <copyright file="RoulleteController.cs" company="Task">
//     Company copyright tag.
// </copyright>
// <summary>
// The roulette controller.
// </summary>
//-----------------------------------------------------------------------

namespace ReoulleteApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ReoulleteApi.Contracts.Roullete;
    using ReoulleteApi.Domain.Dtos;

    /// <summary>
    /// RoulleteController class.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoulleteController : Controller
    {
        /// <summary>
        /// The RoulleteServices.
        /// </summary>
        private readonly IRoulleteServices roulleteServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoulleteController"/> class.
        /// </summary>
        /// <param name="roulleteServices">The roulette service.</param>
        public RoulleteController(IRoulleteServices roulleteServices)
        {
            this.roulleteServices = roulleteServices;
        }

        /// <summary>
        /// Get the entry point.
        /// </summary>
        /// <returns>Returns a welcome message.</returns>
        [HttpGet]
        public string EntryPoint()
        {
            string firstPage = "Welcome to Roullete Online, enter one of  the following endpoints \n" +
                               "/create \n" +
                               "/{roullete_Id}/open \n" +
                               "/{roullete_Id}/bet \n" +
                               "/{roullete_Id}/close \n" +
                               "/list";
            return firstPage;
        }

        /// <summary>
        /// Creates a new roulette.
        /// </summary>
        /// <returns>Returns de roulette id.</returns>
        [Route("create")]
        [HttpGet]
        public string CreateRoullete()
        {
            return this.roulleteServices.GenerateID();
        }

        /// <summary>
        /// Open a roulette by id.
        /// </summary>
        /// <param name="roullete_Id">The roulette id.</param>
        /// <returns>Returns a message if was allow or deny</returns>
        [Route("{roullete_Id}/open")]
        [HttpGet]
        public string OpenRoullete(string roullete_Id)
        {
            return this.roulleteServices.AllowDenyAccess(roullete_Id);
        }

        /// <summary>
        /// Creates a bet by request.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roullete_id">The roulette id.</param>
        /// <param name="request">The request from post</param>
        /// <returns>Returns a message if was successfully created.</returns>
        [Route("{roullete_id}/bet")]
        [HttpPost]
        public string Betroullete([FromHeader(Name = "user-id")] string userId, string roullete_id, [FromBody] BetDto request)
        {
            return this.roulleteServices.Bet(userId, roullete_id, request);
        }

        /// <summary>
        /// Closes a roulette bet.
        /// </summary>
        /// <param name="roullete_id">The roulette id.</param>
        /// <returns>Returns message success or fail close roulette.</returns>
        [Route("{roullete_id}/close")]
        [HttpPost]
        public string CloseRoullete(string roullete_id)
        {
            return this.roulleteServices.Close(roullete_id);
        }

        /// <summary>
        /// List all roulettes created.
        /// </summary>
        /// <returns>Returns a message of created roulettes.</returns>
        [Route("list")]
        [HttpGet]
        public string ListRoullete()
        {
            return this.roulleteServices.ListRoulletes();
        }
    }
}
