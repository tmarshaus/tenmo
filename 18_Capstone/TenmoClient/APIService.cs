using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class APIService
    {
        const string API_URL = "http://localhost:44315/account";
        const string API_TRANSFER_URL = "http://localhost:44315/account/transfer";
        private static RestClient authClient = new RestClient();

        public bool LoggedIn { get { return UserService.IsLoggedIn(); } }
        
        public Account GetAccount()
        {
            if (LoggedIn)
            {
                RestRequest request = new RestRequest($"API_URL/{UserService.GetUserId()}");
                IRestResponse<Account> response = authClient.Get<Account>(request);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    //response not received
                    Console.WriteLine("An error occurred communicating with the server.");
                    return null;
                }
                else if (!response.IsSuccessful)
                {
                    //response non-2xx
                    Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
                    return null;
                }
                else
                {
                    //success
                    return response.Data;
                }

            }
            else
            {
                Console.WriteLine("You are not logged in");
                return null;
            }
        }

        

    }
}
