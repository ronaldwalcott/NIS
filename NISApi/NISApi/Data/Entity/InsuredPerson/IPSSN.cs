using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPSSN : EntityBase
    {
        public int ID { get; set; }
        [StringLength(15)]
        public string SSN { get; set; }
    }
}
