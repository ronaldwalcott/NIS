using NISApi.Data;
using NISApi.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NISApi.Contracts
{
    public interface IPersonManager : IRepository<Person>
    {
        //Task<(IEnumerable<Person> Persons, Pagination Pagination)> GetPersonsAsync(UrlQueryParameters urlQueryParameters);

        //Add more class specific methods here when neccessary
    }
}
