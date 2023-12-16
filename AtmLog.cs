using System.Globalization;

static class AtmLog
{
    public static void LogSuccess(string id, string name, string surname)
    {
        string successLog = $"{name} {surname} with {id} user ID, successfully logged in at {DateTime.Now}";
        LogTransaction(id, successLog);
    }

    public static void LogFail(string id, string name, string surname)
    {
        string failLog = $"{name} {surname} with {id} user ID, failed to log in at {DateTime.Now}";
        LogTransaction(id, failLog);
    }
    
    public static void LogWithdraw(string id, string name, string surname, double amount, double balance)
    {
        string withdrawLog = $"{name} {surname} with {id} user ID, withrawn {amount}£ at {DateTime.Now}";
        string[] saveBalance = {$"Name: {name}", $"Surname: {surname}", $"Balance: {balance}"};
        LogBalance(id, saveBalance);
        LogTransaction(id, withdrawLog);
    }

    public static void LogDeposit(string id, string name, string surname, double amount, double balance)
    {
        string depositLog = $"{name} {surname} with {id} user ID, deposited {amount}£ at {DateTime.Now}";
        string[] saveBalance = {$"Name: {name}", $"Surname: {surname}", $"Balance: {balance}"};
        LogBalance(id, saveBalance);
        LogTransaction(id, depositLog);
    }

    public static void LogSent(string senderId, string takerId, string senderName, string takerName, string senderSurname, string takerSurname, double amount, double balance)
    {
        string sentLog = $"Client with {senderId} Id {senderName} {senderSurname}, transferred {amount}£ to client with {takerId} Id {takerName} {takerSurname}";
        string[] senderBalance = {$"Name: {senderName}", $"Surname: {senderSurname}", $"Balance: {balance}"};
        LogBalance(senderId, senderBalance);
        LogTransaction(senderId, sentLog);
    }

    public static void LogTaken(string senderId, string takerId, string senderName, string takerName, string senderSurname, string takerSurname, double amount, double balance)
    {
        string takenLog = $"Client with {senderId} Id {senderName} {senderSurname}, transferred {amount}£ to client with {takerId} Id {takerName} {takerSurname}";
        string[] takerBalance = {$"Name: {takerName}", $"Surname: {takerSurname}", $"Balance: {balance}"};
        LogBalance(takerId, takerBalance);
        LogTransaction(takerId, takenLog);
    }

    public static void ViewBalance(string id, string name, string surname)
    {
        string balanceLog = $"Client with {id} Id {name} {surname}, has been viewed the balance at {DateTime.Now}.";
        LogTransaction(id, balanceLog);
    }

    public static void LogExit(string id, string name, string surname)
    {
        string exitLog = $"Client with {id} Id {name} {surname}, log out at {DateTime.Now}.";
        LogTransaction(id, exitLog);
    }

    public static void CheckFile()
    {
        if (!Directory.Exists(@"C:\AtmApp"))
        {
            Directory.CreateDirectory(@"C:\AtmApp");

            foreach (var item in UserData.User)
            {
                string idLogLocation = $@"C:\AtmApp\{item.UserId} log.txt";
                FileStream idFile = new(idLogLocation, FileMode.OpenOrCreate, FileAccess.Write);

                string balanceLogLocation = $@"C:\AtmApp\{item.UserId} balance.txt";
                FileStream balanceFile = new(balanceLogLocation, FileMode.OpenOrCreate, FileAccess.Write);

                idFile.Close();
                balanceFile.Close();

                string[] userDefaultAccount = {$"Client: {item.UserName} {item.UserSurName}", $"Balance: {0}"};
                LogBalance(item.UserId, userDefaultAccount);
            }
        }

        else
        {
            string fileLocation = @"C:\AtmApp";
            string[] getIdFile = Directory.GetFiles(fileLocation, "*log.txt");
            string[] getBalanceFile = Directory.GetFiles(fileLocation, "*balance.txt");

            for (int i = 0; i < UserData.User.Count; i++)
            {
                string logFileLocation = getIdFile[i];
                string[] getLines = File.ReadAllLines(logFileLocation);
                double balance = Double.Parse(getLines[1]);

                string fileName = getIdFile[i];
                var clientBalance = UserData.User.FirstOrDefault(u=>u.UserId == fileName);

            }
        }
    }

    public static void LogTransaction(string id, string log)
    {
        string idLogLocation = $@"C:\AtmApp\{id} log.txt";
        FileStream idFile = new(idLogLocation, FileMode.OpenOrCreate, FileAccess.Write);
        idFile.Close();
        File.AppendAllText(idLogLocation, Environment.NewLine + log);
    }

    public static void LogBalance(string id, string[] LogBalance)
    {
        File.WriteAllLines($@"C:\AtmApp\{id} balance.txt", LogBalance);
    }
}