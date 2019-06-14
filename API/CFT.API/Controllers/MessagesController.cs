using System.Collections.Generic;
using CFT.API.Interfaces;
using CFT.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFT.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly IUnitOfWork _unit;

        public MessagesController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public IEnumerable<Messages> Get()
        {
            return _unit.Messages.GetAll();
        }

        //// GET: api/Messages/5
        //[HttpGet]
        //public Messages Get()
        //{
        //    return _unit.Messages.GetCurrentMessage();
        //}

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Messages
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Messages/5
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
