using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity
{
    public class EntityBase
    {
        //Add common Properties here that will be used for all your entities
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public string DeletedById { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public string Action { get; set; }
        public DateTimeOffset CreatedDateTimeUtc { get; set; }
        public DateTimeOffset ModifiedDateTimeUtc { get; set; }
        public DateTimeOffset DeletedDateTimeUtc { get; set; }
        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] Timestamp  { get; set; }
    }
}
