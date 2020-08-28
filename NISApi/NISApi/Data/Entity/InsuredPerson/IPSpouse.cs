using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.InsuredPerson
{
    public class IPSpouse : EntityBase
    {
        public int ID { get; set; }
        [StringLength(70)]
        public string Surname { get; set; }
        [StringLength(70)]
        public string Firstname { get; set; }
        [StringLength(70)]
        public string MiddleName { get; set; }
        [StringLength(70)]
        public string MaidenName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfMarriage { get; set; }

        public int InsuredPersonID { get; set; }
        [ForeignKey("InsuredPersonID")]
        public IPMaster IPMaster { get; set; }

    }
}
