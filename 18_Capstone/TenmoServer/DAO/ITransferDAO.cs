using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<User> GetAllUsers();

        Transfer SendMoneyTo(Transfer transfer);

        List<List<Transfer>> GetUserTransfers();

        bool LogTransfer(int fromId, int toId, decimal sentMoney, int transferType, int transferStatusId);

        bool UpdateBalance(int userId, decimal newBalance);

        TransferDetails GetTransferDetails(int transferId);
    }
}