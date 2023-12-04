using System.Runtime.Serialization;

namespace WebApplication1.Models.Enums;

[DataContract]
public enum EStatus
{
    [EnumMember]
    NotStarted = 0,

    [EnumMember]
    Inprogress = 1,

    [EnumMember]
    Completed = 2,
}
