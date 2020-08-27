using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.SystemTables
{
    public class TablePostOffice : EntityBase
    {
        public int ID { get; set; }
        [StringLength(10)]
        [Required]
        public string Code { get; set; }
        [StringLength(50)]
        [Required]
        public string ShortDescription { get; set; }
        [StringLength(90)]
        [Required]
        public string LongDescription { get; set; }
    }
}
