using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BankApp.Models
{
    public class Accounts
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LastModified { get; set; }

        public int AccountHolderId { get; set; }
        public AccountHolder AccountHolder { get; set; }
    }

    public class WithdrawalRequest
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
    }  

}
