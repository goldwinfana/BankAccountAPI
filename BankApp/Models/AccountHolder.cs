using System;
using System.Collections.Generic;
using BankApp.Models;


namespace BankApp.Models
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
    }

    public class UsrerDetails
    {
        public string IdNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
