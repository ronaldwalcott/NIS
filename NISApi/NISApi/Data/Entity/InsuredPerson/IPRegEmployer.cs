using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPRegEmployer : EntityBase
    {
        public long ID { get; set; }
        [StringLength(100)]
        public string EmployerName { get; set; }
        public bool ActiveEmployer { get; set; }
        [StringLength(100)]
        public string EmployerAddressLine1 { get; set; }
        [StringLength(100)]
        public string EmployerAddressLine2 { get; set; }

        public long? EmployerParishID { get; set; }
        [ForeignKey("EmployerParishID")]
        public TableParish Parish { get; set; }

        public long? EmployerPostalCodeID { get; set; }
        [ForeignKey("EmployerPostalCodeID")]
        public TablePostalCode PostalCode { get; set; }

        public long? EmployerDistrictID { get; set; }
        [ForeignKey("EmployerDistrictID")]
        public TableDistrict District { get; set; }

        public long? EmployerStreetID { get; set; }
        [ForeignKey("EmployerStreetID")]
        public TableStreet Street { get; set; }

        public long? EmployerPostOfficeID { get; set; }
        [ForeignKey("EmployerPostOfficeID")]
        public TablePostOffice PostOffice { get; set; }

        public long InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }

    }
}
