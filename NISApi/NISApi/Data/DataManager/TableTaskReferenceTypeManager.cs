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
    public class TableTaskReferenceTypeManager : ITableTaskReferenceTypeManager
    {
        private readonly ILogger<TableTaskReferenceTypeManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableTaskReferenceTypeManager(NisDbContext dbContext, ILogger<TableTaskReferenceTypeManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableTaskReferenceType> GetTaskReferenceTypes()
        {
            IEnumerable<TableTaskReferenceType> taskReferenceTypes;

            taskReferenceTypes = _context.TableTaskReferenceTypes.AsQueryable();


            return taskReferenceTypes;
        }

        public async Task<IEnumerable<TableTaskReferenceType>> GetAllAsync()
        {
            return await _context.TableTaskReferenceTypes.ToListAsync();
        }

        public async Task<TableTaskReferenceType> GetByIdAsync(object id)
        {
            return await _context.TableTaskReferenceTypes.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableTaskReferenceType taskReferenceType)
        {
            var taskReferenceTypeAdd = new TableTaskReferenceType();
            taskReferenceTypeAdd.Code = taskReferenceType.Code;
            taskReferenceTypeAdd.ShortDescription = taskReferenceType.ShortDescription;
            taskReferenceTypeAdd.LongDescription = taskReferenceType.LongDescription;
            taskReferenceTypeAdd.Action = ActionRecordTypes.Created;
            taskReferenceTypeAdd.CreatedDateTimeUtc = _dateTime.Now;
            taskReferenceTypeAdd.CreatedById = taskReferenceType.CreatedById;
            taskReferenceTypeAdd.CreatedBy = taskReferenceType.CreatedBy;
            _context.TableTaskReferenceTypes.Add(taskReferenceTypeAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableTaskReferenceType taskReferenceType)
        {
            if (await ExistAsync(taskReferenceType.ID))
            {
                var taskReferenceTypeUpdate = await _context.TableTaskReferenceTypes.Where(p => p.ID == taskReferenceType.ID).SingleOrDefaultAsync();
                taskReferenceTypeUpdate.ShortDescription = taskReferenceType.ShortDescription;
                taskReferenceTypeUpdate.LongDescription = taskReferenceType.LongDescription;
                taskReferenceTypeUpdate.Code = taskReferenceType.Code;
                taskReferenceTypeUpdate.Action = ActionRecordTypes.Modified;
                taskReferenceTypeUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                taskReferenceTypeUpdate.ModifiedBy = taskReferenceType.ModifiedBy;
                taskReferenceTypeUpdate.ModifiedById = taskReferenceType.ModifiedById;
                _context.TableTaskReferenceTypes.Update(taskReferenceTypeUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var taskReferenceTypeDelete = await _context.TableTaskReferenceTypes.Where(p => p.ID == id).SingleOrDefaultAsync();
                taskReferenceTypeDelete.IsDeleted = true;
                taskReferenceTypeDelete.Action = ActionRecordTypes.Deleted;
                taskReferenceTypeDelete.DeletedDateTimeUtc = _dateTime.Now;
                taskReferenceTypeDelete.DeletedBy = userData.UserName;
                taskReferenceTypeDelete.DeletedById = userData.UserId;
                _context.TableTaskReferenceTypes.Update(taskReferenceTypeDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableTaskReferenceTypes.AnyAsync(p => p.ID == id);
        }


    }
}
