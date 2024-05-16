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
        [Column("date", TypeName = "date")]
        public DateTime Day { get; set; }

        [Column("time", TypeName = "time(7)")]
        public TimeOnly Time { get; set; }

        [Column("free")]
        [MaxLength(1)]
        public byte[] Free { get; set; }

        [Required]
        [Column("administrator_id")]
        public long? AdministratorId { get; set; }

        [Required]
        [Column("request_id")]
        public long? RequestId { get; set; }

        [ForeignKey("AdministratorId")]
        [InverseProperty("TimeSlots")]
        public User? Administrator { get; set; } = null!;
        
        //множственная связь
        //[JsonIgnore]
        //[InverseProperty("TimeSlot")]
        //public ICollection<Request>? Requests { get; set; } = new List<Request>();

    
        [InverseProperty("TimeSlot")]
        public Request? Request { get; set; }


    }
}
