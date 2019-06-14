using System.Collections.Generic;
using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFT.API.Controllers
{
    [Produces("application/json")]
    [Route("api/TripTypes")]
    public class TripTypesController : Controller
    {
        private readonly IUnitOfWork _unit;

        public TripTypesController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: api/TripTypes
        [HttpGet]
        public IEnumerable<TripTypes> Get()
        {
            return _unit.TripTypes.GetAll();
        }

        // GET: api/TripTypes/5
        [HttpGet("{id}")]
        public TripTypes Get(int id)
        {
            return _unit.TripTypes.Get(id);
        }
        
        // POST: api/TripTypes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/TripTypes/5
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
