
static class Transaction
{
    public static void WithDraw(string userId)
    {
        Console.WriteLine("Please enter amount to withdraw: ");
        double amount = Double.Parse(Console.ReadLine());
        var client = UserData.User.FirstOrDefault(c=> c.UserId == userId);

        if ((client.Balance - amount) < 0)
        {
            Console.Clear();
            Console.WriteLine("Insufficient funds. Push a button to continue...");
            Console.ReadKey();
        }
        else
        {
            client.Balance -= amount;
            Console.WriteLine($"{amount}£ were successfully withrawn.");
            AtmLog.LogWithdraw(client.UserId, client.UserName, client.UserSurName, amount, client.Balance);
            ViewBalance(client.UserId, client.Balance);
        }
    }

    public static void Deposit(string userId)
    {
        Console.WriteLine("Please enter the amount to deposit: ");
        double amount = Double.Parse(Console.ReadLine());

        var client = UserData.User.FirstOrDefault(c=> c.UserId == userId);

        client.Balance += amount;
        Console.WriteLine("Deposit was successful");
        AtmLog.LogDeposit(client.UserId, client.UserName, client.UserSurName, amount, client.Balance);
        ViewBalance(client.UserId, client.Balance);

    }

    public static void Transfer(string userId)
    {
        Console.WriteLine("Please enter User Id to Transfer Money: ");
        string transferId = Console.ReadLine();

        var moneyTaker = UserData.User.FirstOrDefault(t=> t.UserId == transferId);
        var moneySender = UserData.User.FirstOrDefault(s=> s.UserId == userId);

        if (moneyTaker != null)
        {
            Console.WriteLine("Please enter amount to transfer: ");
            double amount = Double.Parse(Console.ReadLine());

            if ((moneySender.Balance - amount) < 0)
            {
                Console.Clear();
                Console.WriteLine("Insufficient funds!");
            }
            else
            {
                moneySender.Balance -= amount;
                moneyTaker.Balance += amount;
                Console.WriteLine($"{amount}£ sent to {moneyTaker.UserName} {moneyTaker.UserSurName}.");
                ViewBalance(moneySender.UserId, moneySender.Balance);
                AtmLog.LogSent(moneySender.UserId, moneyTaker.UserId, moneySender.UserName, moneyTaker.UserName, moneySender.UserSurName, moneyTaker.UserSurName, amount, moneySender.Balance);
                AtmLog.LogSent(moneySender.UserId, moneyTaker.UserId, moneySender.UserName, moneyTaker.UserName, moneySender.UserSurName, moneyTaker.UserSurName, amount, moneyTaker.Balance);
            }
        }
        else
        {
            Console.WriteLine("Account not found!");
        }
    }

    public static void ViewBalance(string userId, double balance)
    {
        var customer = UserData.User.FirstOrDefault(c=>c.UserId == userId);
        Console.WriteLine($"Account: {customer.UserName} {customer.UserSurName} \nBalance: {customer.Balance}");
        AtmLog.ViewBalance(customer.UserId, customer.UserName, customer.UserSurName);
        FinishTransaction(customer.UserId, customer.UserName, customer.UserSurName);
    }

    public static void LogOut(string userId, string userName, string userSurName)
    {
        AtmLog.LogExit(userId, userName, userSurName);
        Environment.Exit(0);
    }

    public static void FinishTransaction(string userId, string userName, string userSurName)
    {
        tryFinish:
            Console.WriteLine("\t1: Exit \n\t2: Main Menu");
            string option = Console.ReadLine();
            
            switch (option)
            {
                case "1":
                    LogOut(userId, userName, userSurName);
                    break;   

                case "2":
                    break;  

                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    goto tryFinish;
            }
    }
}