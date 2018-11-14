using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace WebApp
{
    // TODO 5: unauthorized users should receive 401 status code and should be redirected to Login endpoint   
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ValueTask<Account> Get()
        {
            Response.Redirect("/api/register");
            return _service.LoadOrCreateAsync(Request.Cookies["userId"]);
        }
        
        [Authorize]
        [HttpGet]
        public Account GetByInternalId()
        {
            Int64.TryParse(Request.Cookies["userExternalId"], out var internalId);
            Response.Redirect("/api/register");
            var account = _service.GetFromCache(internalId);
            if (account.Role != "Admin")
            {
                Response.StatusCode = 400;
                return null;
            }

            return account;
        }

        [Authorize]
        [HttpPost("counter")]
        public async Task UpdateAccount()
        {
            //Update account in cache, don't bother saving to DB, this is not an objective of this task.
            var account = await Get();
            account.Counter++;            
        }
    }
}