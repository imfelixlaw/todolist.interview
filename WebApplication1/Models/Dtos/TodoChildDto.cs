using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Dtos
{
    public class TodoChildDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(450)]
        public string UserId { get; set; }

        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(15), MaxLength(255)]
        public string? Description { get; set; }

        public DateTime? DuetoDateTime { get; set; }

        public EStatus Status { get; set; }

        public string? Tags { get; set; }

        public EPriority Priority { get; set; }
    }

    public class TodoChildResponseDto : TodoChildDto
    {
        public string StatusDesc { get => Enum.GetName(Status); }

        public string PriorityDesc { get => Enum.GetName(Priority); }
    }
}
