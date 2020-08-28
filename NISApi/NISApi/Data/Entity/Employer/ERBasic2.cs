using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.Employer
{
    public class ERBasic2 : EntityBase
    {
        public int ID { get; set; }

        public int NumberOfBranches { get; set; }
        public int NumberOfDirectors { get; set; }
        public int NumberOfEmployees { get; set; }
        [StringLength(70)]
        [Required]
        public string SurnamePersonRegistering { get; set; }
        [StringLength(70)]
        [Required]
        public string FirstNamePersonRegistering { get; set; }
        [StringLength(70)]
        public string MiddleNamePersonRegistering { get; set; }
        [StringLength(70)]
        public string PositionPersonRegistering { get; set; }

        public int ERMasterID { get; set; }
        [ForeignKey("ERMasterID")]
        public ERMaster ERMaster { get; set; }

    }
}
