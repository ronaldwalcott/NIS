using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.Employer
{
    public class ERBasic : EntityBase
    {
        public int ID { get; set; }
        [StringLength(100)]
        public string BusinessAddressLine1 { get; set; }
        [StringLength(100)]
        public string BusinessAddressLine2 { get; set; }

        public int? BusinessParishID { get; set; }
        [ForeignKey("BusinessParishID")]
        public TableParish BusinessParish { get; set; }

        public int? BusinessPostalCodeID { get; set; }
        [ForeignKey("BusinessPostalCodeID")]
        public TablePostalCode BusinessPostalCode { get; set; }

        public int? BusinessDistrictID { get; set; }
        [ForeignKey("BusinessDistrictID")]
        public TableDistrict BusinessDistrict { get; set; }

        public int? BusinessStreetID { get; set; }
        [ForeignKey("BusinessStreetID")]
        public TableStreet BusinessStreet { get; set; }

        public int? BusinessPostOfficeID { get; set; }
        [ForeignKey("BusinessPostOfficeID")]
        public TablePostOffice BusinessPostOffice { get; set; }

        [StringLength(100)]
        public string MailingAddressLine1 { get; set; }
        [StringLength(100)]
        public string MailingAddressLine2 { get; set; }

        public int? MailingParishID { get; set; }
        [ForeignKey("MailingParishID")]
        public TableParish MailingParish { get; set; }

        public int? MailingPostalCodeID { get; set; }
        [ForeignKey("MailingPostalCodeID")]
        public TablePostalCode MailingPostalCode { get; set; }

        public int? MailingDistrictID { get; set; }
        [ForeignKey("MailingDistrictID")]
        public TableDistrict MailingDistrict { get; set; }

        public int? MailingStreetID { get; set; }
        [ForeignKey("MailingStreetID")]
        public TableStreet MailingStreet { get; set; }

        public int? MailingPostOfficeID { get; set; }
        [ForeignKey("MailingPostOfficeID")]
        public TablePostOffice MailingPostOffice { get; set; }

        public int? IndustryID { get; set; }
        [ForeignKey("IndustryID")]
        public TableIndustry Industry { get; set; }


        [StringLength(100)]
        public string NatureOfBusiness { get; set; }
        [StringLength(100)]
        public string LocationOfRecords { get; set; }
        [StringLength(100)]
        public string CollectorateOfPayment { get; set; }

        public int? CollectionID { get; set; }
        [ForeignKey("CollectionID")]
        public TableCollection Collection { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfLiability { get; set; }

        public int ERMasterID { get; set; }
        [ForeignKey("ERMasterID")]
        public ERMaster ERMaster { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

    }
}
