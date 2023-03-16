using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;

namespace BankApp.Data
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly BankDbContext _dbContext;

        public AuditTrailRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AuditTrail GetAuditTrail(int Id)
        {
            return _dbContext.AuditTrail.Find(Id);
        }

        public IEnumerable<AuditTrail> GetAuditTrails()
        {
            return _dbContext.AuditTrail.ToList();
        }

        public void AddAuditTrail(AuditTrail auditTrail)
        {
            _dbContext.AuditTrail.Add(auditTrail);
            _dbContext.SaveChanges();
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