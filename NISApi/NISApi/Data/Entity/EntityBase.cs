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
        public long RowCreatedById { get; set; }
        public long RowModifiedById { get; set; }
        public DateTime RowCreatedDateTimeUtc { get; set; }
        public DateTime RowModifiedDateTimeUtc { get; set; }
        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] Timestamp  { get; set; }
    }
}
