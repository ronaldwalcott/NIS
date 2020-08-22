using System;
using System.ComponentModel.DataAnnotations;

namespace NISApi.Data.Entity
{
    public class Person : EntityBase
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
