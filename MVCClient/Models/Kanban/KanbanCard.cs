using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models.Kanban
{
    public class KanbanCard
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string TaskType { get; set; }
        public string Priority { get; set; }
        public string ReferenceEntity { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTimeOffset? ReferenceDate { get; set; }
        public DateTimeOffset? DateToBeCompleted { get; set; }
        public string Colour { get; set; }
        public string User { get; set; }
        public string UserID { get; set; }
    }
}
