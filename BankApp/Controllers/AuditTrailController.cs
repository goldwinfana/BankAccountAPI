using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Models;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditTrailController : Controller
    {
        private IAuditTrailRepository _auditTrailRepository;
        public AuditTrailController(BankDbContext bankDbContext)
        {
            this._auditTrailRepository = new AuditTrailRepository(bankDbContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_auditTrailRepository.GetAuditTrails());
        }
    }
}
