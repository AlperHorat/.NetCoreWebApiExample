using NetCoreWebApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.Services
{
    public interface ITravelService
    {
        Travel GetTravelById(Guid id);
        List<Travel> GetAllTravel();
        bool AddTravel(Travel travel);
        bool PublishTravel(Guid id);
        bool UnPublishTravel(Guid id);
        List<Travel> SearchTravel(string from, string to);
        bool JoinTravel(Guid id, int userid);
    }
}
