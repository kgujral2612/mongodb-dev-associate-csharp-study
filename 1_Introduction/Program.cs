using MongoDB.Bson;
using MongoDB.Driver;

public class Connect
{            
  // Update db password in the connection string
  private const string MongoConnectionString = "mongodb+srv://kaushambi2612:<db_password>@cluster0.kmmzz.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

  public static void Main(string[] args)
  {
    Console.WriteLine("Attempting to connect to Atlas cluster...");

    // Connect to your Atlas cluster via a MongoClient
    var client = new MongoClient(MongoConnectionString);

    // Send a ping to confirm a successful connection
    try {
        var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        Console.WriteLine("Successfully connected to Atlas");
    } 
    catch (Exception e) { Console.WriteLine(e);}
  }
}
