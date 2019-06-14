//using System.Collections.Generic;
//using CFT.API.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace CFT.API.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/School")]    
//    [Authorize]
//    public class SchoolController : Controller
//    {
//        private readonly IUnitOfWork _unit;

//        public SchoolController( IUnitOfWork unit)
//        {
//            _unit = unit;
//        }

//        // GET: api/School
//        [HttpGet]
//        public IEnumerable<Schools> Get()
//        {
//            return _unit.Schools.GetAll();
//        }

//        // GET: api/School/5
//        [HttpGet("{id}")]
//        public Schools Get(int id)
//        {
//            return _unit.Schools.Get(id);
//        }

//        // GET: api/School/5
//        [HttpGet]
//        [Route("GetSchoolsBySystem/{id}")]
//        public IEnumerable<Schools> GetSchoolsBySystem(int id)
//        {
//            return _unit.Schools.Find(x => x.SchoolSystemId.Equals(id));
//        }

//        // POST: api/School
//        [HttpPost]
//        public void Post([FromBody]string value)
//        {
//        }
        
//        // PUT: api/School/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody]string value)
//        {
//        }
        
//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
