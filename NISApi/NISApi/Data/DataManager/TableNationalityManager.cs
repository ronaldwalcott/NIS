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
    public class TableNationalityManager : ITableNationalityManager
    {
        private readonly ILogger<TableNationalityManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableNationalityManager(NisDbContext dbContext, ILogger<TableNationalityManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableNationality> GetNationalities()
        {
            IEnumerable<TableNationality> nationalities;

            nationalities = _context.TableNationalities.AsQueryable();


            return nationalities;
        }

        public async Task<IEnumerable<TableNationality>> GetAllAsync()
        {
            return await _context.TableNationalities.ToListAsync();
        }

        public async Task<TableNationality> GetByIdAsync(object id)
        {
            return await _context.TableNationalities.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableNationality nationality)
        {
            var nationalityAdd = new TableNationality();
            nationalityAdd.Code = nationality.Code;
            nationalityAdd.ShortDescription = nationality.ShortDescription;
            nationalityAdd.LongDescription = nationality.LongDescription;
            nationalityAdd.Action = ActionRecordTypes.Created;
            nationalityAdd.CreatedDateTimeUtc = _dateTime.Now;
            nationalityAdd.CreatedById = nationality.CreatedById;
            nationalityAdd.CreatedBy = nationality.CreatedBy;
            _context.TableNationalities.Add(nationalityAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableNationality nationality)
        {
            if (await ExistAsync(nationality.ID))
            {
                var nationalityUpdate = await _context.TableNationalities.Where(p => p.ID == nationality.ID).SingleOrDefaultAsync();
                nationalityUpdate.ShortDescription = nationality.ShortDescription;
                nationalityUpdate.LongDescription = nationality.LongDescription;
                nationalityUpdate.Code = nationality.Code;
                nationalityUpdate.Action = ActionRecordTypes.Modified;
                nationalityUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                nationalityUpdate.ModifiedBy = nationality.ModifiedBy;
                nationalityUpdate.ModifiedById = nationality.ModifiedById;
                _context.TableNationalities.Update(nationalityUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var nationalityDelete = await _context.TableNationalities.Where(p => p.ID == id).SingleOrDefaultAsync();
                nationalityDelete.IsDeleted = true;
                nationalityDelete.Action = ActionRecordTypes.Deleted;
                nationalityDelete.DeletedDateTimeUtc = _dateTime.Now;
                nationalityDelete.DeletedBy = userData.UserName;
                nationalityDelete.DeletedById = userData.UserId;
                _context.TableNationalities.Update(nationalityDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableNationalities.AnyAsync(p => p.ID == id);
        }


    }
}