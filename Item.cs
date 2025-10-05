namespace App;


public class Item 
{
    // Ett id-nummer för varan.
    public int Id;

    // Vilken användare som äger varan 
    public string OwnerUsername;

    // Namnet/titeln på varan
    public string ItemName;

    // Beskrivningstext om varan
    public string ItemInfo;

    // Körs när man skapar en ny Item
    public Item(int id, string ownerUsername, string itemName, string itemInfo)
    {
        Id = id;
        OwnerUsername = ownerUsername;
        ItemName = itemName;
        ItemInfo = itemInfo;
    }
}
