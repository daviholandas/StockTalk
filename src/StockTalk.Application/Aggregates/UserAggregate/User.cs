using StockTalk.Application.Common;

namespace StockTalk.Application.Aggregates.UserAggregate;

public class User : Entity
{
    public User(string name,
        string nickName,
        string password)
    {
        Name = name;
        NickName = nickName;
    }
    
    private User(){}

    public string Name { get; init; }
    public string NickName { get; private set; }
}