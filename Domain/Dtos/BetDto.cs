using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReoulleteApi.Domain.Dtos
{
    public class BetDto
    {

        [Range(0, 10000, ErrorMessage = "Value must be between 0 to 10000")]
        public float CashAmount { get; set; }

        [Range(0, 38, ErrorMessage = "Value must be between 0 to 38")]
        public int Number { get; set; }


    }
}
