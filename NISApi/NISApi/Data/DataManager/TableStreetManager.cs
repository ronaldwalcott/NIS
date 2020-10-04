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
    public class TableStreetManager : ITableStreetManager
    {
        private readonly ILogger<TableStreetManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableStreetManager(NisDbContext dbContext, ILogger<TableStreetManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableStreet> GetStreets()
        {
            IEnumerable<TableStreet> streets;

            streets = _context.TableStreets.AsQueryable();


            return streets;
        }

        public async Task<IEnumerable<TableStreet>> GetAllAsync()
        {
            return await _context.TableStreets.ToListAsync();
        }

        public async Task<TableStreet> GetByIdAsync(object id)
        {
            return await _context.TableStreets.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableStreet street)
        {
            var streetAdd = new TableStreet();
            streetAdd.Code = street.Code;
            streetAdd.ShortDescription = street.ShortDescription;
            streetAdd.LongDescription = street.LongDescription;
            streetAdd.Action = ActionRecordTypes.Created;
            streetAdd.CreatedDateTimeUtc = _dateTime.Now;
            streetAdd.CreatedById = street.CreatedById;
            streetAdd.CreatedBy = street.CreatedBy;
            _context.TableStreets.Add(streetAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableStreet street)
        {
            if (await ExistAsync(street.ID))
            {
                var streetUpdate = await _context.TableStreets.Where(p => p.ID == street.ID).SingleOrDefaultAsync();
                streetUpdate.ShortDescription = street.ShortDescription;
                streetUpdate.LongDescription = street.LongDescription;
                streetUpdate.Code = street.Code;
                streetUpdate.Action = ActionRecordTypes.Modified;
                streetUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                streetUpdate.ModifiedBy = street.ModifiedBy;
                streetUpdate.ModifiedById = street.ModifiedById;
                _context.TableStreets.Update(streetUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var streetDelete = await _context.TableStreets.Where(p => p.ID == id).SingleOrDefaultAsync();
                streetDelete.IsDeleted = true;
                streetDelete.Action = ActionRecordTypes.Deleted;
                streetDelete.DeletedDateTimeUtc = _dateTime.Now;
                streetDelete.DeletedBy = userData.UserName;
                streetDelete.DeletedById = userData.UserId;
                _context.TableStreets.Update(streetDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableStreets.AnyAsync(p => p.ID == id);
        }


    }
}
