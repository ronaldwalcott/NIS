using NISApi.Data.Entity.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.DTO.Request.Users
{
    public class PersonTaskRequest
    {
        public string key { get; set; }
        public string action { get; set; }
        public string keyColumn { get; set; }
        public string table { get; set; }

        public List<PersonTask> added { get; set; }
        public List<PersonTask> changed { get; set; }
        public List<PersonTask> deleted { get; set; }
        public PersonTask value { get; set; }
    }
}
