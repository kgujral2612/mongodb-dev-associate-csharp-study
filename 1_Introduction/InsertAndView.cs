using MongoDB.Bson;
using MongoDB.Driver;

public class InsertAndView
{
    static InsertAndView()
    {
        Connect();
    }

    private const string MongoConnectionString = "mongodb+srv://kaushambi2612:<db_password>.kmmzz.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
    private static IMongoCollection<Person> peopleCollection;

    private static void Connect()
    {
        Console.WriteLine("Attempting to connect to Atlas cluster...");

        // Connect to the atlas cluster
        try {
            var client = new MongoClient(MongoConnectionString);
            peopleCollection = client.GetDatabase("gettingStarted").GetCollection<Person>("people"); // if the collection does not already exist, it will be implicitly created upon the first write 
            Console.WriteLine("Successfully connected to Atlas");
        } 
        catch (Exception e) { Console.WriteLine(e);}
    }
    public static void Insert()
    {
        Console.WriteLine("Inserting new documents into the collection...");

        var newPeople = new List<Person>() {
            new() {
                Name = new Name { First = "Alan", Last = "Turing" },
                Birth = new DateTime(1912, 5, 23), // May 23, 1912                                                                                                                            
                Death = new DateTime(1954, 5, 7),  // May 7, 1954                                                                                                                                 
                Contribs = ["Turing machine", "Turing test", "Turingery"],
                Views = 1250000,
            },
            new() {
                Name = new Name { First = "Grace", Last = "Hopper" },
                Birth = new DateTime(1906, 12, 9), // Dec 9, 1906                                                                                                                            
                Death = new DateTime(1992, 1, 1),  // Jan 1, 1992                                                                                                                                 
                Contribs = ["Mark I", "UNIVAC", "COBOL"],
                Views = 3860000,
            }
        };
        peopleCollection.InsertMany(newPeople);
        Console.WriteLine("Documents inserted successfully.");
    }

    public static void FindDocument()
    {
        var filter = Builders<Person>.Filter
                                    .Eq(p => p.Name.Last, "Turing");
        var document = peopleCollection.Find(filter).FirstOrDefault();

        Console.WriteLine($"Document found:\n{document.ToBsonDocument()}");
    }
}