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
    public class TableIndustryManager : ITableIndustryManager
    {
        private readonly ILogger<TableIndustryManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableIndustryManager(NisDbContext dbContext, ILogger<TableIndustryManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableIndustry> GetIndustries()
        {
            IEnumerable<TableIndustry> industries;

            industries = _context.TableIndustries.AsQueryable();


            return industries;
        }

        public async Task<IEnumerable<TableIndustry>> GetAllAsync()
        {
            return await _context.TableIndustries.ToListAsync();
        }

        public async Task<TableIndustry> GetByIdAsync(object id)
        {
            return await _context.TableIndustries.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableIndustry industry)
        {
            var industryAdd = new TableIndustry();
            industryAdd.Code = industry.Code;
            industryAdd.ShortDescription = industry.ShortDescription;
            industryAdd.LongDescription = industry.LongDescription;
            industryAdd.Action = ActionRecordTypes.Created;
            industryAdd.CreatedDateTimeUtc = _dateTime.Now;
            industryAdd.CreatedById = industry.CreatedById;
            industryAdd.CreatedBy = industry.CreatedBy;
            _context.TableIndustries.Add(industryAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableIndustry industry)
        {
            if (await ExistAsync(industry.ID))
            {
                var industryUpdate = await _context.TableIndustries.Where(p => p.ID == industry.ID).SingleOrDefaultAsync();
                industryUpdate.ShortDescription = industry.ShortDescription;
                industryUpdate.LongDescription = industry.LongDescription;
                industryUpdate.Code = industry.Code;
                industryUpdate.Action = ActionRecordTypes.Modified;
                industryUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                industryUpdate.ModifiedBy = industry.ModifiedBy;
                industryUpdate.ModifiedById = industry.ModifiedById;
                _context.TableIndustries.Update(industryUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var industryDelete = await _context.TableIndustries.Where(p => p.ID == id).SingleOrDefaultAsync();
                industryDelete.IsDeleted = true;
                industryDelete.Action = ActionRecordTypes.Deleted;
                industryDelete.DeletedDateTimeUtc = _dateTime.Now;
                industryDelete.DeletedBy = userData.UserName;
                industryDelete.DeletedById = userData.UserId;
                _context.TableIndustries.Update(industryDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableIndustries.AnyAsync(p => p.ID == id);
        }


    }
}