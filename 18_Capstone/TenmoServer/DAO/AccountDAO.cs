using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private string connString = "Server=.\\SQLEXPRESS;Database=tenmo;Trusted_Connection=True;";

        public Account GetAccount(int userId)
        {
            string sql = "Select * from accounts where user_id = @user_id";
            Account acct = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        acct.UserId = Convert.ToInt32(rdr["user_id"]);
                        acct.Balance = Convert.ToDecimal(rdr["balance"]);
                        acct.AccountId = Convert.ToInt32(rdr["account_id"]);
                    }
                    return acct;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}