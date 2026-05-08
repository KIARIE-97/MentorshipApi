using System;

namespace Mentorship.API.Attributes;

public class IdempotentAttribute :Attribute
{
    public string OperationType {get;}
    public  int TtlSeconds {get; set;}

    public IdempotentAttribute(string operationType)
    {
        OperationType = operationType;
    }

}
