using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;

namespace BankApp.Data
{
    public interface IAccountHolderRepository : IDisposable
    {
        IEnumerable<AccountHolder> GetAccountHolders();
        AccountHolder GetAccountHolderById(string idNumber);
        AccountHolder GetAccountHolderByEmail(string EmailAddress);
        void AddAccountHolder(AccountHolder accountHolder);
        void DeleteAccountHolder(string idNumber);
        void UpdateAccountHolder(AccountHolder accountHolder);
        void Save();
    }
}
