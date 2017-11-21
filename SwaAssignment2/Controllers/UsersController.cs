using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SwaAssignment2.Users;

namespace SwaAssignment2.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserDatabase _userDatabase;

        public UsersController(IUserDatabase database)
        {
            _userDatabase = database;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _userDatabase.GetAllUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public Person Get(string id)
        {
            return _userDatabase.GetUser(id);
        }
        
        // POST: api/Users
        [HttpPost]
        public void Post([FromBody]Person value)
        {
            _userDatabase.AddUser(value);
        }
        
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Person value)
        {
            _userDatabase.UpdateDataForUser(id, value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _userDatabase.RemoveUser(id);
        }
    }
}
