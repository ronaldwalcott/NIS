﻿using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Contracts
{
    public interface ITableStreetManager : IRepository<TableStreet>
    {
        IEnumerable<TableStreet> GetStreets();
    }
}
