using MongoDB.Bson;

public class Person
{
    public ObjectId Id { get; set; }
    public Name Name { get; set; }
    public DateTime Birth { get; set; }
    public DateTime Death { get; set; }
    public string[] Contribs { get; set; }
    public int Views { get; set; }
}
public class Name
{
    public string First { get; set; }
    public string Last { get; set; }
}