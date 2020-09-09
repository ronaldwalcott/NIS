using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data.Entity.Employer
{
    public class ERMaster : EntityBase
    {
        public long ID { get; set; }
        [StringLength(15)]
        public string RegistrationNumber { get; set; }
        [StringLength(100)]
        [Required]
        public string EmployerName { get; set; }
        [StringLength(100)]
        [Required]
        public string TradeName { get; set; }

        public ERBasic ERBasic { get; set; }
        public ERBasic2 ERBasic2 { get; set; }
        public ERContact ERContact { get; set; }
        public ICollection<ERDirector> ERDirectors { get; set; }

        public DateTimeOffset? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }

    }
}
