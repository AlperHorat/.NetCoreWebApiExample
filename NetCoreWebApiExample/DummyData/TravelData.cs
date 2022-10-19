using NetCoreWebApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.DummyData
{
    public class TravelData
    {
        public static List<Travel> travelList = new List<Travel>();
        public static List<TravelRows> travelrowsList = new List<TravelRows>();

        static TravelData()
        {
            travelList.Add(new Travel { Id = Guid.NewGuid(), UserId = 1, From = "Ankara", To = "İstanbul", Comment = "test", Date = DateTime.Now.AddDays(-20), SeatsCount = 5, Status = StatusType.Published });
            travelList.Add(new Travel { Id = Guid.NewGuid(), UserId = 2, From = "Bursa", To = "İstanbul", Comment = "test", Date = DateTime.Now.AddDays(-10), SeatsCount = 4, Status = StatusType.Published });
            travelList.Add(new Travel { Id = Guid.NewGuid(), UserId = 2, From = "İstanbul", To = "Mersin", Comment = "test", Date = DateTime.Now.AddDays(-1), SeatsCount = 3, Status = StatusType.Published });
            travelList.Add(new Travel { Id = Guid.NewGuid(), UserId = 1, From = "Antalya", To = "Hatay", Comment = "test", Date = DateTime.Now.AddDays(-5), SeatsCount = 2, Status = StatusType.UnPublished });
            travelList.Add(new Travel { Id = Guid.NewGuid(), UserId = 1, From = "Ankara", To = "Hatay", Comment = "test", Date = DateTime.Now.AddDays(-5), SeatsCount = 5, Status = StatusType.UnPublished });
        }
    }
}
