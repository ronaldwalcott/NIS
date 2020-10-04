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
    public class TableMaritalStatusManager : ITableMaritalStatusManager
    {
        private readonly ILogger<TableMaritalStatusManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableMaritalStatusManager(NisDbContext dbContext, ILogger<TableMaritalStatusManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableMaritalStatus> GetMaritalStatuses()
        {
            IEnumerable<TableMaritalStatus> maritalStatuses;

            maritalStatuses = _context.TableMaritalStatuses.AsQueryable();


            return maritalStatuses;
        }

        public async Task<IEnumerable<TableMaritalStatus>> GetAllAsync()
        {
            return await _context.TableMaritalStatuses.ToListAsync();
        }

        public async Task<TableMaritalStatus> GetByIdAsync(object id)
        {
            return await _context.TableMaritalStatuses.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableMaritalStatus maritalStatus)
        {
            var maritalStatusAdd = new TableMaritalStatus();
            maritalStatusAdd.Code = maritalStatus.Code;
            maritalStatusAdd.ShortDescription = maritalStatus.ShortDescription;
            maritalStatusAdd.LongDescription = maritalStatus.LongDescription;
            maritalStatusAdd.Action = ActionRecordTypes.Created;
            maritalStatusAdd.CreatedDateTimeUtc = _dateTime.Now;
            maritalStatusAdd.CreatedById = maritalStatus.CreatedById;
            maritalStatusAdd.CreatedBy = maritalStatus.CreatedBy;
            _context.TableMaritalStatuses.Add(maritalStatusAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableMaritalStatus maritalStatus)
        {
            if (await ExistAsync(maritalStatus.ID))
            {
                var maritalStatusUpdate = await _context.TableMaritalStatuses.Where(p => p.ID == maritalStatus.ID).SingleOrDefaultAsync();
                maritalStatusUpdate.ShortDescription = maritalStatus.ShortDescription;
                maritalStatusUpdate.LongDescription = maritalStatus.LongDescription;
                maritalStatusUpdate.Code = maritalStatus.Code;
                maritalStatusUpdate.Action = ActionRecordTypes.Modified;
                maritalStatusUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                maritalStatusUpdate.ModifiedBy = maritalStatus.ModifiedBy;
                maritalStatusUpdate.ModifiedById = maritalStatus.ModifiedById;
                _context.TableMaritalStatuses.Update(maritalStatusUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var maritalStatusDelete = await _context.TableMaritalStatuses.Where(p => p.ID == id).SingleOrDefaultAsync();
                maritalStatusDelete.IsDeleted = true;
                maritalStatusDelete.Action = ActionRecordTypes.Deleted;
                maritalStatusDelete.DeletedDateTimeUtc = _dateTime.Now;
                maritalStatusDelete.DeletedBy = userData.UserName;
                maritalStatusDelete.DeletedById = userData.UserId;
                _context.TableMaritalStatuses.Update(maritalStatusDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableMaritalStatuses.AnyAsync(p => p.ID == id);
        }


    }
}