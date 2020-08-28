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
        public int ID { get; set; }
        public int? DocumentTypeId { get; set; }
        public TableDocumentType DocumentType { get; set; }
        public int InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }

    }
}
