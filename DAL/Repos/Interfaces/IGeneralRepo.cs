using DAL.Repos.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Interfaces
{
    public interface IGeneralRepo : IRepo<General>
    {
        ValueTask<General?> FindSingle();
    }
}
