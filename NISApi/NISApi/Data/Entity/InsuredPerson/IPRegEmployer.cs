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
        public int ID { get; set; }
        [StringLength(100)]
        public string EmployerName { get; set; }
        public bool ActiveEmployer { get; set; }
        [StringLength(100)]
        public string EmployerAddressLine1 { get; set; }
        [StringLength(100)]
        public string EmployerAddressLine2 { get; set; }

        public int? EmployerParishID { get; set; }
        [ForeignKey("EmployerParishID")]
        public TableParish Parish { get; set; }

        public int? EmployerPostalCodeID { get; set; }
        [ForeignKey("EmployerPostalCodeID")]
        public TablePostalCode PostalCode { get; set; }

        public int? EmployerDistrictID { get; set; }
        [ForeignKey("EmployerDistrictID")]
        public TableDistrict District { get; set; }

        public int? EmployerStreetID { get; set; }
        [ForeignKey("EmployerStreetID")]
        public TableStreet Street { get; set; }

        public int? EmployerPostOfficeID { get; set; }
        [ForeignKey("EmployerPostOfficeID")]
        public TablePostOffice PostOffice { get; set; }

        public int InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }

    }
}
