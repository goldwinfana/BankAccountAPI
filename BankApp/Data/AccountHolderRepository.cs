using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;

namespace BankApp.Data
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
        private readonly BankDbContext _dbContext;

        public AccountHolderRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AccountHolder GetAccountHolderById(string idNumber)
        {
            return _dbContext.AccountHolders.Where(o => o.IdNumber == idNumber).FirstOrDefault();
        }

        public AccountHolder GetAccountHolderByEmail(string EmailAddress)
        {
            return _dbContext.AccountHolders.Where(o => o.EmailAddress == EmailAddress).FirstOrDefault();
        }

        public IEnumerable<AccountHolder> GetAccountHolders()
        {
            return _dbContext.AccountHolders.ToList();
        }

        public void AddAccountHolder(AccountHolder accountHolder)
        {
            _dbContext.AccountHolders.Add(accountHolder);
        }

        public void UpdateAccountHolder(AccountHolder accountHolder)
        {
            _dbContext.AccountHolders.Update(accountHolder);
            _dbContext.SaveChanges();
        }

        public void DeleteAccountHolder(string idNumber)
        {
            var accountHolder = _dbContext.AccountHolders.Find(idNumber);
            if (accountHolder != null)
            {
                _dbContext.AccountHolders.Remove(accountHolder);
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


