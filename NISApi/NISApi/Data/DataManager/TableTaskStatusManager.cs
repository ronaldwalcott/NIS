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
    public class TableTaskStatusManager : ITableTaskStatusManager
    {
        private readonly ILogger<TableTaskStatusManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableTaskStatusManager(NisDbContext dbContext, ILogger<TableTaskStatusManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableTaskStatus> GetTaskStatuses()
        {
            IEnumerable<TableTaskStatus> statuses;

            statuses = _context.TableTaskStatuses.AsQueryable();


            return statuses;
        }

        public async Task<IEnumerable<TableTaskStatus>> GetAllAsync()
        {
            return await _context.TableTaskStatuses.ToListAsync();
        }

        public async Task<TableTaskStatus> GetByIdAsync(object id)
        {
            return await _context.TableTaskStatuses.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableTaskStatus status)
        {
            var statusAdd = new TableTaskStatus();
            statusAdd.Code = status.Code;
            statusAdd.ShortDescription = status.ShortDescription;
            statusAdd.LongDescription = status.LongDescription;
            statusAdd.Action = ActionRecordTypes.Created;
            statusAdd.CreatedDateTimeUtc = _dateTime.Now;
            statusAdd.CreatedById = status.CreatedById;
            statusAdd.CreatedBy = status.CreatedBy;
            _context.TableTaskStatuses.Add(statusAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableTaskStatus status)
        {
            if (await ExistAsync(status.ID))
            {
                var statusUpdate = await _context.TableTaskStatuses.Where(p => p.ID == status.ID).SingleOrDefaultAsync();
                statusUpdate.ShortDescription = status.ShortDescription;
                statusUpdate.LongDescription = status.LongDescription;
                statusUpdate.Code = status.Code;
                statusUpdate.Action = ActionRecordTypes.Modified;
                statusUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                statusUpdate.ModifiedBy = status.ModifiedBy;
                statusUpdate.ModifiedById = status.ModifiedById;
                _context.TableTaskStatuses.Update(statusUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var statusDelete = await _context.TableTaskStatuses.Where(p => p.ID == id).SingleOrDefaultAsync();
                statusDelete.IsDeleted = true;
                statusDelete.Action = ActionRecordTypes.Deleted;
                statusDelete.DeletedDateTimeUtc = _dateTime.Now;
                statusDelete.DeletedBy = userData.UserName;
                statusDelete.DeletedById = userData.UserId;
                _context.TableTaskStatuses.Update(statusDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableTaskStatuses.AnyAsync(p => p.ID == id);
        }


    }
}
