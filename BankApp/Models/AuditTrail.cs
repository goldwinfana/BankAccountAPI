using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class AuditTrail
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string ControllerName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}