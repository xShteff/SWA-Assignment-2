using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwaAssignment2.Users
{
    public interface IUserDatabase
    {
        Task<IEnumerable<Person>> GetRandomUsers(int numberOfEmployees);
    }
}
