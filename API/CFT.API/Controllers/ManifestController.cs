using System.Collections.Generic;
using System.Linq;
using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFT.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Manifest")]
    public class ManifestController : Controller
    {

        private readonly IUnitOfWork _unit;

        public ManifestController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: api/Manifest
        [HttpGet]
        public IEnumerable<TripManifests> Get()
        {
            return _unit.Manifests.GetAll();
        }

        // GET: api/Manifest/5
        [HttpGet("{id}")]
        public TripManifests Get(int id)
        {
            return _unit.Manifests.Get(id);
        }

        // GET: api/Manifest/5
        [HttpGet("getmanifestsbybus/{id}")]
        public IEnumerable<TripManifests> GetManifestsByBus(int id)
        {
            var trips = _unit.Trips.Find(x => x.AssignedBus.Equals(id)).Select(x => x.Id);
            return _unit.Manifests.Find(x => trips.Contains(x.TripId));
        }

        // POST: api/Manifest
        [HttpPost]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]TripManifests value)
        {
            try
            {
                _unit.Manifests.Add(value);
                _unit.Complete();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        // PUT: api/Manifest/5
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
