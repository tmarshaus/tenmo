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
            Console.WriteLine("Not yet implemented!");
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

            //Use SendMoney method 
            apiService.SendMoney(toUserId);

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
