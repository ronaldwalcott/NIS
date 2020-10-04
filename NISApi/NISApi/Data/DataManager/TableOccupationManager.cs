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
    public class TableOccupationManager : ITableOccupationManager
    {
        private readonly ILogger<TableOccupationManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableOccupationManager(NisDbContext dbContext, ILogger<TableOccupationManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableOccupation> GetOccupations()
        {
            IEnumerable<TableOccupation> occupations;

            occupations = _context.TableOccupations.AsQueryable();


            return occupations;
        }

        public async Task<IEnumerable<TableOccupation>> GetAllAsync()
        {
            return await _context.TableOccupations.ToListAsync();
        }

        public async Task<TableOccupation> GetByIdAsync(object id)
        {
            return await _context.TableOccupations.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableOccupation occupation)
        {
            var occupationAdd = new TableOccupation();
            occupationAdd.Code = occupation.Code;
            occupationAdd.ShortDescription = occupation.ShortDescription;
            occupationAdd.LongDescription = occupation.LongDescription;
            occupationAdd.Action = ActionRecordTypes.Created;
            occupationAdd.CreatedDateTimeUtc = _dateTime.Now;
            occupationAdd.CreatedById = occupation.CreatedById;
            occupationAdd.CreatedBy = occupation.CreatedBy;
            _context.TableOccupations.Add(occupationAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableOccupation occupation)
        {
            if (await ExistAsync(occupation.ID))
            {
                var occupationUpdate = await _context.TableOccupations.Where(p => p.ID == occupation.ID).SingleOrDefaultAsync();
                occupationUpdate.ShortDescription = occupation.ShortDescription;
                occupationUpdate.LongDescription = occupation.LongDescription;
                occupationUpdate.Code = occupation.Code;
                occupationUpdate.Action = ActionRecordTypes.Modified;
                occupationUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                occupationUpdate.ModifiedBy = occupation.ModifiedBy;
                occupationUpdate.ModifiedById = occupation.ModifiedById;
                _context.TableOccupations.Update(occupationUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var occupationDelete = await _context.TableOccupations.Where(p => p.ID == id).SingleOrDefaultAsync();
                occupationDelete.IsDeleted = true;
                occupationDelete.Action = ActionRecordTypes.Deleted;
                occupationDelete.DeletedDateTimeUtc = _dateTime.Now;
                occupationDelete.DeletedBy = userData.UserName;
                occupationDelete.DeletedById = userData.UserId;
                _context.TableOccupations.Update(occupationDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableOccupations.AnyAsync(p => p.ID == id);
        }


    }
}