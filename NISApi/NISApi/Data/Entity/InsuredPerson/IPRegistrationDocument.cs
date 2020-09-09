using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPRegistrationDocument : EntityBase
    {
        public long ID { get; set; }
        public long? DocumentTypeId { get; set; }
        public TableDocumentType DocumentType { get; set; }
        public long InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }

    }
}
