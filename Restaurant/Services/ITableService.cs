﻿using Restaurant.Database.Models;
using System.Collections.Generic;

namespace Restaurant.Services
{
    public interface ITableService
    {
        IEnumerable<Table> GetAllTables();
    }
}
