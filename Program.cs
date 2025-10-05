using App; 


// Lista med användare. Här lägger vi in ett testkonto för att enkelt kunna logga in
List<User> users = new List<User>();
users.Add(new User("test", "123"));

// Lista med alla items som alla användare lägger upp
List<Item> items = new List<Item>(); // Tom i början. Fylls på när användare lägger upp varor
//items.Add(new Item("Svärd")); // Använde för test

// Variabeln för den användare som är inloggad just nu
// null betyder att ingen är inloggad
User? active_user = null;

// Styr huvudloopen
bool running = true;


// Huvudloop, programmet körs tills running blir false
while (running)
{
    Console.Clear(); 

    // Om ingen är inloggad: visa startmenyn (login/register/exit)
    if (active_user == null)
    {
        Console.WriteLine("Trading System - Menu");
        Console.WriteLine("[Inte inloggad]");
        Console.WriteLine("");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
        string meny = Console.ReadLine(); 

        switch (meny)
        {
            case "1": // Login
                Console.Clear();
                Console.WriteLine("Trading System - Login menu");
                Console.WriteLine("");
                Console.Write("Username: ");
                string username = Console.ReadLine(); 
                Console.Write("Password: ");
                string password = Console.ReadLine(); 

                // Försöker hitta en user i listan som matchar username + password
                foreach (User u in users)
                {
                    if (u.TryLogin(username, password))
                    {
                        active_user = u; // Om träff: sätt aktuell inloggad användare
                        break;           // och lämna loopen
                    }
                }

                // Om ingen match hittades: visa felmeddelande
                if (active_user == null)
                {
                    Console.WriteLine("Fel användarnamn eller lösenord. Tryck Enter för att fortsätta...");
                    Console.ReadLine();
                }
            break;

            case "2": // Register (skapa konto)
                Console.Clear();
                Console.WriteLine("Trading System - Register Menu");
                Console.Write("Choose username: ");
                string newUsername = Console.ReadLine(); // Nytt användarnamn

                // Kollar om användarnamnet redan finns
                bool exists = false;
                foreach (User u in users)
                {
                    if (u.GetUsername() == newUsername)
                    {
                        exists = true; // Hittade en dubblett
                        break;
                    }
                }

                if (exists)
                {
                    Console.WriteLine("Användarnamnet är upptaget. Tryck Enter för att fortsätta...");
                    Console.ReadLine();
                    break; // Tillbaka till menyn (ingen registrering)
                }

                Console.Write("Choose password: ");
                string newPassword = Console.ReadLine(); // Nytt lösenord

                // Skapa och lägg till användaren i listan
                var newUser = new User(newUsername, newPassword);
                users.Add(newUser);

                // Auto-login: gör den nya användaren till aktiv/inloggad
                active_user = newUser;
        
            break;

            case "3": // Exit (avsluta programmet)
                running = false; // Bryter huvudloopen
            break;

            default: // Ogiltigt menyval
                Console.WriteLine("Okänt kommando. Tryck Enter för att fortsätta...");
                Console.ReadLine();
            break;
        }
    }
    // Annars: någon är inloggad → visa inloggad meny 
    else
    {
        Console.WriteLine("Trading System - Meny");
        Console.WriteLine("Logged in as: " + active_user.GetUsername());
        Console.WriteLine("");
        Console.WriteLine("1. Add item");           // Lägg upp en ny vara
        Console.WriteLine("2. See my items");       // Visa mina varor
        Console.WriteLine("3. See tradable items"); // Visa andras varor (sånt jag kan byta mot)
        Console.WriteLine("0. Logout");             // Logga ut (tillbaka till startmeny)
        string input = Console.ReadLine();          // Läs menyval

        switch (input)
        {
            case "1": // Lägg till vara
                Console.Clear();
                Console.WriteLine("Add item");
                Console.WriteLine("");
                Console.Write("Item Name: ");
                string itemName = Console.ReadLine(); // Namn på varan
                Console.Write("Description: ");
                string itemInfo = Console.ReadLine(); // Beskrivning av varan

                // Tar nästa användar-id från den inloggade användaren
                int newId = active_user.GetNextItemId();

                // Skapa item-objektet
                Item item = new Item
                (
                    newId,                         // Id är unikt per användare
                    active_user.GetUsername(),     // Ägare = den inloggade
                    itemName,
                    itemInfo
                );

                // Lägg in item i den globala listan över alla items
                items.Add(item);

                
            break;

            case "2": // Visa mina varor
                Console.Clear();
                Console.WriteLine("My items");
                Console.WriteLine("");
                bool anyMine = false; // Flagga för om man hittade några

                // Går igenom alla items och skriv ut de som ägs av den inloggade
                foreach (Item it in items)
                {
                    if (it.OwnerUsername == active_user.GetUsername())
                    {
                        Console.WriteLine(it.Id + " | " + it.ItemName + " - " + it.ItemInfo);
                        anyMine = true;
                    }
                }

                // Om inga egna varor fanns
                if (!anyMine)
                {
                    Console.WriteLine("(no items yet)");
                }
                Console.WriteLine("");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine(); // Går tillbaka till menyn
            break;

            case "3": // Visa andras varor
                Console.Clear();
                Console.WriteLine("Tradable items - Choose an item you would like to trade");
                Console.WriteLine("");

                bool anyOthers = false; // Flagga för om det fanns några

                // Gå igenom alla items och skriv ut de som ägs av någon annan användare
                foreach (Item it in items)
                {
                    if (it.OwnerUsername != active_user.GetUsername())
                    {
                        Console.WriteLine(it.Id + " | " + it.ItemName + " - " + it.ItemInfo + " (owner: " + it.OwnerUsername + ")");
                        anyOthers = true;
                    }
                }

                // Om inga andras varor fanns
                if (!anyOthers)
                {
                    Console.WriteLine("(no tradable items right now)");
                }
                Console.WriteLine("");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine(); // Tillbaks till menyn
            break;

            case "0": // Logout
                active_user = null; // Sätt inloggad användare till null → tillbaka till startmeny
            break;

            default: // Ogiltigt menyval
                Console.WriteLine("Okänt kommando. Tryck Enter för att fortsätta...");
                Console.ReadLine();
            break;
        }
    }
}
