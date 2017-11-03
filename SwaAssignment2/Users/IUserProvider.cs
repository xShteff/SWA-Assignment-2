using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwaAssignment2.Users
{
    public interface IUserProvider
    {
        Task<IEnumerable<Person>> GetRandomUsers(int numberOfEmployees);
    }
}
