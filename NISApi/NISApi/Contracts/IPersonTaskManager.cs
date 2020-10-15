using NISApi.Data.Entity.SystemTables;
using NISApi.Data.Entity.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Contracts
{
    public interface IPersonTaskManager : IRepository<PersonTask>
    {
        IEnumerable<PersonTask> GetPersonTasks();
        Task<IEnumerable<PersonTask>> GetByUserAsync(string userId);
    }
}