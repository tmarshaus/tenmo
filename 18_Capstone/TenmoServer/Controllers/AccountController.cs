using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;

namespace TenmoServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountDAO accountDAO;

        [HttpGet]
        public decimal GetAccountBalance(int userId)
        {
            return accountDAO.GetBalance(userId);
        }
    }
}