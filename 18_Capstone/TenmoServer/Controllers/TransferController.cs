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
    [Authorize]
    public class TransferController : ControllerBase
    {
        private ITransferDAO transferDAO;
        private IAccountDAO accountDAO;

        public TransferController(ITransferDAO transferDAO, IAccountDAO accountDAO)
        {
            this.transferDAO = transferDAO;
            this.accountDAO = accountDAO;
        }

        [HttpPost]
        public ActionResult<Transfer> SendTEBucks(Transfer transfer)
        {
            //Make sure from account is owned by current user
            Account currentUserAccount = accountDAO.GetAccount(GetUserId());
            transfer.AccountFrom = currentUserAccount.AccountId;
            transferDAO.SendMoneyTo(transfer);
            return Ok(transfer);
        }

        [HttpPost]
        public ActionResult<Transfer> RequestTEBucks(Transfer transfer)
        {
            //Make sure from account is owned by current user
            Account currentUserAccount = accountDAO.GetAccount(GetUserId());
            transfer.AccountFrom = currentUserAccount.AccountId;
            transferDAO.RequestMoney(transfer);
            return Ok(transfer);
        }

        [HttpGet("users")]
        public ActionResult<List<User>> DisplayAllUsers()
        {
            List<User> users = transferDAO.GetAllUsers();
            return Ok(users);
        }

        private int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            return String.IsNullOrEmpty(strUserId) ? 0 : Convert.ToInt32(strUserId);
        }

        [HttpGet]
        public ActionResult<List<Transfer>> GetTransferList()
        {
            List<Transfer> transfers = transferDAO.GetUserTransfers();
            return Ok(transfers);
        }

        [HttpGet("{transferId}")]
        public ActionResult<TransferDetails> GetTransfer(int transferId)
        {
            TransferDetails details = transferDAO.GetTransferDetails(transferId);
            return Ok(details);
        }

        [HttpPut("{transferId}")]
        public ActionResult<Transfer> UpdateApprovedTransferStatus(int transferId)
        {
            Transfer transfer = transferDAO.UpdateApprovedTransfer(transferId);
            return Ok(transfer);
        }

        [HttpPut("{transferId}")]
        public ActionResult<Transfer> UpdateRejectedTransferStatus(int transferId)
        {
            Transfer transfer = transferDAO.UpdateRejectedTransfer(transferId);
            return Ok(transfer);
        }

    }
}