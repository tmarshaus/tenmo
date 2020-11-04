using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private string connString = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";

        public List<User> GetAllAccounts()
        {
            List<User> users = new List<User>();
            string sql = "Select user_id, username from users";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

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

        public Account SendMoneyTo(int fromUserId, int toUserId, decimal sentMoney)
        {
            string sql = "Select balance from account where user_id = @fromUserId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

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
    }
}