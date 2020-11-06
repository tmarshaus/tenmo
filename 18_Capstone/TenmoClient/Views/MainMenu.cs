using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private APIService apiService = new APIService();

        public MainMenu()
        {
            AddOption("View your current balance", ViewBalance)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View your pending requests", ViewRequests)
                .AddOption("Send TE bucks", SendTEBucks)
                .AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            Console.WriteLine($"Your current account balance is: {apiService.GetAccount().Balance}");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            List<List<Transfer>> userTransfers = apiService.GetUserTransfers();

            foreach (Transfer tran in userTransfers[0])
            {
                if (tran.AccountFrom == UserService.GetUserId())
                {
                    Console.WriteLine($"Id:{tran.TransferId}         To:{tran.UsernameTo}         Amount:{tran.Amount}");
                }
            }
            foreach (Transfer tran in userTransfers[1])
            {
                if (tran.AccountTo == UserService.GetUserId())
                {
                    Console.WriteLine($"Id:{tran.TransferId}         From:{tran.UsernameFrom}         Amount:{tran.Amount}");
                }
            }

            Console.WriteLine("Please enter transfer ID to view more details: ");

            Int32.TryParse(Console.ReadLine(), out int toTransferId);
            TransferDetails details = apiService.GetTransferDetails(toTransferId);

            Console.WriteLine($"Id: {details.Id} Account from:{details.From} Account to:{details.To} Type:{details.Type} Status:{details.Status} Amount:{details.Amount}");

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            //Get list of all users and return them
            List<User> users = apiService.GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"User ID: {user.UserId}     Username: {user.Username}");
            }
            //Ask for user input of ID
            Console.WriteLine("Please input the User ID you would like to send TE Bucks: ");

            //Read user input
            Int32.TryParse(Console.ReadLine(), out int toUserId);

            //Ask for user input of money needing to be sent
            Console.WriteLine("How much money would youlike to send?");

            //Read input
            Decimal.TryParse(Console.ReadLine(), out decimal moneySent);

            //Use SendMoney method
            apiService.SendMoney(toUserId, moneySent);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult RequestTEBucks()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new API_User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}