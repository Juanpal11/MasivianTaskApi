using ReoulleteApi.Entities;
using System;
using System.Collections.Generic;

namespace ReoulleteApi
{
    public class Roullete
    {
        public Roullete()
        {
            Users = new List<User>();
        }
        public bool Open { get; set; }

        public IList<User> Users { get; set; }
    }
}