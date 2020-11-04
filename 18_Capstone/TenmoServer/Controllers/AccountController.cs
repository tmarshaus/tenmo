using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountDAO accountDAO;

        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        [HttpGet("{userId}")]
        public ActionResult<Account> GetUserAccount(int userId)
        {
            Account acct = accountDAO.GetAccount(userId);

            if (acct == null)
            {
                return NotFound();
            }

            return Ok(acct);
        }
    }
}