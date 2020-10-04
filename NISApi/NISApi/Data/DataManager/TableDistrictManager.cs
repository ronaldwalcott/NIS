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
    public class TableDistrictManager : ITableDistrictManager
    {
        private readonly ILogger<TableDistrictManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableDistrictManager(NisDbContext dbContext, ILogger<TableDistrictManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableDistrict> GetDistricts()
        {
            IEnumerable<TableDistrict> districts;

            districts = _context.TableDistricts.AsQueryable();


            return districts;
        }

        public async Task<IEnumerable<TableDistrict>> GetAllAsync()
        {
            return await _context.TableDistricts.ToListAsync();
        }

        public async Task<TableDistrict> GetByIdAsync(object id)
        {
            return await _context.TableDistricts.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableDistrict district)
        {
            var districtAdd = new TableDistrict();
            districtAdd.Code = district.Code;
            districtAdd.ShortDescription = district.ShortDescription;
            districtAdd.LongDescription = district.LongDescription;
            districtAdd.Action = ActionRecordTypes.Created;
            districtAdd.CreatedDateTimeUtc = _dateTime.Now;
            districtAdd.CreatedById = district.CreatedById;
            districtAdd.CreatedBy = district.CreatedBy;
            _context.TableDistricts.Add(districtAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableDistrict district)
        {
            if (await ExistAsync(district.ID))
            {
                var districtUpdate = await _context.TableDistricts.Where(p => p.ID == district.ID).SingleOrDefaultAsync();
                districtUpdate.ShortDescription = district.ShortDescription;
                districtUpdate.LongDescription = district.LongDescription;
                districtUpdate.Code = district.Code;
                districtUpdate.Action = ActionRecordTypes.Modified;
                districtUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                districtUpdate.ModifiedBy = district.ModifiedBy;
                districtUpdate.ModifiedById = district.ModifiedById;
                _context.TableDistricts.Update(districtUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var districtDelete = await _context.TableDistricts.Where(p => p.ID == id).SingleOrDefaultAsync();
                districtDelete.IsDeleted = true;
                districtDelete.Action = ActionRecordTypes.Deleted;
                districtDelete.DeletedDateTimeUtc = _dateTime.Now;
                districtDelete.DeletedBy = userData.UserName;
                districtDelete.DeletedById = userData.UserId;
                _context.TableDistricts.Update(districtDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableDistricts.AnyAsync(p => p.ID == id);
        }


    }
}