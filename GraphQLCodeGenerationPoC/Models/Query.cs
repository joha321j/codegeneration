using System.Runtime.Serialization;

namespace GraphQLCodeGenerationPoC.Models;

public class Query
{
    // One method per Source should be generated
    public Azure FetchFromAzure()
    {
        return new Azure();
    }
}

// One class per source with the containing get methods should be generated
public class Azure
{
    [GraphQLName("metricsResult")]
    public MetricsResult GetMetric()
    {
        return new MetricsResult
        {
            Value = new MetricsResultInfo
            {
                Data = "Lulullulul"
            }
        };
    }
}

[DataContract]
public class MetricsResult
{
    [DataMember(Name="value", EmitDefaultValue=false)]
    public MetricsResultInfo Value { get; set; }
}

[DataContract]
public class MetricsResultInfo
{
    [DataMember(Name="data", EmitDefaultValue=false)]
    public string Data { get; set; }
}


// public class Query
// {
//     [GraphQLType(typeof(User))]
//     [GraphQLName("getUser")]
//     public User GetUser()
//     {
//         // return new { name = "Karsten", title = "Cunt" };
//         return new User("Karsten", "Cunt");
//     }
//     
//     [GraphQLType(typeof(ObjectType))]
//     [GraphQLName("getDepartment")]
//     public object GetDepartment()
//     {
//         return new { name = "Derp" };
//     }
// }
//
// public class User : ObjectType
// {
//     public User(string Name, string Title)
//     {
//         this.Name = Name;
//         this.Title = Title;
//     }
//
//     public string Name { get; init; }
//     public string Title { get; init; }
//     
//     
// }