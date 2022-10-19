using NetCoreWebApiExample.DummyData;
using NetCoreWebApiExample.Models;
using NetCoreWebApiExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.Managers
{
    public class TravelManager : ITravelService
    {
        public bool AddTravel(Travel travel)
        {
            if (TravelData.travelList.Where(a => a.Id == travel.Id).Count() == 0)
            {
                TravelData.travelList.Add(travel);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Travel> GetAllTravel()
        {
            var list = TravelData.travelList;
            list.ForEach(a => a.EmptySeatsCount = a.SeatsCount - TravelData.travelrowsList.Where(b => b.TravelId == a.Id).Count());
            return list;
        }
        public Travel GetTravelById(Guid id)
        {
            return TravelData.travelList.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool JoinTravel(Guid id, int userid)
        {
            Travel p = TravelData.travelList.Where(a => a.Id == id).FirstOrDefault();
            if (p != null)
            {
                TravelRows entity = new TravelRows();
                entity.Id = Guid.NewGuid();
                entity.UserId = userid;
                entity.TravelId = id;
                TravelData.travelrowsList.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PublishTravel(Guid id)
        {
            Travel p = TravelData.travelList.Where(a => a.Id == id).FirstOrDefault();
            if (p != null)
            {
                p.Status = StatusType.Published;
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Travel> SearchTravel(string from, string to)
        {
            List<Travel> list = TravelData.travelList.Where(a => a.Status == StatusType.Published).ToList();
            list.ForEach(a => a.EmptySeatsCount = a.SeatsCount - TravelData.travelrowsList.Where(b => b.TravelId == a.Id).Count());
            if (!string.IsNullOrEmpty(from))
            {
                list = list.Where(a => a.From.ToLower().Contains(from.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(to))
            {
                list = list.Where(a => a.To.ToLower().Contains(to.ToLower())).ToList();
            }
            return list;
        }

        public bool UnPublishTravel(Guid id)
        {
            Travel p = TravelData.travelList.Where(a => a.Id == id).FirstOrDefault();
            if (p != null)
            {
                p.Status = StatusType.UnPublished;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
