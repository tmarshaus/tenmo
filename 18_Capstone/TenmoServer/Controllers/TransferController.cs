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
    [Route("account/[controller]")]
    //[Authorize]
    public class TransferController : ControllerBase
    {
        private ITransferDAO transferDAO;
        private IAccountDAO accountDAO;

        public TransferController(ITransferDAO transferDAO, IAccountDAO accountDAO)
        {
            this.transferDAO = transferDAO;
            this.accountDAO = accountDAO;
        }

        [HttpPut("{toUserId}")]
        public ActionResult<Account> SendTEBucks(int toUserId, decimal sentMoney)
        {
            Account currentUserAccount = transferDAO.SendMoneyTo(toUserId, sentMoney);
            return Ok(currentUserAccount);
        }

        [HttpGet("users")]
        public ActionResult<List<User>> DisplayAllUsers()
        {
            List<User> users = transferDAO.GetAllAccounts();
            return Ok(users);
        }
    }
}