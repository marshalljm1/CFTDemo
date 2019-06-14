using System.Collections.Generic;
using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFT.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Travelers")]
    public class TravelersController : Controller
    {
        private readonly IUnitOfWork _unit;

        public TravelersController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: api/Travelers
        [HttpGet]
        public IEnumerable<Travelers> Get()
        {
            return _unit.Travelers.GetAll();
        }

        // GET: api/Travelers/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Travelers
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Travelers/5
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
