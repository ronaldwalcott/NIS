using Simple.OData.Client;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public interface INisHttpOdataClient
    {
        ODataClient GetClient();
    }
}
