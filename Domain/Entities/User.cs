﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Domain.Entities;

[Table("users")]
public partial class User : BaseEntity
{
    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("pass")]
    [StringLength(255)]
    [Unicode(false)]
    public string Pass { get; set; } = null!;

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("surname")]
    [StringLength(255)]
    [Unicode(false)]
    public string Surname { get; set; } = null!;

    [Column("patronymic")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Patronymic { get; set; }

    [Column("phone")]
    [StringLength(16)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [Column("gender")]
    public short Gender { get; set; }

    [Column("admin")]
    [MaxLength(1)]
    public byte[] Admin { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Administrator")]
    public  ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [JsonIgnore]
    [InverseProperty("User")]
    public Request Request { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Administrator")]
    public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();

    [JsonIgnore]
    [InverseProperty("Administrator")]
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public override string ToString()
    {
        return $"{Surname} {Name} {Patronymic} - {(Admin[0] == 0? "СТУДЕНТ": "АДМИНИСТРАТОР")}," +
            $" id = {Id}, phone = {Phone}, email = {Email}, pass = {Pass}";
    }
}
