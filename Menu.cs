using System.Net.Http.Headers;

public class Menu
{
    public void LogIn()
    {
    tryLogIn:

        Console.WriteLine("Please enter your UserId: ");
        string idNum = Console.ReadLine();
        Console.WriteLine("Please enter password: ");
        string userPassword = Console.ReadLine();

        var newUser = UserData.User.FirstOrDefault(u => u.UserId == idNum);
        AtmLog.CheckFile();

        if (newUser != null && newUser.UserPass == userPassword)
        {
            AtmLog.LogSuccess(newUser.UserId, newUser.UserName, newUser.UserSurName);
        tryMenu:
            Console.Clear();
            Console.WriteLine($"Welcome {newUser.UserName} {newUser.UserSurName}. Please select an option from the list: ");
            Console.WriteLine("\t1: Withdrawal \n\t2: Deposit \n\t3: Transfer \n\t4: Balance Inquiry \n\t5: Log Out");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    Transaction.WithDraw(newUser.UserId);
                    break;
                case "2":
                    Transaction.Deposit(newUser.UserId);
                    break;
                case "3":
                    Transaction.Transfer(newUser.UserId);
                    break;
                case "4":
                    Transaction.ViewBalance(newUser.UserId, newUser.Balance);
                    break;
                case "5":
                    Transaction.LogOut(newUser.UserId, newUser.UserName, newUser.UserSurName);
                    break;
                default:
                    Console.WriteLine("Wrong Input. Please enter a button.");
                    Console.ReadKey();
                    break;
            }
            goto tryMenu;
        }
        else
        {
            Console.Clear();
            if (newUser == null)
            {

            }
            else if (newUser.UserId != null && newUser.UserPass != userPassword)
            {
                AtmLog.CheckFile();
                AtmLog.LogFail(newUser.UserId, newUser.UserName, newUser.UserSurName);
            }
            Console.WriteLine("Incorrect User Id or Password.");

            goto tryLogIn;
        }
    }
}