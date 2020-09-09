using System.Linq;
using NISApi.Contracts;
using NISApi.Data;
using NISApi.Data.Entity;
using NISApi.DTO.Request;
using NISApi.DTO.Response;
using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using NISApi.Data.Entity.User;
using IdentityModel;
using NISApi.DTO.Response.SystemTables;
using Microsoft.AspNet.OData;
using AutoWrapper.Filters;

namespace NISApi.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TableCollectionsController : ControllerBase
    {

        private readonly ILogger<TableCollectionsController> _logger;
        private readonly ITableCollectionManager _collectionManager;
        private readonly IMapper _mapper;
        public TableCollectionsController(ITableCollectionManager collectionManager, IMapper mapper, ILogger<TableCollectionsController> logger)
        {
            _collectionManager = collectionManager;
            _mapper = mapper;
            _logger = logger;
        }

        
   //     [AutoWrapIgnore]
   //should be IQueryable
       // 
        [EnableQuery]
        [HttpGet]
        public IEnumerable<CollectionQueryResponse> Get([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            //var queryString = Request.Query;
            var data = _collectionManager.GetCollections(urlQueryParameters);
            var collections = _mapper.Map<IEnumerable<CollectionQueryResponse>>(data.Collections);

            return collections;
        }



        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CollectionQueryResponse>), Status200OK)]
        //public async Task<IEnumerable<CollectionQueryResponse>> Get()
        //{
        //    var data = await _collectionManager.GetAllAsync();
        //    var collections = _mapper.Map<IEnumerable<CollectionQueryResponse>>(data);

        //    return collections;
        //}

    }
}