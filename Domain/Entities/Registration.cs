using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("registrations")]
public partial class Registration:BaseEntity
{
    [Required]
    [Column("request_id")]
    public long RequestId { get; set; }

    [Required]
    [Column("administrator_id")]
    public long AdministratorId { get; set; }

    [Required]
    [Column("room_id")]
    public long RoomId { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [ForeignKey("AdministratorId")]
    [InverseProperty("Registrations")]
    public  User Administrator { get; set; } = null!;

    [ForeignKey("RequestId")]
    [InverseProperty("Registrations")]
    public  Request Request { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("Registrations")]
    public  Room Room { get; set; } = null!;

    public override string ToString()
    {
        return $"reg id {Id} - admin {Administrator} - room {Room} - requser {Request}";
    }
}
