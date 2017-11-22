using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using dweis.WPFPresentation.UserData;
using Newtonsoft.Json;

namespace SwaAssignment2.Users
{
    public class UserDatabase : IUserDatabase
    {
        private readonly List<Person> _users = new List<Person>();

        public UserDatabase()
        {
            LoadNewUsers(20).Wait();
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _users;
        }

        public void RemoveUser(string id)
        {
            _users.RemoveAll(person => person.Id == id);
        }

        public void UpdateDataForUser(string id, Person updatedUser)
        {
            RemoveUser(id);
            _users.Add(updatedUser);
        }

        public void AddUser(Person user)
        {
            _users.Add(user);
        }

        public Person GetUser(string id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }

        public async Task LoadNewUsers(int count)
        {
            var users = new List<Person>();
            var rng = new Random();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://randomuser.me/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/?nat=dk,gb,us&inc=name,phone,email,location,picture,login,nat&results={count}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    UserResponseRoot userResponse = JsonConvert.DeserializeObject<UserResponseRoot>(json);
                    foreach (var randomUser in userResponse.Results)
                    {
                        var salary = rng.Next(1, 40) * 1000;
                        var name = new Name
                        {
                            FirstName = randomUser.Name.First.FirstToUpper(),
                            LastName = randomUser.Name.Last.FirstToUpper()
                        };
                        var location = new Location
                        {
                            City = randomUser.Location.City.FirstToUpper(),
                            Postcode = randomUser.Location.Postcode.ToString(),
                            State = randomUser.Location.State.FirstToUpper(),
                            Street = randomUser.Location.Street.FirstToUpper()
                        };
                        var user = new Person
                        {
                            Id = randomUser.Login.Username,
                            Name = name,
                            Email = randomUser.Email,
                            Phone = randomUser.Phone,
                            Address = location,
                            Picture = randomUser.Picture.Large,
                            Nationality = randomUser.Nat,
                            Salary = salary
                        };
                        users.Add(user);
                    }
                }
            }
            _users.AddRange(users);
        }
    }

    static class StringExtensions
    {
        public static string FirstToUpper(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            return char.ToUpper(text.First()) + text.Substring(1);
        }
    }
}
