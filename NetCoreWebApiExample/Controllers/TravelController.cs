using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiExample.DummyData;
using NetCoreWebApiExample.Models;
using NetCoreWebApiExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly ITravelService _travelService;
        private readonly User _currentuser;

        public TravelController(ITravelService travelService)
        {
            this._travelService = travelService;
            this._currentuser = UserData.UserList.Where(a => a.Id == 1).FirstOrDefault(); // buradan kullanıcı set ederek kendi dataları üzerinde işlem yapması için basit bi yetki kontrolü yapmak için
        }

        [HttpGet("getalltravel")]
        public List<Travel> getAllTravel()
        {
            return _travelService.GetAllTravel();
        }
        [HttpPost("addtravel")]
        public ActionResult AddTravel(string from, string to, DateTime date, string comment, int seatcount)
        {
            Travel entity = new Travel();
            entity.Id = Guid.NewGuid();
            entity.UserId = _currentuser.Id; // kendi eklediği için mevcut kullanıcı set ediliyor
            entity.From = from;
            entity.To = to;
            entity.Date = date;
            entity.Comment = comment;
            entity.SeatsCount = seatcount;
            entity.EmptySeatsCount = seatcount; //ilk tanımlamada tüm koltuklar boş set ediliyor
            entity.Status = StatusType.New;

            var result = _travelService.AddTravel(entity);

            if (result)
                return Ok("Kayıt Eklendi. Id = " + entity.Id);
            else
                return BadRequest("Kayıt Yapılırken bir hata meydana geldi");

        }
        [HttpPost("publishtravel")]
        public ActionResult PublishTravel(Guid id)
        {
            Travel entity = _travelService.GetTravelById(id);

            if (entity.UserId != _currentuser.Id)
            {
                return BadRequest("Bu kayıt üzerinde işlem yapamazsınız");
            }

            var result = _travelService.PublishTravel(id);

            if (result)
                return Ok("Kayıt Yayınlandı");
            else
                return BadRequest("Kayıt Yayınlanırken bir hata meydana geldi");
        }
        [HttpPost("unpublishtravel")]
        public ActionResult UnPublishTravel(Guid id)
        {
            Travel entity = _travelService.GetTravelById(id);

            if (entity.UserId != _currentuser.Id)
            {
                return BadRequest("Bu kayıt üzerinde işlem yapamazsınız");
            }

            var result = _travelService.UnPublishTravel(id);

            if (result)
                return Ok("Kayıt Yayından kaldırıldı");
            else
                return BadRequest("Kayıt Yayından kaldırılırken bir hata meydana geldi");
        }
        [HttpGet("searchtravel")]
        public List<Travel> SearchTravel(string from, string to)
        {
            return _travelService.SearchTravel(from, to);
        }

        [HttpPost("jointravel")]
        public ActionResult JoinTravel(Guid id)
        {
            Travel entity = _travelService.GetTravelById(id);
            if (entity != null)
            {
                if (entity.UserId == _currentuser.Id)
                {
                    return BadRequest("Kendi tanımladığınız seyahate katılım sağlayamazsınız.");
                }
                //test yaparken ard arda katılım sağlayarak denenebilmesi için aynı kullanıcının birden fazla katılım sağlaması validasyonunu yapmadım.

                if (entity.Status == StatusType.Published)
                {
                    var emptyseats = entity.SeatsCount - TravelData.travelrowsList.Where(a => a.TravelId == id).Count();
                    if (emptyseats > 0)
                    {
                        var result = _travelService.JoinTravel(id, _currentuser.Id);

                        if (result)
                            return Ok("Katılım Sağlandı");
                        else
                            return BadRequest("Katılım sağlanırken bir hata meydana geldi");
                    }
                    else
                    {
                        return BadRequest("Katılım sağlamak istediğiniz seyahatte boş koltuk bulunmamaktadır.");
                    }
                }
                else
                {
                    return BadRequest("Katılım sağlamak istediğiniz seyahat yayında değil.");
                }
            }
            else
            {
                return BadRequest("Kayıt Yok.");
            }

        }
    }
}
