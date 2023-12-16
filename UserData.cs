public class UserData
{
    private static List<User> _user;

    static UserData()
    {
        _user = new()
        {
            new User{UserId = "00000000001", UserName = "Gonca", UserSurName = "Kaya", UserPass = "2000", Balance = 5000},
            new User{UserId = "00000000002", UserName = "Metin", UserSurName = "Topaloglu", UserPass = "2001", Balance = 10000},
            new User{UserId = "00000000003", UserName = "Ahu", UserSurName = "Kutay", UserPass = "2002", Balance = 7500},
            new User{UserId = "00000000004", UserName = "Deniz", UserSurName = "Behçet", UserPass = "2003", Balance = 9000},
            new User{UserId = "00000000005", UserName = "Peri", UserSurName = "Togay", UserPass = "2004", Balance = 8700},
            new User{UserId = "00000000006", UserName = "Arda", UserSurName = "Gür", UserPass = "2005", Balance = 12000}
        };
    }

    public static List<User> User => _user;
}