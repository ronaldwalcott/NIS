using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.Employer
{
    public class ERContact : EntityBase
    {
        public long ID { get; set; }
        [Phone]
        public string MainTelephoneNumber { get; set; }
        [Phone]
        public string SecondaryTelephoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string WebAddress { get; set; }

        public long ERMasterID { get; set; }
        [ForeignKey("ERMasterID")]
        public ERMaster ERMaster { get; set; }

    }
}
