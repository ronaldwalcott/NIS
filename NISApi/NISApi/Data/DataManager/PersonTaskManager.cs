using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NISApi.Constants;
using NISApi.Contracts;
using NISApi.Data.Entity.SystemTables;
using NISApi.Data.Entity.Tasks;
using NISApi.Data.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.DataManager
{
    public class PersonTaskManager : IPersonTaskManager
    {
        private readonly ILogger<TableCountryManager> _logger;
        private readonly NisDbContext _context;
        private readonly IDateTimeUtc _dateTime;
        public PersonTaskManager(NisDbContext dbContext, ILogger<TableCountryManager> logger, IDateTimeUtc dateTimeUtc)
        {
            _logger = logger;
            _context = dbContext;
            _dateTime = dateTimeUtc;
        }

        public IEnumerable<PersonTask> GetPersonTasks()
        {
            IEnumerable<PersonTask> personTasks;
            personTasks = _context.PersonTasks.AsQueryable();
            return personTasks;
        }

        public async Task<IEnumerable<PersonTask>> GetAllAsync()
        {
            return await _context.PersonTasks.ToListAsync();
        }

        public async Task<PersonTask> GetByIdAsync(object id)
        {
            return await _context.PersonTasks.Where(p => p.ID == Convert.ToInt64(id)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonTask>> GetByUserAsync(string userId)
        {
            return await _context.PersonTasks.Where(p => p.UserID == userId).ToListAsync();
        }


        public async Task<long> CreateAsync(PersonTask personTask)
        {
            var personTaskAdd = new PersonTask();
            personTaskAdd.Title = personTask.Title;
            personTaskAdd.Status = personTask.Status;
            personTaskAdd.Summary = personTask.Summary;
            personTaskAdd.TaskType = personTask.TaskType;
            personTaskAdd.Priority = personTask.Priority;
            personTaskAdd.ReferenceEntity = personTask.ReferenceEntity;
            personTaskAdd.ReferenceNumber = personTask.ReferenceNumber;
            personTaskAdd.ReferenceDate = personTask.ReferenceDate;
            personTaskAdd.DateToBeCompleted = personTask.DateToBeCompleted;
            personTaskAdd.Colour = personTask.Colour;
            personTaskAdd.User = personTask.User;
            personTaskAdd.UserID = personTask.UserID;
            personTaskAdd.Action = ActionRecordTypes.Created;
            personTaskAdd.CreatedDateTimeUtc = _dateTime.Now;
            personTaskAdd.CreatedById = personTask.CreatedById;
            personTaskAdd.CreatedBy = personTask.CreatedBy;
            _context.PersonTasks.Add(personTaskAdd);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(PersonTask personTask)
        {
            if (await ExistAsync(personTask.ID))
            {
                var personTaskUpdate = await _context.PersonTasks.Where(p => p.ID == personTask.ID).SingleOrDefaultAsync();
                personTaskUpdate.Title = personTask.Title;
                personTaskUpdate.Status = personTask.Status;
                personTaskUpdate.Summary = personTask.Summary;
                personTaskUpdate.TaskType = personTask.TaskType;
                personTaskUpdate.Priority = personTask.Priority;
                personTaskUpdate.ReferenceEntity = personTask.ReferenceEntity;
                personTaskUpdate.ReferenceNumber = personTask.ReferenceNumber;
                personTaskUpdate.ReferenceDate = personTask.ReferenceDate;
                personTaskUpdate.DateToBeCompleted = personTask.DateToBeCompleted;
                personTaskUpdate.Colour = personTask.Colour;
                personTaskUpdate.User = personTask.User;
                personTaskUpdate.UserID = personTask.UserID;
                personTaskUpdate.Action = ActionRecordTypes.Modified;
                personTaskUpdate.ModifiedDateTimeUtc = _dateTime.Now;
                personTaskUpdate.ModifiedBy = personTask.ModifiedBy;
                personTaskUpdate.ModifiedById = personTask.ModifiedById;
                _context.PersonTasks.Update(personTaskUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id, UserData userData)
        {
            if (await ExistAsync(id))
            {
                var personTaskDelete = await _context.PersonTasks.Where(p => p.ID == id).SingleOrDefaultAsync();
                personTaskDelete.IsDeleted = true;
                personTaskDelete.Action = ActionRecordTypes.Deleted;
                personTaskDelete.DeletedDateTimeUtc = _dateTime.Now;
                personTaskDelete.DeletedBy = userData.UserName;
                personTaskDelete.DeletedById = userData.UserId;
                _context.PersonTasks.Update(personTaskDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }


        public async Task<bool> ExistAsync(long id)
        {
            return await _context.PersonTasks.AnyAsync(p => p.ID == id);
        }


    }
}