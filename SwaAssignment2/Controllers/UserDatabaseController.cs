using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwaAssignment2.Users;

namespace SwaAssignment2.Controllers
{
    [Produces("application/json")]
    [Route("api/UserDatabase")]
    public class UserDatabaseController : Controller
    {
        private readonly IUserDatabase _userDatabase;

        public UserDatabaseController(IUserDatabase database)
        {
            _userDatabase = database;
        }

        // GET: api/UserDatabase
        [HttpGet]
        public async Task<object> Get()
        {
            await _userDatabase.LoadNewUsers(20);
            return new { Status = "Success" };
        }

        // GET: api/UserDatabase/5
        [HttpGet("{id}")]
        public async Task<Object> Get(int count)
        {
            await _userDatabase.LoadNewUsers(count);
            return new { Status = "Success" };
        }

        // POST: api/UserDatabase
        [HttpPost]
        public void Post([FromBody]Command value)
        {
            if (string.Equals(value.Action, "dropDatabase", StringComparison.InvariantCultureIgnoreCase))
            {
                _userDatabase.DropAllUsers();
            }
        }
    }

    public class Command
    {
        public string Action { get; set; }
    }
}