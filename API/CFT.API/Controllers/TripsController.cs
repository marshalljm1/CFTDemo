using System.Collections.Generic;
using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFT.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Trips")]
    public class TripsController : Controller
    {
        private readonly IUnitOfWork _unit;

        public TripsController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<Trips> Get()
        {
            return _unit.Trips.GetAll();
        }

        // GET: api/Trips/5
        [HttpGet("{id}", Name = "Get")]
        public Trips Get(int id)
        {
            return _unit.Trips.Get(id);
        }

        //GET: api/Trips/GetTripsByBus5
        [HttpGet]
        [Route("GetTripsByBus/{id}")]
        public IEnumerable<Trips> GetTripsByBus(int id)
        {
            return _unit.Trips.Find(x => x.AssignedBus.Equals(id));
        }

        //GET: api/Trips/GetTripsByBus5
        [HttpGet]
        [Route("GetPriceByTripType/{id}")]
        public decimal GetPriceByTripType(int id)
        {
            return _unit.TripTypes.Get(id).Price;
        }

        // POST: api/Trips
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
