﻿using DAL.Repos.Base;
using Domain.Entities;

namespace DAL.Repos.Interfaces
{
    public interface IRequestRepo:IRepo<Request>
    {
        //public Request GetRequestByUser(User user);
        ValueTask<int> AddAsync(Request entity, DateTime? date = null, bool persist = true);
    }
}
