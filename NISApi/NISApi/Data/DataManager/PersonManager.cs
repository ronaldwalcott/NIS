﻿using NISApi.Contracts;
using NISApi.Data.Entity;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NISApi.Data.Entity.User;

namespace NISApi.Data.DataManager
{
    public class PersonManager : IPersonManager
    {
        private readonly ILogger<PersonManager> _logger;
        private readonly NisDbContext _context;
        public PersonManager(NisDbContext dbContext, ILogger<PersonManager> logger) 
        {
            _logger = logger;
            _context = dbContext;
        }

        //public async Task<(IEnumerable<Person> Persons, Pagination Pagination)> GetPersonsAsync(UrlQueryParameters urlQueryParameters)
        //{
        //    IEnumerable<Person> persons;
        //    int recordCount = default;

        //    persons = await _context.Persons.ToListAsync();

        //    var metadata = new Pagination
        //    {
        //        PageNumber = urlQueryParameters.PageNumber,
        //        PageSize = urlQueryParameters.PageSize,
        //        TotalRecords = recordCount

        //    };

        //    return (persons, metadata);
        //}
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(object id)
        {
            return await _context.Persons.Where(p => p.Id == Convert.ToInt32(id)).SingleOrDefaultAsync();
        }

        public async Task<long> CreateAsync(Person person)
        {
            var personAdd = new Person();
            personAdd.DateOfBirth = person.DateOfBirth;
            personAdd.FirstName = person.FirstName;
            personAdd.LastName = person.LastName;
            _context.Persons.Add(personAdd);

            return await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAsync(Person person)
        {
            if (await ExistAsync(person))
            {
                var personUpdate = await _context.Persons.Where(p => p.Id == person.Id).SingleOrDefaultAsync();
                personUpdate.DateOfBirth = person.DateOfBirth;
                personUpdate.FirstName = person.FirstName;
                personUpdate.LastName = person.LastName;
                _context.Persons.Update(personUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        //public async Task<bool> DeleteAsync(Person person)
        //{
        //    if (await ExistAsync(person))
        //    {
        //        var personDelete = await _context.Persons.Where(p => p.Id == person.Id).SingleOrDefaultAsync();
        //        personDelete.IsDeleted = true;
        //        _context.Persons.Update(personDelete);
        //        return await _context.SaveChangesAsync() > 0;
        //    }
        //    return false;
        //}

        public async Task<bool> DeleteAsync(object id, UserData userData)
        {
            if (await _context.Persons.AnyAsync(p => p.Id == Convert.ToInt32(id)))
            {
                var personDelete = await _context.Persons.Where(p => p.Id == Convert.ToInt32(id)).SingleOrDefaultAsync();
                personDelete.IsDeleted = true;
                personDelete.DeletedBy = userData.UserName;
                personDelete.DeletedById = userData.UserId;
              //  personDelete.DeletedDateTimeUtc = 
                _context.Persons.Update(personDelete);
                //_context.Persons.Remove(await GetByIdAsync(id));
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> ExistAsync(object id)
        {
            return await _context.Persons.AnyAsync(p => p.Id == Convert.ToInt32(id));
        }

        //public async Task<bool> ExecuteWithTransactionScope()
        //{

        //    using (var dbCon = new SqlConnection(DbConnectionString))
        //    {
        //        await dbCon.OpenAsync();
        //        var transaction = await dbCon.BeginTransactionAsync();

        //        try
        //        {
        //            //Do stuff here Insert, Update or Delete
        //            Task q1 = dbCon.ExecuteAsync("<Your SQL Query here>");
        //            Task q2 = dbCon.ExecuteAsync("<Your SQL Query here>");
        //            Task q3 = dbCon.ExecuteAsync("<Your SQL Query here>");

        //            await Task.WhenAll(q1, q2, q3);

        //            //Commit the Transaction when all query are executed successfully

        //            await transaction.CommitAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            //Rollback the Transaction when any query fails
        //            transaction.Rollback();
        //            _logger.Log(LogLevel.Error, ex, "Error when trying to execute database operations within a scope.");

        //            return false;
        //        }
        //    }
        //    return true;
        //}

    }
}
