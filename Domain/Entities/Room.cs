using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("rooms")]
public partial class Room:BaseEntity
{

    [Column("floor")]
    public short Floor { get; set; }

    [Column("number")]
    public short Number { get; set; }

    [Column("add_number")]
    public short? AddNumber { get; set; }

    [Column("gender")]
    public short? Gender { get; set; }

    [Column("free_slots")]
    public short FreeSlots { get; set; }

    [Required]
    [Column("administrator_id")]
    public long AdministratorId { get; set; }

    [ForeignKey("AdministratorId")]
    [InverseProperty("Rooms")]
    public User Administrator { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Room")]
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public override string ToString()
    {
        return $"Room{Floor}{Number}-{AddNumber} with gender = {Gender} have {FreeSlots} free slots";
    }
}
