//-----------------------------------------------------------------------
// <copyright file="BetDto.cs" company="Task">
//     Company copyright tag.
// </copyright>
// <summary>
// The Bet Dto.
// </summary>
//-----------------------------------------------------------------------

namespace ReoulleteApi.Domain.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// BetDto class.
    /// </summary>
    public class BetDto
    {
        /// <summary>
        /// Gets or sets the cash amount to bet.
        /// </summary>
        [Range(0, 10000, ErrorMessage = "Value must be between 0 to 10000")]
        public float CashAmount { get; set; }

        /// <summary>
        /// Gets or sets the number to bet.
        /// </summary>
        [Range(0, 38, ErrorMessage = "Value must be between 0 to 38")]
        public int Number { get; set; }
    }
}
