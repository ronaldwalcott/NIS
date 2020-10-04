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
    public class TablePostalCodeManager : ITablePostalCodeManager
    {
        private readonly ILogger<TablePostalCodeManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TablePostalCodeManager(NisDbContext dbContext, ILogger<TablePostalCodeManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TablePostalCode> GetPostalCodes()
        {
            IEnumerable<TablePostalCode> postalCodes;

            postalCodes = _context.TablePostalCodes.AsQueryable();


            return postalCodes;
        }

        public async Task<IEnumerable<TablePostalCode>> GetAllAsync()
        {
            return await _context.TablePostalCodes.ToListAsync();
        }

        public async Task<TablePostalCode> GetByIdAsync(object id)
        {
            return await _context.TablePostalCodes.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TablePostalCode postalCode)
        {
            var postalCodeAdd = new TablePostalCode();
            postalCodeAdd.Code = postalCode.Code;
            postalCodeAdd.ShortDescription = postalCode.ShortDescription;
            postalCodeAdd.LongDescription = postalCode.LongDescription;
            postalCodeAdd.Action = ActionRecordTypes.Created;
            postalCodeAdd.CreatedDateTimeUtc = _dateTime.Now;
            postalCodeAdd.CreatedById = postalCode.CreatedById;
            postalCodeAdd.CreatedBy = postalCode.CreatedBy;
            _context.TablePostalCodes.Add(postalCodeAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TablePostalCode postalCode)
        {
            if (await ExistAsync(postalCode.ID))
            {
                var postalCodeUpdate = await _context.TablePostalCodes.Where(p => p.ID == postalCode.ID).SingleOrDefaultAsync();
                postalCodeUpdate.ShortDescription = postalCode.ShortDescription;
                postalCodeUpdate.LongDescription = postalCode.LongDescription;
                postalCodeUpdate.Code = postalCode.Code;
                postalCodeUpdate.Action = ActionRecordTypes.Modified;
                postalCodeUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                postalCodeUpdate.ModifiedBy = postalCode.ModifiedBy;
                postalCodeUpdate.ModifiedById = postalCode.ModifiedById;
                _context.TablePostalCodes.Update(postalCodeUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var postalCodeDelete = await _context.TablePostalCodes.Where(p => p.ID == id).SingleOrDefaultAsync();
                postalCodeDelete.IsDeleted = true;
                postalCodeDelete.Action = ActionRecordTypes.Deleted;
                postalCodeDelete.DeletedDateTimeUtc = _dateTime.Now;
                postalCodeDelete.DeletedBy = userData.UserName;
                postalCodeDelete.DeletedById = userData.UserId;
                _context.TablePostalCodes.Update(postalCodeDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TablePostalCodes.AnyAsync(p => p.ID == id);
        }


    }
}