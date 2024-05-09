using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //[Timestamp]
        //public byte[]? TimeStamp { get; set; }
    }
}
