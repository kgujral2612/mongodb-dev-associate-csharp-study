using MongoDB.Bson;

public class Items{
    public ObjectId Id { get; set; }
    public string item { get; set; }
    public int qty { get; set; }
    public Size size { get; set; }
    public string status { get; set; }
}

public class Size{
    public double h { get; set; }
    public double w { get; set; }
    public string uom { get; set; }
}