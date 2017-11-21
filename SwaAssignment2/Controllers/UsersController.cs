using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Person>> Get()
        {
            return await _userDatabase.GetRandomUsers(20);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public Person Get(int id)
        {
            return null;
        }
        
        // POST: api/Users
        [HttpPost]
        public void Post([FromBody]Person value)
        {
        }
        
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Person value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
