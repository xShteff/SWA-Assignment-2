using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using dweis.WPFPresentation.UserData;
using Newtonsoft.Json;

namespace SwaAssignment2.Users
{
    public class UserProvider : IUserProvider
    {
        public async Task<IEnumerable<Person>> GetRandomUsers(int numberOfEmployees)
        {
            var users = new List<Person>();
            var rng = new Random();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://randomuser.me/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/?nat=dk,gb,us&inc=name,phone,email,location,picture,login,nat&results={numberOfEmployees}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    UserResponseRoot userResponse = JsonConvert.DeserializeObject<UserResponseRoot>(json);
                    foreach (var randomUser in userResponse.Results)
                    {
                        var salary = rng.Next(1, 40) * 1000;
                        var name = new Name
                        {
                            FirstName = randomUser.Name.First,
                            LastName = randomUser.Name.Last
                        };
                        var location = new Location
                        {
                            City = randomUser.Location.City,
                            Postcode = randomUser.Location.Postcode.ToString(),
                            State = randomUser.Location.State,
                            Street = randomUser.Location.Street
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
            return users;
        }
    }
}
