// <copyright file="Roullete.cs" company="Task">
//     Company copyright tag.
// </copyright>
// <summary>
// The Roullete entity.
// </summary>
//-----------------------------------------------------------------------

namespace ReoulleteApi
{
    using System.Collections.Generic;
    using ReoulleteApi.Entities;

    /// <summary>
    /// The roulette class.
    /// </summary>
    public class Roullete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Roullete"/> class.
        /// </summary>
        public Roullete()
        {
            this.Users = new List<User>();
        }

        /// <summary>
        /// Gets or sets a Open roulette status.
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// Gets or sets a list o Users.
        /// </summary>
        public IList<User> Users { get; set; }
    }
}