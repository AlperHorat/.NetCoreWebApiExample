using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.Models
{
    public class TravelRows
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Guid TravelId { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
