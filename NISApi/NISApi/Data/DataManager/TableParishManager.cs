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
    public class TableParishManager : ITableParishManager
    {
        private readonly ILogger<TableParishManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableParishManager(NisDbContext dbContext, ILogger<TableParishManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableParish> GetParishes()
        {
            IEnumerable<TableParish> parishes;

            parishes = _context.TableParishes.AsQueryable();


            return parishes;
        }

        public async Task<IEnumerable<TableParish>> GetAllAsync()
        {
            return await _context.TableParishes.ToListAsync();
        }

        public async Task<TableParish> GetByIdAsync(object id)
        {
            return await _context.TableParishes.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableParish parish)
        {
            var parishAdd = new TableParish();
            parishAdd.Code = parish.Code;
            parishAdd.ShortDescription = parish.ShortDescription;
            parishAdd.LongDescription = parish.LongDescription;
            parishAdd.Action = ActionRecordTypes.Created;
            parishAdd.CreatedDateTimeUtc = _dateTime.Now;
            parishAdd.CreatedById = parish.CreatedById;
            parishAdd.CreatedBy = parish.CreatedBy;
            _context.TableParishes.Add(parishAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableParish parish)
        {
            if (await ExistAsync(parish.ID))
            {
                var parishUpdate = await _context.TableParishes.Where(p => p.ID == parish.ID).SingleOrDefaultAsync();
                parishUpdate.ShortDescription = parish.ShortDescription;
                parishUpdate.LongDescription = parish.LongDescription;
                parishUpdate.Code = parish.Code;
                parishUpdate.Action = ActionRecordTypes.Modified;
                parishUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                parishUpdate.ModifiedBy = parish.ModifiedBy;
                parishUpdate.ModifiedById = parish.ModifiedById;
                _context.TableParishes.Update(parishUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var parishDelete = await _context.TableParishes.Where(p => p.ID == id).SingleOrDefaultAsync();
                parishDelete.IsDeleted = true;
                parishDelete.Action = ActionRecordTypes.Deleted;
                parishDelete.DeletedDateTimeUtc = _dateTime.Now;
                parishDelete.DeletedBy = userData.UserName;
                parishDelete.DeletedById = userData.UserId;
                _context.TableParishes.Update(parishDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableParishes.AnyAsync(p => p.ID == id);
        }


    }
}
