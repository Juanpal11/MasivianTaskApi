using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReoulleteApi.Domain.Entities
{
    public class RoulleteList
    {
        public RoulleteList()
        {
            List = new List<ToList>();
        }
        public IList<ToList> List { get; set; }
    }
}
