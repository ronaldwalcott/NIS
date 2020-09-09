using NISApi.Data.Entity.SystemTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPBasic : EntityBase
    {
        public long ID { get; set; }
        [StringLength(70)]
        public string AliasName { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(16)]
        public string NationalRegistrationNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(70)]
        public string OccupationFreeform { get; set; }
        [StringLength(70)]
        public string PreviousName { get; set; }
        [StringLength(100)]
        public string AddressLine1 { get; set; }
        [StringLength(100)]
        public string AddressLine2 { get; set; }
        [StringLength(70)]
        public string MotherSurname { get; set; }
        [StringLength(70)]
        public string MotherFirstName { get; set; }
        [StringLength(70)]
        public string MotherMiddleName { get; set; }
        [StringLength(70)]
        public string MotherMaidenName { get; set; }
        [StringLength(70)]
        public string FatherSurname { get; set; }
        [StringLength(70)]
        public string FatherFirstName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }

        public long? CountryOfBirthID { get; set; }
        [ForeignKey("CountryOfBirthID")]
        public TableCountry CountryOfBirth { get; set; }

        public long? NationalityID { get; set; }
        [ForeignKey("NationalityID")]
        public TableNationality Nationality { get; set; }

        public long? OccupationID { get; set; }
        [ForeignKey("OccupationID")]
        public TableOccupation Occupation { get; set; }

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

        public long? EmploymentTypeID { get; set; }
        [ForeignKey("EmploymentTypeID")]
        public TableEmploymentType EmploymentType { get; set; }

        public long? MaritalStatusID { get; set; }
        [ForeignKey("MaritalStatusID")]
        public TableMaritalStatus MaritalStatus { get; set; }

        public long? ParishOfBirthID { get; set; }
        [ForeignKey("ParishOfBirthID")]
        public TableParish ParishOfBirth { get; set; }

        public long InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }


        public DateTimeOffset? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }


    }
}
