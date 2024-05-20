using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("requests")]
public partial class Request:BaseEntity
{

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("preference_date", TypeName = "datetime")]
    public DateTime? PreferenceDate { get; set; }

    [Required]
    [Column("user_id")]
    public long UserId { get; set; }

    [Required]
    [Column("time_slot_id")]
    public long TimeSlotId { get; set; }

    [JsonIgnore]
    [InverseProperty("Request")]
    public  Registration? Registration { get; set; } 

    [ForeignKey("UserId")]
    [InverseProperty("Request")]
    public User User { get; set; } = null!;

    [ForeignKey("TimeSlotId")]
    [InverseProperty("Request")]
    public TimeSlot TimeSlot { get; set; } = null!;

    public override string ToString()
    {
        return $"requset from {User} at {Date}, preference date is {PreferenceDate}";
    }
}
