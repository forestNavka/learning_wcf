using System.Runtime.Serialization;

namespace NorthwindService.DTOs
{
    [DataContract]
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
