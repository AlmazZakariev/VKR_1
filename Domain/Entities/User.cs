using System;
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

    [Column("admin")]
    [MaxLength(1)]
    public byte[] Admin { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("Administrator")]
    public  ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [JsonIgnore]
    [InverseProperty("User")]
    public  ICollection<Request> Requests { get; set; } = new List<Request>();

    public override string ToString()
    {
        return $"{Surname} {Name} {Patronymic} is admin = {Admin[0] == '1'}, id = {Id}";
    }
}
