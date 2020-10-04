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
    public class TableDocumentTypeManager : ITableDocumentTypeManager
    {
        private readonly ILogger<TableDocumentTypeManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public TableDocumentTypeManager(NisDbContext dbContext, ILogger<TableDocumentTypeManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<TableDocumentType> GetDocumentTypes()
        {
            IEnumerable<TableDocumentType> documentTypes;

            documentTypes = _context.TableDocumentTypes.AsQueryable();


            return documentTypes;
        }

        public async Task<IEnumerable<TableDocumentType>> GetAllAsync()
        {
            return await _context.TableDocumentTypes.ToListAsync();
        }

        public async Task<TableDocumentType> GetByIdAsync(object id)
        {
            return await _context.TableDocumentTypes.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(TableDocumentType documentType)
        {
            var documentTypeAdd = new TableDocumentType();
            documentTypeAdd.Code = documentType.Code;
            documentTypeAdd.ShortDescription = documentType.ShortDescription;
            documentTypeAdd.LongDescription = documentType.LongDescription;
            documentTypeAdd.Action = ActionRecordTypes.Created;
            documentTypeAdd.CreatedDateTimeUtc = _dateTime.Now;
            documentTypeAdd.CreatedById = documentType.CreatedById;
            documentTypeAdd.CreatedBy = documentType.CreatedBy;
            _context.TableDocumentTypes.Add(documentTypeAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TableDocumentType documentType)
        {
            if (await ExistAsync(documentType.ID))
            {
                var documentTypeUpdate = await _context.TableDocumentTypes.Where(p => p.ID == documentType.ID).SingleOrDefaultAsync();
                documentTypeUpdate.ShortDescription = documentType.ShortDescription;
                documentTypeUpdate.LongDescription = documentType.LongDescription;
                documentTypeUpdate.Code = documentType.Code;
                documentTypeUpdate.Action = ActionRecordTypes.Modified;
                documentTypeUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                documentTypeUpdate.ModifiedBy = documentType.ModifiedBy;
                documentTypeUpdate.ModifiedById = documentType.ModifiedById;
                _context.TableDocumentTypes.Update(documentTypeUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var documentTypeDelete = await _context.TableDocumentTypes.Where(p => p.ID == id).SingleOrDefaultAsync();
                documentTypeDelete.IsDeleted = true;
                documentTypeDelete.Action = ActionRecordTypes.Deleted;
                documentTypeDelete.DeletedDateTimeUtc = _dateTime.Now;
                documentTypeDelete.DeletedBy = userData.UserName;
                documentTypeDelete.DeletedById = userData.UserId;
                _context.TableDocumentTypes.Update(documentTypeDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.TableDocumentTypes.AnyAsync(p => p.ID == id);
        }


    }
}