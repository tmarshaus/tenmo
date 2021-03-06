﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class APIService
    {
        private const string API_URL = "https://localhost:44315/account";
        private const string API_TRANSFER_URL = "https://localhost:44315/account/transfer";
        private static RestClient authClient = new RestClient();

        public bool LoggedIn { get { return UserService.IsLoggedIn(); } }

        public Account GetAccount()
        {
            if (LoggedIn)
            {
                RestRequest request = new RestRequest($"{API_URL}/{UserService.GetUserId()}");

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

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

        public List<User> GetUsers()
        {
            if (LoggedIn)
            {
                RestRequest request = new RestRequest($"{API_TRANSFER_URL}/users");

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<List<User>> response = authClient.Get<List<User>>(request);

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

        public Transfer SendMoney(int toUserId, decimal sentMoney)
        {
            if (LoggedIn)
            {
                Transfer transfer = new Transfer();
                transfer.AccountFrom = UserService.GetUserId();
                transfer.AccountTo = toUserId;
                transfer.Amount = sentMoney;
                transfer.TransferStatusId = 2;
                transfer.TransferTypeId = 2;

                RestRequest request = new RestRequest($"{API_TRANSFER_URL}/sends");
                request.AddJsonBody(transfer);

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<Transfer> response = authClient.Post<Transfer>(request);

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

        public Transfer RequestMoney(int fromUserId, decimal requestedMoney)
        {
            if (LoggedIn)
            {
                Transfer transfer = new Transfer();
                transfer.AccountFrom = fromUserId;
                transfer.AccountTo = UserService.GetUserId();
                transfer.Amount = requestedMoney;
                transfer.TransferStatusId = 1;
                transfer.TransferTypeId = 1;

                RestRequest request = new RestRequest($"{API_TRANSFER_URL}/requests");
                request.AddJsonBody(transfer);

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<Transfer> response = authClient.Post<Transfer>(request);

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

        public List<Transfer> GetUserTransfers()
        {
            if (LoggedIn)
            {
                RestRequest request = new RestRequest($"{API_TRANSFER_URL}");

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<List<Transfer>> response = authClient.Get<List<Transfer>>(request);

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

        public List<Transfer> GetPendingUserTransfers()
        {
            if (LoggedIn)
            {
                RestRequest request = new RestRequest($"{API_TRANSFER_URL}");

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<List<Transfer>> response = authClient.Get<List<Transfer>>(request);

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

        public Transfer ApproveTransfer(int toUserId, decimal sentMoney)
        {
            if (LoggedIn)
            {
                Transfer transfer = new Transfer();
                transfer.AccountFrom = UserService.GetUserId();
                transfer.AccountTo = toUserId;
                transfer.Amount = sentMoney;
                transfer.TransferStatusId = 2;
                transfer.TransferTypeId = 1;

                RestRequest request = new RestRequest($"{API_TRANSFER_URL}/approved");
                request.AddJsonBody(transfer);

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<Transfer> response = authClient.Post<Transfer>(request);

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

        public Transfer UpdateTransfer(int transferId, int toUserId, decimal requestAmount, int userSelection)
        {
            if (LoggedIn)
            {
                Transfer transfer = new Transfer();
                transfer.TransferId = transferId;
                transfer.AccountFrom = UserService.GetUserId();
                transfer.AccountTo = toUserId;
                transfer.Amount = requestAmount;

                if (userSelection == 1)
                {
                    transfer.TransferStatusId = 2;
                }
                else
                {
                    transfer.TransferStatusId = 3;
                }

                transfer.TransferTypeId = 1;

                RestRequest request = new RestRequest($"{API_TRANSFER_URL}/{transferId}");
                request.AddJsonBody(transfer);
                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<Transfer> response = authClient.Put<Transfer>(request);

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

        //Integrated into one method
        //public Transfer UpdateRejectedTransfer(int transferId, int toUserId, decimal requestAmount)
        //{
        //    if (LoggedIn)
        //    {
        //        Transfer transfer = new Transfer();
        //        transfer.AccountFrom = UserService.GetUserId();
        //        transfer.AccountTo = toUserId;
        //        transfer.Amount = requestAmount;
        //        transfer.TransferStatusId = 3;
        //        transfer.TransferTypeId = 1;

        //        RestRequest request = new RestRequest($"{API_TRANSFER_URL}/{transferId}");
        //        request.AddJsonBody(transfer);
        //        authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //        IRestResponse<Transfer> response = authClient.Put<Transfer>(request);

        //        if (response.ResponseStatus != ResponseStatus.Completed)
        //        {
        //            //response not received
        //            Console.WriteLine("An error occurred communicating with the server.");
        //            return null;
        //        }
        //        else if (!response.IsSuccessful)
        //        {
        //            //response non-2xx
        //            Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
        //            return null;
        //        }
        //        else
        //        {
        //            //success
        //            return response.Data;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("You are not logged in");
        //        return null;
        //    }
        //}

        public TransferDetails GetTransferDetails(int transferId)
        {
            if (LoggedIn)
            {
                RestRequest request = new RestRequest($"{API_TRANSFER_URL}/{transferId}");

                authClient.Authenticator = new JwtAuthenticator(UserService.GetToken());

                IRestResponse<TransferDetails> response = authClient.Get<TransferDetails>(request);

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