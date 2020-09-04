using NISApi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Infrastructure.Helpers
{
    public class SystemDateTimeUtc : IDateTimeUtc
    {
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }

    }
}
