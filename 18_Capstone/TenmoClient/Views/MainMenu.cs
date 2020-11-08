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
            Console.WriteLine($"Your current account balance is: {apiService.GetAccount().Balance:C} \n\n\rPress any key to return to the Main Menu");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            List<Transfer> userTransfers = apiService.GetUserTransfers();
            Console.WriteLine("Transfer History");
            Console.WriteLine("-------------------------------------------");
            foreach (Transfer tran in userTransfers)
            {
                if (tran.AccountFrom == UserService.GetUserId())
                {
                    Console.WriteLine($"Transfer ID:{tran.TransferId}\tTo:{tran.UsernameTo}\tAmount:{tran.Amount:C}");
                }
                else
                {
                    Console.WriteLine($"Transfer ID:{tran.TransferId}\tFrom:{tran.UsernameFrom}\tAmount:{tran.Amount:C}");
                }
            }
            Console.WriteLine("-------------------------------------------");
            Console.Write("Please enter Transfer ID to view more details: ");

            Int32.TryParse(Console.ReadLine(), out int toTransferId);
            TransferDetails details = apiService.GetTransferDetails(toTransferId);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("-------------------------------------------\n");
            Console.WriteLine($"Id: {details.Id}");
            Console.WriteLine($"Account from: {details.From}");
            Console.WriteLine($"Account to: {details.To}");
            Console.WriteLine($"Type: {details.Type}");
            Console.WriteLine($"Status: {details.Status}");
            Console.WriteLine($"Amount: {details.Amount:C}\n");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Please press any key to return to the Main Menu");
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

            Console.WriteLine("Send TE Bucks");
            Console.WriteLine("-------------------------------------------");
            foreach (User user in users)
            {
                if (user.UserId != UserService.GetUserId())
                {
                    Console.WriteLine($"User ID: {user.UserId} \t Username: {user.Username}");
                }
            }
            Console.WriteLine("-------------------------------------------");

            //Ask for user input of ID
            Console.Write("Please input the User ID for whom you are sending TE Bucks: ");

            //Read user input
            Int32.TryParse(Console.ReadLine(), out int toUserId);

            //Ask for user input of amount to send
            Console.Write("Please input the amount of money you are sending: ");

            //Read input
            Decimal.TryParse(Console.ReadLine(), out decimal moneySent);

            Console.WriteLine("-------------------------------------------");

            //Check balance to determine whether or not transaction is approved 
            if (apiService.GetAccount().Balance >= moneySent)
            {
                //Use SendMoney method
                apiService.SendMoney(toUserId, moneySent);
                Console.WriteLine("Send TE Bucks transaction is approved! \n");
            }
            else
            {
                Console.WriteLine("Send TE Bucks Transaction Denied: Insufficient Funds \n");
            }

            Console.WriteLine("Press any key to return to main menu");

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