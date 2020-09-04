using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Contracts
{
    public interface IDateTimeUtc
    {
        DateTimeOffset Now { get; }
    }
}
