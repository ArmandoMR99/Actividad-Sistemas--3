[System.Serializable]
public class UserData
{
    public string username;
    public string password;

    public UserData(string u, string p)
    {
        username = u;
        password = p;
    }
}