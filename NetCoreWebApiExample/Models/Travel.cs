using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.Models
{
    public class Travel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int SeatsCount { get; set; }
        public int EmptySeatsCount { get; set; }
        public string Status { get; set; }
    }
}
