namespace App;

// Klassen User representerar en användare i systemet
public class User
{
    
    public string Username;

    // Lösenord hålls privat
    string _password; 

    // Varje användare har sin egen räknare för item-id, startar på 1
    int nextItemId = 1;

    // Konstruktor: körs när man skapar en ny User
    public User(string username, string password)
    {
        Username = username;
        _password = password;
    }

    // Jämför inmatat namn/lösen med detta objektets värden
    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }

    // Hämtar användarnamnet
    public string GetUsername()
    {
        return Username;
    }

    // Ger nästa item-id för just denna användare och ökar räknaren till nästa gång
    public int GetNextItemId()
    {
        int current = nextItemId;   // Spara nuvarande värde
        nextItemId = nextItemId + 1; // Öka till nästa
        return current;             // Returnera det man ska använda nu
    }
}
