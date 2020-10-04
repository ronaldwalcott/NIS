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
    public class TablePostOfficeManager : ITablePostOfficeManager
    {
        private readonly ILogger<TablePostOfficeManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TablePostOfficeManager(NisDbContext dbContext, ILogger<TablePostOfficeManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TablePostOffice> GetPostOffices()
        {
            IEnumerable<TablePostOffice> postOffices;

            postOffices = _context.TablePostOffices.AsQueryable();


            return postOffices;
        }

        public async Task<IEnumerable<TablePostOffice>> GetAllAsync()
        {
            return await _context.TablePostOffices.ToListAsync();
        }

        public async Task<TablePostOffice> GetByIdAsync(object id)
        {
            return await _context.TablePostOffices.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TablePostOffice postOffice)
        {
            var postOfficeAdd = new TablePostOffice();
            postOfficeAdd.Code = postOffice.Code;
            postOfficeAdd.ShortDescription = postOffice.ShortDescription;
            postOfficeAdd.LongDescription = postOffice.LongDescription;
            postOfficeAdd.Action = ActionRecordTypes.Created;
            postOfficeAdd.CreatedDateTimeUtc = _dateTime.Now;
            postOfficeAdd.CreatedById = postOffice.CreatedById;
            postOfficeAdd.CreatedBy = postOffice.CreatedBy;
            _context.TablePostOffices.Add(postOfficeAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TablePostOffice postOffice)
        {
            if (await ExistAsync(postOffice.ID))
            {
                var postOfficeUpdate = await _context.TablePostOffices.Where(p => p.ID == postOffice.ID).SingleOrDefaultAsync();
                postOfficeUpdate.ShortDescription = postOffice.ShortDescription;
                postOfficeUpdate.LongDescription = postOffice.LongDescription;
                postOfficeUpdate.Code = postOffice.Code;
                postOfficeUpdate.Action = ActionRecordTypes.Modified;
                postOfficeUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                postOfficeUpdate.ModifiedBy = postOffice.ModifiedBy;
                postOfficeUpdate.ModifiedById = postOffice.ModifiedById;
                _context.TablePostOffices.Update(postOfficeUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var postOfficeDelete = await _context.TablePostOffices.Where(p => p.ID == id).SingleOrDefaultAsync();
                postOfficeDelete.IsDeleted = true;
                postOfficeDelete.Action = ActionRecordTypes.Deleted;
                postOfficeDelete.DeletedDateTimeUtc = _dateTime.Now;
                postOfficeDelete.DeletedBy = userData.UserName;
                postOfficeDelete.DeletedById = userData.UserId;
                _context.TablePostOffices.Update(postOfficeDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TablePostOffices.AnyAsync(p => p.ID == id);
        }


    }
}