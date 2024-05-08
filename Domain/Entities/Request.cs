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

    [JsonIgnore]
    [InverseProperty("Request")]
    public  ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [ForeignKey("UserId")]
    [InverseProperty("Requests")]
    public  User User { get; set; } = null!;
    public override string ToString()
    {
        return $"requset from {User} at {Date}, preference date is {PreferenceDate}";
    }
}
