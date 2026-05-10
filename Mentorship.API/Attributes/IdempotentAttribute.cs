using System;

namespace Mentorship.API.Attributes;

public class IdempotentAttribute :Attribute
{
    public string OperationType {get;}
    public  int TtlSeconds {get; set;} = 86400;

    public IdempotentAttribute(string operationType)
    {
        OperationType = operationType;
    }

}
