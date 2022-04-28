namespace Staff_Project
{
    using System;

    class Staff : User
    {
        public decimal? Salary { get; set; } 
        public DateTime? FirstDayInCompany { get; set; }
    }
}
