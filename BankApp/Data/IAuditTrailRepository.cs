
using BankApp.Models;
using System;
using System.Collections.Generic;

namespace BankApp.Data
{
    public interface IAuditTrailRepository : IDisposable
    {
        IEnumerable<AuditTrail> GetAuditTrails();
        AuditTrail GetAuditTrail(int Id);
        void AddAuditTrail(AuditTrail AuditTrail);
        void Save();
    }
}