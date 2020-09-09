using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPAddressApproved : EntityBase
    {
        public long ID { get; set; }
        public bool? AddressApproval { get; set; }

    }
}
