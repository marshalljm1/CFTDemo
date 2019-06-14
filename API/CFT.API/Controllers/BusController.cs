using System.Collections.Generic;
using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFT.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Bus")]
    public class BusController : Controller
    {
        private readonly IUnitOfWork _unit;

        public BusController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: api/Bus
        [HttpGet]
        public IEnumerable<Buses> Get()
        {
            return _unit.Buses.GetAll();
        }

        // GET: api/Bus/5
        [HttpGet("{id}")]
        public Buses Get(int id)
        {
            return _unit.Buses.Get(id);
        }
        
        // POST: api/Bus
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Bus/5
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
