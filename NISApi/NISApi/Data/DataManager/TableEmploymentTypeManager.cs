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
    public class TableEmploymentTypeManager : ITableEmploymentTypeManager
    {
        private readonly ILogger<TableEmploymentTypeManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableEmploymentTypeManager(NisDbContext dbContext, ILogger<TableEmploymentTypeManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableEmploymentType> GetEmploymentTypes()
        {
            IEnumerable<TableEmploymentType> employmentTypes;

            employmentTypes = _context.TableEmploymentTypes.AsQueryable();


            return employmentTypes;
        }

        public async Task<IEnumerable<TableEmploymentType>> GetAllAsync()
        {
            return await _context.TableEmploymentTypes.ToListAsync();
        }

        public async Task<TableEmploymentType> GetByIdAsync(object id)
        {
            return await _context.TableEmploymentTypes.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableEmploymentType employmentType)
        {
            var employmentTypeAdd = new TableEmploymentType();
            employmentTypeAdd.Code = employmentType.Code;
            employmentTypeAdd.ShortDescription = employmentType.ShortDescription;
            employmentTypeAdd.LongDescription = employmentType.LongDescription;
            employmentTypeAdd.Action = ActionRecordTypes.Created;
            employmentTypeAdd.CreatedDateTimeUtc = _dateTime.Now;
            employmentTypeAdd.CreatedById = employmentType.CreatedById;
            employmentTypeAdd.CreatedBy = employmentType.CreatedBy;
            _context.TableEmploymentTypes.Add(employmentTypeAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableEmploymentType employmentType)
        {
            if (await ExistAsync(employmentType.ID))
            {
                var employmentTypeUpdate = await _context.TableEmploymentTypes.Where(p => p.ID == employmentType.ID).SingleOrDefaultAsync();
                employmentTypeUpdate.ShortDescription = employmentType.ShortDescription;
                employmentTypeUpdate.LongDescription = employmentType.LongDescription;
                employmentTypeUpdate.Code = employmentType.Code;
                employmentTypeUpdate.Action = ActionRecordTypes.Modified;
                employmentTypeUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                employmentTypeUpdate.ModifiedBy = employmentType.ModifiedBy;
                employmentTypeUpdate.ModifiedById = employmentType.ModifiedById;
                _context.TableEmploymentTypes.Update(employmentTypeUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var employmentTypeDelete = await _context.TableEmploymentTypes.Where(p => p.ID == id).SingleOrDefaultAsync();
                employmentTypeDelete.IsDeleted = true;
                employmentTypeDelete.Action = ActionRecordTypes.Deleted;
                employmentTypeDelete.DeletedDateTimeUtc = _dateTime.Now;
                employmentTypeDelete.DeletedBy = userData.UserName;
                employmentTypeDelete.DeletedById = userData.UserId;
                _context.TableEmploymentTypes.Update(employmentTypeDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableEmploymentTypes.AnyAsync(p => p.ID == id);
        }


    }
}
