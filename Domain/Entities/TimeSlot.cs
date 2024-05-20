using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("time_slots")]
    public partial class TimeSlot:BaseEntity
    {
        [Column("date", TypeName = "dateTime")]
        public DateTime Date { get; set; }

        [Column("free")]
        [MaxLength(1)]
        public byte[] Free { get; set; }

        [Required]
        [Column("administrator_id")]
        public long? AdministratorId { get; set; }

        [ForeignKey("AdministratorId")]
        [InverseProperty("TimeSlots")]
        public User? Administrator { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("TimeSlot")]
        public Request? Request { get; set; } = null!;

    }
}
