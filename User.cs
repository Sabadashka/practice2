namespace Staff_Project
{
    using System.Collections.Generic;

    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
