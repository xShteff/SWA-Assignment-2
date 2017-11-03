
namespace dweis.WPFPresentation.UserData
{
    public class User
    {
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public Login Login { get; set; }
        public string Dob { get; set; }
        public string Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Id Id { get; set; }
        public Picture Picture { get; set; }
        public string Nat { get; set; }

        public override string ToString()
        {
            return $"{Name} \nemail: {Email} \nDOB: {Dob} \nCell: {Cell}";
        }
    }
}
