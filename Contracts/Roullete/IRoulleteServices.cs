using ReoulleteApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReoulleteApi.Contracts.Roullete
{
    public interface IRoulleteServices
    {
        string GenerateID();

        string AllowDenyAccess(string roullete_Id);

        string Bet(string UserId, string roullete_Id, BetDto request);

        string Close(string roullete_Id);

        string ListRoulletes();
    }
}
