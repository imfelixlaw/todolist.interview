using System.Runtime.Serialization;

namespace WebApplication1.Models.Enums;

[DataContract]
public enum ESortBy
{
    [EnumMember]
    Name = 0,

    [EnumMember]
    Status = 1,

    [EnumMember]
    DueDate = 2,
}
