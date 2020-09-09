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
        public long ID { get; set; }
        [StringLength(100)]
        public string BusinessAddressLine1 { get; set; }
        [StringLength(100)]
        public string BusinessAddressLine2 { get; set; }

        public long? BusinessParishID { get; set; }
        [ForeignKey("BusinessParishID")]
        public TableParish BusinessParish { get; set; }

        public long? BusinessPostalCodeID { get; set; }
        [ForeignKey("BusinessPostalCodeID")]
        public TablePostalCode BusinessPostalCode { get; set; }

        public long? BusinessDistrictID { get; set; }
        [ForeignKey("BusinessDistrictID")]
        public TableDistrict BusinessDistrict { get; set; }

        public long? BusinessStreetID { get; set; }
        [ForeignKey("BusinessStreetID")]
        public TableStreet BusinessStreet { get; set; }

        public long? BusinessPostOfficeID { get; set; }
        [ForeignKey("BusinessPostOfficeID")]
        public TablePostOffice BusinessPostOffice { get; set; }

        [StringLength(100)]
        public string MailingAddressLine1 { get; set; }
        [StringLength(100)]
        public string MailingAddressLine2 { get; set; }

        public long? MailingParishID { get; set; }
        [ForeignKey("MailingParishID")]
        public TableParish MailingParish { get; set; }

        public long? MailingPostalCodeID { get; set; }
        [ForeignKey("MailingPostalCodeID")]
        public TablePostalCode MailingPostalCode { get; set; }

        public long? MailingDistrictID { get; set; }
        [ForeignKey("MailingDistrictID")]
        public TableDistrict MailingDistrict { get; set; }

        public long? MailingStreetID { get; set; }
        [ForeignKey("MailingStreetID")]
        public TableStreet MailingStreet { get; set; }

        public long? MailingPostOfficeID { get; set; }
        [ForeignKey("MailingPostOfficeID")]
        public TablePostOffice MailingPostOffice { get; set; }

        public long? IndustryID { get; set; }
        [ForeignKey("IndustryID")]
        public TableIndustry Industry { get; set; }


        [StringLength(100)]
        public string NatureOfBusiness { get; set; }
        [StringLength(100)]
        public string LocationOfRecords { get; set; }
        [StringLength(100)]
        public string CollectorateOfPayment { get; set; }

        public long? CollectionID { get; set; }
        [ForeignKey("CollectionID")]
        public TableCollection Collection { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfLiability { get; set; }

        public long ERMasterID { get; set; }
        [ForeignKey("ERMasterID")]
        public ERMaster ERMaster { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

    }
}
