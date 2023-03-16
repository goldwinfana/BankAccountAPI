using Microsoft.AspNetCore.Mvc;
using System;
using BankApp.Data;
using BankApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BankApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private IAuditTrailRepository _auditTrailRepository;
        private IAccountRepository _accountRepository;
        private IAccountHolderRepository _accountholdersRepository;
        private static IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        public AccountController(BankDbContext bankDbContext)
        {
            this._accountRepository = new AccountRepository(bankDbContext);
            this._accountholdersRepository = new AccountHolderRepository(bankDbContext);
            this._auditTrailRepository = new AuditTrailRepository(bankDbContext);
        }

       
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsrerDetails accountHolder)
        {
            // Authenticate user here...

            var user = _accountholdersRepository.GetAccountHolderById(accountHolder.IdNumber);

            if (user ==null)
            {
                return Ok("Incorrect credentials provided...");
            }

            if (user.EmailAddress != accountHolder.EmailAddress)
            {
                return Ok("Incorrect credentials provided...");
            }
            // Generate JWT token
            var securitykey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.IdNumber),
            new Claim(ClaimTypes.Name, user.IdNumber),
            new Claim(ClaimTypes.Email, user.EmailAddress)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            TrackActivities("User with id number "+user.IdNumber+" logged in","Account");
            return Ok(tokenString);
        }


        [HttpGet]
        public IActionResult GetBankAccounts()
        {
            var identity = HttpContext.User.Identity;
            TrackActivities(this.User+ " views all accounts", "Account");
            return Ok(new { users= _accountRepository.GetAccounts(),user= identity});
        }
        

        [HttpGet("AccountNumber")]
        public IActionResult GetBankAccount(string accountNumber)
        {
            var bankAccount = _accountRepository.GetAccountByAccountNumber(accountNumber);
            if (bankAccount == null)
            {
                return NotFound();
            }
            TrackActivities(this.User + " gets accounts details", "Account");
            return Ok(bankAccount);
        }

        [HttpPost("WithdrawalRequest")]
        public IActionResult CreateWithdrawal([FromBody] WithdrawalRequest request)
        {
            var Account = _accountRepository.GetAccountByAccountNumber(request.AccountNumber);
            if (Account == null)
            {
                return NotFound();
            }
            if (!Account.IsActive)
            {
                return BadRequest("Account is not active.");
            }

            if (request.Amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than 0.");
            }

            if (request.Amount > Account.AvailableBalance)
            {
                return BadRequest("Withdrawal amount cannot be greater than account balance.");
            }

            if (request.Amount == Account.AvailableBalance)
            {
                if (Account.AccountType != "Fixed Deposit")
                {
                    return BadRequest("100% withdrwall is only allowed on Fixed Deposit Accounts.");
                }
                
            }
            TrackActivities(Account.Name +" withdraws "+request.Amount+" from account no:"+Account.AccountNumber+"", "Account");
            Account.AvailableBalance -= request.Amount;
            _accountRepository.UpdateAccount(Account);
            return Ok(Account);
        }

        private void TrackActivities(string action, string controller)
        {
            var auditTrail = new AuditTrail
            {
                Action = action,
                ControllerName = controller,
                TimeStamp = new System.DateTime()
            };
            _auditTrailRepository.AddAuditTrail(auditTrail);
        }
    }
}

