using NISApi.Data;
using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Contracts
{
    public interface ITableCollectionManager : IRepository<TableCollection>
    {
        (IEnumerable<TableCollection> Collections, Pagination Pagination) GetCollections(UrlQueryParameters urlQueryParameters);
    }
}
