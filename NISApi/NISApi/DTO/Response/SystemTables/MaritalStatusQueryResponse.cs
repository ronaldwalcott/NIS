using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.DTO.Response.SystemTables
{
    public class MaritalStatusQueryResponse
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
