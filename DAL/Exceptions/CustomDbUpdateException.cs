using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class CustomDbUpdateException : CustomException
    {
        public CustomDbUpdateException() { }
        public CustomDbUpdateException(string message) : base(message) { }
        public CustomDbUpdateException(string message, DbUpdateException innerException) : base(message, innerException) { }
    }
}
