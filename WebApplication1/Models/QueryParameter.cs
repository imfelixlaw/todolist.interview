using System.Runtime.Serialization;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models;

[DataContract]
public class QueryParameter
{
    [DataMember]
    public ESortBy? SortBy { get; set; } //= ESortBy.Name;

    [DataMember]
    public bool? IsSortAsc { get; set; } //= true;

    [DataMember]
    public EStatus[]? Statuses { get; set; } //= null;

    [DataMember]
    public DateTime[]? DateTimes { get; set; } //= null;
}
