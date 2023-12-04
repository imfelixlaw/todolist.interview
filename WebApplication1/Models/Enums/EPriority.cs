using System.Runtime.Serialization;

namespace WebApplication1.Models.Enums;


[DataContract]
public enum EPriority
{
    [EnumMember]
    Normal = 0,

    [EnumMember]
    Low = 1,

    [EnumMember]
    High = 2,
}
