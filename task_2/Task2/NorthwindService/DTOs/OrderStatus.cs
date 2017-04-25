using System.Runtime.Serialization;

namespace NorthwindService.DTOs
{
    [DataContract(Namespace = "http://epm.com/Northwind")]
    public enum OrderStatus
    {
        [EnumMember]
        New,

        [EnumMember]
        InProgress,

        [EnumMember]
        Completed
    }
}
