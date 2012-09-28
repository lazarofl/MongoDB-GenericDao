#MongoDB GenericDao
###C# implementation to GenericDao pattern using MongoDB Driver and Linq support

##What methods are supported?
- T GetByID(ID id);
- IEnumerable<T> GetAll();
- T GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition);
- IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition);
- IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition, int maxresult, bool orderByDescending);
- T Save(T pobject);
- void Delete(T pobject);

#How to use?
Each class must have an inheritance from MongoDBGenericDao.MongoDBEntity class

##Create a class

```csharp
public class Message : MongoDBEntity
{
    public DateTime DateCreated { get; set; }
    public string From { get; set; }
    public List<string> To { get; set; }
    public string Content { get; set; }
}
```

##Create a business class
```csharp
public interface IBMessage : IDao<Message, string>
{
	Message AddNewMessage(string from, List<string> to, string messagetext);
}

public class BMessage : MongoDBGenericDao<Message>, IBMessage
{
    public BActivity(string mongodbconnectionstring) : base(mongodbconnectionstring) { }

    public Message AddNewMessage(string from, List<string> to, string messagetext)
    {
    	if(to == null || to.Count == 0)
            throw new ApplicationException("'to' cannot be empty");

    	var message = new Message
    		{
    			DateCreated = DateTime.Now.ToUniversalTime(), //ToUniversalTime() provides datetime serialization
    			From = from,
    			To = to
    		};

    	return this.Save(message); //saves in mongodb and return updated reference
    }
}
```

##Usage

```csharp
IBMessage bmessage = new BMessage("[your connectionstring here. ex: server=appserver-db;database=mymongodbdatabasename]");

var message = bmessage.AddNewMessage("mongodb@mongodb.com",new List<string>().Add("nononono@nonono.com"),"Message content nonononononononono");
var messageid = message.Id;


var top50MessagesThatContainsHelloWordMessage = bmessage.GetAll(x=>x.Content.Contains("hello world"), 50, true);

//remove the first message from top50MessagesThatContainsHelloWordMessage
bmessage.Delete(top50MessagesThatContainsHelloWordMessage.First());
```