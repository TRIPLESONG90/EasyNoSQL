# EasyNoSQL

## How to use

A quick example

```C#
// Create your model
public class Model : ModelBase
{
    public string Name { get; set; }
}

//create collection instance
ICollection<Model> collection;

bool isMongoDB = false;

//MongoDB
if (isMongoDB)
{
    string dbname = @"dbname";
    string collectionName = "models";
    string connString = "mongodb://localhost";
    collection = new MongoDBCollection<Model>(connString, dbname, collectionName);
}

//LiteDB
else
{
    string dbname = @"dbname.db";
    string collectionName = "models";
    collection = new LiteDBCollection<Model>(dbname, collectionName);
}

```
