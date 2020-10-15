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
    public class TableTaskTypeManager : ITableTaskTypeManager
    {
        private readonly ILogger<TableTaskTypeManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableTaskTypeManager(NisDbContext dbContext, ILogger<TableTaskTypeManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableTaskType> GetTaskTypes()
        {
            IEnumerable<TableTaskType> taskTypes;

            taskTypes = _context.TableTaskTypes.AsQueryable();


            return taskTypes;
        }

        public async Task<IEnumerable<TableTaskType>> GetAllAsync()
        {
            return await _context.TableTaskTypes.ToListAsync();
        }

        public async Task<TableTaskType> GetByIdAsync(object id)
        {
            return await _context.TableTaskTypes.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableTaskType taskType)
        {
            var taskTypeAdd = new TableTaskType();
            taskTypeAdd.Code = taskType.Code;
            taskTypeAdd.ShortDescription = taskType.ShortDescription;
            taskTypeAdd.LongDescription = taskType.LongDescription;
            taskTypeAdd.Action = ActionRecordTypes.Created;
            taskTypeAdd.CreatedDateTimeUtc = _dateTime.Now;
            taskTypeAdd.CreatedById = taskType.CreatedById;
            taskTypeAdd.CreatedBy = taskType.CreatedBy;
            _context.TableTaskTypes.Add(taskTypeAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableTaskType taskType)
        {
            if (await ExistAsync(taskType.ID))
            {
                var taskTypeUpdate = await _context.TableTaskTypes.Where(p => p.ID == taskType.ID).SingleOrDefaultAsync();
                taskTypeUpdate.ShortDescription = taskType.ShortDescription;
                taskTypeUpdate.LongDescription = taskType.LongDescription;
                taskTypeUpdate.Code = taskType.Code;
                taskTypeUpdate.Action = ActionRecordTypes.Modified;
                taskTypeUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                taskTypeUpdate.ModifiedBy = taskType.ModifiedBy;
                taskTypeUpdate.ModifiedById = taskType.ModifiedById;
                _context.TableTaskTypes.Update(taskTypeUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var taskTypeDelete = await _context.TableTaskTypes.Where(p => p.ID == id).SingleOrDefaultAsync();
                taskTypeDelete.IsDeleted = true;
                taskTypeDelete.Action = ActionRecordTypes.Deleted;
                taskTypeDelete.DeletedDateTimeUtc = _dateTime.Now;
                taskTypeDelete.DeletedBy = userData.UserName;
                taskTypeDelete.DeletedById = userData.UserId;
                _context.TableTaskTypes.Update(taskTypeDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableTaskTypes.AnyAsync(p => p.ID == id);
        }


    }
}
