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
    public class TableCountryManager : ITableCountryManager
    {
        private readonly ILogger<TableCountryManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableCountryManager(NisDbContext dbContext, ILogger<TableCountryManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableCountry> GetCountries()
        {
            IEnumerable<TableCountry> countries;

            countries = _context.TableCountries.AsQueryable();


            return countries;
        }

        public async Task<IEnumerable<TableCountry>> GetAllAsync()
        {
            return await _context.TableCountries.ToListAsync();
        }

        public async Task<TableCountry> GetByIdAsync(object id)
        {
            return await _context.TableCountries.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableCountry country)
        {
            var countryAdd = new TableCountry();
            countryAdd.Code = country.Code;
            countryAdd.ShortDescription = country.ShortDescription;
            countryAdd.LongDescription = country.LongDescription;
            countryAdd.Action = ActionRecordTypes.Created;
            countryAdd.CreatedDateTimeUtc = _dateTime.Now;
            countryAdd.CreatedById = country.CreatedById;
            countryAdd.CreatedBy = country.CreatedBy;
            _context.TableCountries.Add(countryAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableCountry country)
        {
            if (await ExistAsync(country.ID))
            {
                var countryUpdate = await _context.TableCountries.Where(p => p.ID == country.ID).SingleOrDefaultAsync();
                countryUpdate.ShortDescription = country.ShortDescription;
                countryUpdate.LongDescription = country.LongDescription;
                countryUpdate.Code = country.Code;
                countryUpdate.Action = ActionRecordTypes.Modified;
                countryUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                countryUpdate.ModifiedBy = country.ModifiedBy;
                countryUpdate.ModifiedById = country.ModifiedById;
                _context.TableCountries.Update(countryUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var countryDelete = await _context.TableCountries.Where(p => p.ID == id).SingleOrDefaultAsync();
                countryDelete.IsDeleted = true;
                countryDelete.Action = ActionRecordTypes.Deleted;
                countryDelete.DeletedDateTimeUtc = _dateTime.Now;
                countryDelete.DeletedBy = userData.UserName;
                countryDelete.DeletedById = userData.UserId;
                _context.TableCountries.Update(countryDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableCountries.AnyAsync(p => p.ID == id);
        }


    }
}