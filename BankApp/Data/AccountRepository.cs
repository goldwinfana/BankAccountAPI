using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;

namespace BankApp.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDbContext _dbContext;

        public AccountRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Accounts GetAccountByAccountNumber(string accountNumber)
        {
            return _dbContext.Accounts.Where(o=>o.AccountNumber ==accountNumber).FirstOrDefault();
        }

        public IEnumerable<Accounts> GetAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public void AddAccount(Accounts account)
        {
            _dbContext.Accounts.Add(account);
        }

        public void UpdateAccount(Accounts account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(string AccountNumber)
        {
            var accounts = _dbContext.Accounts.Find(AccountNumber);
            if (accounts != null)
            {
                _dbContext.Accounts.Remove(accounts);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}


