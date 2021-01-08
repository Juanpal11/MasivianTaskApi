//-----------------------------------------------------------------------
// <copyright file="IRoulleteServices.cs" company="Task">
//     Company copyright tag.
// </copyright>
// <summary>
// IRoulleteServices Interface.
// </summary>
//-----------------------------------------------------------------------

namespace ReoulleteApi.Contracts.Roullete
{
    using ReoulleteApi.Domain.Dtos;

    /// <summary>
    /// The contract of roullete service.
    /// </summary>
    public interface IRoulleteServices
    {
        /// <summary>
        /// Generates a random Id for a roullete.
        /// </summary>
        /// <returns>Return a random string id.</returns>
        string GenerateID();

        /// <summary>
        /// Generates an allow or deny message if roulette is open or not.
        /// </summary>
        /// <param name="roullete_Id">The roulette Id.</param>
        /// <returns>Returns a string with allow or deny message</returns>
        string AllowDenyAccess(string roullete_Id);

        /// <summary>
        /// Creates a bet.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="roullete_Id">The roulette Id.</param>
        /// <param name="request">The request parameters from post.</param>
        /// <returns>Returns successful or failed bet creation.</returns>
        string Bet(string userId, string roullete_Id, BetDto request);

        /// <summary>
        /// Close a specific roulette id bet.
        /// </summary>
        /// <param name="roullete_Id">The roulette Id.</param>
        /// <returns>Returns successful or failed close bet game.</returns>
        string Close(string roullete_Id);

        /// <summary>
        /// Get a list of created roulettes.
        /// </summary>
        /// <returns>Return a list of roulettes with status.</returns>
        string ListRoulletes();
    }
}
