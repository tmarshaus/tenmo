using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TenmoClient;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private IAccountDAO accountDAO;

        public TransferDAO(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        private string connString = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";

        public List<User> GetAllAccounts()
        {
            List<User> users = new List<User>();
            string sql = "Select user_id, username from users where user_id != @currentUserId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@currentUserId", UserService.GetUserId());

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(rdr["user_id"]);
                        user.Username = Convert.ToString(rdr["username"]);
                        users.Add(user);
                    }
                    return users;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Transfer SendMoneyTo(Transfer transfer) //removed fromUserId
        {
            //Get current User id balance
            decimal currentUserBalance = accountDAO.GetAccount(transfer.AccountFrom).Balance;

            //Get toUserid balance
            decimal toUserBalance = accountDAO.GetAccount(transfer.AccountTo).Balance;

            //Check balance of current user against sentMoney
            if (currentUserBalance >= transfer.Amount)
            {
                //Updated balance for user
                decimal newCurrentUserBalance = currentUserBalance - transfer.Amount;

                //Updated balance for toUserId
                decimal newToUserBalance = toUserBalance + transfer.Amount;

                //Update transfer status
                transfer.TransferStatusId = 2;

                //Update balance for current user
                UpdateBalance(transfer.AccountFrom, newCurrentUserBalance);

                //Update balance for toUser
                UpdateBalance(transfer.AccountTo, newToUserBalance);

                LogTransfer(transfer.AccountFrom, transfer.AccountTo, transfer.Amount, 2, transfer.TransferStatusId);
                return transfer;
            }
            else
            {
                transfer.TransferStatusId = 3;
                return transfer;
            }
        }

        public bool UpdateBalance(int userId, decimal newBalance)
        {
            string sql = @"Update accounts
                               Set balance = @newBalance
                                   Where user_id = @userId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool LogTransfer(int fromId, int toId, decimal sentMoney, int transferType, int transferStatusId)
        {
            string sql = @"Insert into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
                            Values(@transferTypeId, @transferStatusId, @accountFrom, @accountTo, @amount)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transferTypeId", transferType);
                    cmd.Parameters.AddWithValue("@transferStatusId", transferStatusId);
                    cmd.Parameters.AddWithValue("@accountFrom", fromId);
                    cmd.Parameters.AddWithValue("@accountTo", toId);
                    cmd.Parameters.AddWithValue("@amount", sentMoney);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Transfer> GetUserTransfers()
        {
            string sql = "Select * from transfers where @currentUserId in (account_from, account_to)";
            List<Transfer> transfers = new List<Transfer>();
            int currentUserId = UserService.GetUserId();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@currentUserId", currentUserId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Transfer tran = new Transfer();

                        tran.TransferId = Convert.ToInt32(rdr["transfer_id"]);
                        tran.TransferTypeId = Convert.ToInt32(rdr["transfer_type_id"]);
                        tran.TransferStatusId = Convert.ToInt32(rdr["transfer_status_id"]);
                        tran.AccountFrom = Convert.ToInt32(rdr["account_from"]);
                        tran.AccountTo = Convert.ToInt32(rdr["account_to"]);
                        tran.Amount = Convert.ToDecimal(rdr["amount"]);

                        transfers.Add(tran);
                    }
                    return transfers;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}