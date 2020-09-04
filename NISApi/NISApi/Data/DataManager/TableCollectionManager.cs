using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NISApi.Constants;
using NISApi.Contracts;
using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.DataManager
{
    public class TableCollectionManager : ITableCollectionManager
    {
        private readonly ILogger<TableCollectionManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableCollectionManager(NisDbContext dbContext, ILogger<TableCollectionManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public (IEnumerable<TableCollection> Collections, Pagination Pagination) GetCollections(UrlQueryParameters urlQueryParameters)
        {
            IEnumerable<TableCollection> collections;
            int recordCount = 0;

            collections = _context.TableCollections.AsQueryable();

            var metadata = new Pagination
            {
                PageNumber = urlQueryParameters.PageNumber,
                PageSize = urlQueryParameters.PageSize,
                TotalRecords = recordCount
            };

            return (collections, metadata);
        }

        public async Task<IEnumerable<TableCollection>> GetAllAsync()
        {
            return await _context.TableCollections.ToListAsync();
        }

        public async Task<TableCollection> GetByIdAsync(object id)
        {
            return await _context.TableCollections.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableCollection collection)
        {
            var collectionAdd = new TableCollection();
            collectionAdd.Code = collection.Code;
            collectionAdd.ShortDescription = collection.ShortDescription;
            collectionAdd.LongDescription = collection.LongDescription;
            collectionAdd.Action = ActionRecordTypes.Created;
            collectionAdd.CreatedDateTimeUtc = _dateTime.Now;
            collectionAdd.CreatedById = collection.CreatedById;
            collectionAdd.CreatedBy = collection.CreatedBy;
            _context.TableCollections.Add(collectionAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableCollection collection)
        {
            if (await ExistAsync(collection.ID))
            {
                var collectionUpdate = await _context.TableCollections.Where(p => p.ID == collection.ID).SingleOrDefaultAsync();
                collectionUpdate.Code = collection.Code;
                collectionUpdate.ShortDescription = collection.ShortDescription;
                collectionUpdate.LongDescription = collection.LongDescription;
                collectionUpdate.Action = ActionRecordTypes.Modified;
                collectionUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                collectionUpdate.ModifiedBy = collection.ModifiedBy;
                collectionUpdate.ModifiedById = collection.ModifiedById;
                _context.TableCollections.Update(collectionUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(TableCollection collection)
        {
            if (await ExistAsync(collection.ID))
            {
                var collectionDelete = await _context.TableCollections.Where(p => p.ID == collection.ID).SingleOrDefaultAsync();
                collectionDelete.IsDeleted = true;
                collectionDelete.Action = ActionRecordTypes.Deleted;
                collectionDelete.DeletedDateTimeUtc = _dateTime.Now;
                collectionDelete.DeletedBy = collection.DeletedBy;
                collectionDelete.DeletedById = collection.DeletedById;
                _context.TableCollections.Update(collectionDelete);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> ExistAsync(object id)
        {
            return await _context.TableCollections.AnyAsync(p => p.ID == Convert.ToInt64(id));
        }


    }
}
