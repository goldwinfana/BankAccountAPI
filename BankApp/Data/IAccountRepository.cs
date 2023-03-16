using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;

namespace BankApp.Data
{
    public interface IAccountRepository : IDisposable
    {
        IEnumerable<Accounts> GetAccounts();
        Accounts GetAccountByAccountNumber(string AccountNumber);
        void AddAccount(Accounts Account);
        void DeleteAccount(string AccountNumber);
        void UpdateAccount(Accounts Account);
        void Save();
    }
}
