using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPAddress : EntityBase
    {
        public long ID { get; set; }
        [StringLength(100)]
        public string AddressLine1 { get; set; }
        [StringLength(100)]
        public string AddressLine2 { get; set; }

        public long? ParishID { get; set; }
        [ForeignKey("ParishID")]
        public TableParish Parish { get; set; }

        public long? PostalCodeID { get; set; }
        [ForeignKey("PostalCodeID")]
        public TablePostalCode PostalCode { get; set; }

        public long? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public TableDistrict District { get; set; }

        public long? StreetID { get; set; }
        [ForeignKey("StreetID")]
        public TableStreet Street { get; set; }

        public long? PostOfficeID { get; set; }
        [ForeignKey("PostOfficeID")]
        public TablePostOffice PostOffice { get; set; }

        public long InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

    }
}
