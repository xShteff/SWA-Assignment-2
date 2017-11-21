using System.Collections.Generic;

namespace SwaAssignment2.Users
{
    public interface IUserDatabase
    {
        IEnumerable<Person> GetAllUsers();
        void RemoveUser(string id);
        void UpdateDataForUser(string id, Person updatedUser);
        void AddUser(Person user);
        Person GetUser(string id);
    }
}
