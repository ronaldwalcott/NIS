using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NISApi.Constants;
using NISApi.Contracts;
using NISApi.Data.Entity.SystemTables;
using NISApi.Data.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.DataManager
{
    public class TableTaskPriorityManager : ITableTaskPriorityManager
    {
        private readonly ILogger<TableTaskPriorityManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableTaskPriorityManager(NisDbContext dbContext, ILogger<TableTaskPriorityManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableTaskPriority> GetTaskPriorities()
        {
            IEnumerable<TableTaskPriority> priorities;

            priorities = _context.TableTaskPriorities.AsQueryable();


            return priorities;
        }

        public async Task<IEnumerable<TableTaskPriority>> GetAllAsync()
        {
            return await _context.TableTaskPriorities.ToListAsync();
        }

        public async Task<TableTaskPriority> GetByIdAsync(object id)
        {
            return await _context.TableTaskPriorities.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableTaskPriority priority)
        {
            var priorityAdd = new TableTaskPriority();
            priorityAdd.Code = priority.Code;
            priorityAdd.ShortDescription = priority.ShortDescription;
            priorityAdd.LongDescription = priority.LongDescription;
            priorityAdd.Action = ActionRecordTypes.Created;
            priorityAdd.CreatedDateTimeUtc = _dateTime.Now;
            priorityAdd.CreatedById = priority.CreatedById;
            priorityAdd.CreatedBy = priority.CreatedBy;
            _context.TableTaskPriorities.Add(priorityAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableTaskPriority priority)
        {
            if (await ExistAsync(priority.ID))
            {
                var priorityUpdate = await _context.TableTaskPriorities.Where(p => p.ID == priority.ID).SingleOrDefaultAsync();
                priorityUpdate.ShortDescription = priority.ShortDescription;
                priorityUpdate.LongDescription = priority.LongDescription;
                priorityUpdate.Code = priority.Code;
                priorityUpdate.Action = ActionRecordTypes.Modified;
                priorityUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                priorityUpdate.ModifiedBy = priority.ModifiedBy;
                priorityUpdate.ModifiedById = priority.ModifiedById;
                _context.TableTaskPriorities.Update(priorityUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var priorityDelete = await _context.TableTaskPriorities.Where(p => p.ID == id).SingleOrDefaultAsync();
                priorityDelete.IsDeleted = true;
                priorityDelete.Action = ActionRecordTypes.Deleted;
                priorityDelete.DeletedDateTimeUtc = _dateTime.Now;
                priorityDelete.DeletedBy = userData.UserName;
                priorityDelete.DeletedById = userData.UserId;
                _context.TableTaskPriorities.Update(priorityDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableTaskPriorities.AnyAsync(p => p.ID == id);
        }


    }
}
