using MongoDB.Bson;
using MongoDB.Driver;

public class QueryDocuments{

    static QueryDocuments()
    {
        Connect();
    }

    private const string MongoConnectionString = "mongodb+srv://kaushambi2612:jMeb2cCw7foawWdY@cluster0.kmmzz.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
    private static IMongoCollection<Items> inventoryCollection;
    
    public static void Connect(){
        Console.WriteLine("Attempting to connect to Atlas cluster...");

        // Connect to the atlas cluster
        try {
            var client = new MongoClient(MongoConnectionString);
            inventoryCollection = client.GetDatabase("gettingStarted").GetCollection<Items>("inventory");
            Console.WriteLine("Successfully connected to Atlas");
        } 
        catch (Exception e) { Console.WriteLine(e);}
    }

    public static void InsertDocs()
    {
        var documents = new List<Items>
        {
            new()
            {
                item = "journal",
                qty = 25,
                size = new Size { h = 14, w = 21, uom = "cm"},
                status = "A"
            },
            new()
            {
                item = "notebook",
                qty = 50,
                size = new Size { h = 8.5, w = 11, uom = "in"},
                status = "A"
            },
            new()
            {
                item = "paper",
                qty = 100,
                size = new Size { h = 8.5, w = 11, uom = "in"},
                status = "A"
            },
            new()
            {
                item = "planner",
                qty = 75,
                size = new Size { h = 22.85, w = 30, uom = "cm"},
                status = "A"
            },
            new()
            {
                item = "postcard",
                qty = 45,
                size = new Size { h = 10, w = 15.75, uom = "cm"},
                status = "A"
            }
        };

        inventoryCollection.InsertMany(documents);
    }

    public static void SelectAll(){
        var filter = Builders<Items>.Filter.Empty;
        var documents = inventoryCollection.Find(filter).ToList();
        PrintDocuments(documents);
    }

    public static void SelectWithItemName() {
        var filter = Builders<Items>.Filter.Regex(x => x.item, "^p");
        var documents = inventoryCollection.Find(filter).ToList();
        Console.WriteLine($"Items where name starts with 'P': \n");
        PrintDocuments(documents);
    }

    public static void SelectWithQty()
    {
        var filter = Builders<Items>.Filter.Gte(x => x.qty, 75);
        var documents = inventoryCollection.Find(filter).ToList();
        Console.WriteLine($"Items with quantity greater than equal to 75: \n");
        PrintDocuments(documents);
    }

    public static void SelectWithUOM()
    {
        var filter = Builders<Items>.Filter.Eq(x => x.size.uom, "cm");
        var documents = inventoryCollection.Find(filter).ToList();
        Console.WriteLine($"Items with UOM = cm:");
        PrintDocuments(documents);
    }
    public static void SelectWithStatus()
    {
        var filter = Builders<Items>.Filter.Eq(x=>x.status, "A");
        var documents = inventoryCollection.Find(filter).ToList();
        Console.WriteLine($"Items with status A:");
        PrintDocuments(documents);
    }

    private static void PrintDocuments(List<Items> documents)
    {
        if (documents == null)
            return;

        foreach (var doc in documents)
        {
            Console.WriteLine(doc.ToBsonDocument());
        }
    }
}