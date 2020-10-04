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
using NISApi.Data.Entity.SystemTables;
using NISApi.DTO.Request.SystemTables;
using Microsoft.AspNet.OData.Routing;
using NISApi.Infrastructure.Helpers;

namespace NISApi.API.v1
{ 
   
    [Route("api/v1/TableCollections")]
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
        public IEnumerable<CollectionQueryResponse> Get()
        {
            var data =  _collectionManager.GetCollections();
            var collections = _mapper.Map<IEnumerable<CollectionQueryResponse>>(data);

            return collections;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreateCollectionRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var collection = _mapper.Map<TableCollection>(createRequest);
            collection.CreatedBy = userData.UserName;
            collection.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _collectionManager.CreateAsync(collection), Status201Created);
        }


       // [Route("{id:long}")]
        [HttpPut]
        //[ODataRoute("({id})")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        //public async Task<IActionResult> Put([FromODataUri] long key, [FromBody] UpdateCollectionRequest updateRequest)
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateCollectionRequest updateRequest)
        {
            if (!ModelState.IsValid) 
            { 
                throw new ApiProblemDetailsException(ModelState); 
            }

            if (key != updateRequest.ID)
            {
                throw new ApiProblemDetailsException($"Problem accessing record with Code: {updateRequest.Code}.", Status400BadRequest);
//                return ap  BadRequest();
            }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var collection = _mapper.Map<TableCollection>(updateRequest);
            collection.ModifiedBy = userData.UserName;
            collection.ModifiedById = userData.UserId;

            if (await _collectionManager.UpdateAsync(collection))
            {
                return new ApiResponse($"Record with Code: {updateRequest.Code} sucessfully updated.", true);
            }
            else
            {
                throw new ApiProblemDetailsException($"Record with Id: {key} does not exist.", Status404NotFound);
            }
        }

        [Route("{id:long}")]
        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Delete([FromODataUri] long key)
        {
            if (!ModelState.IsValid) 
            { 
                throw new ApiProblemDetailsException(ModelState); 
            }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            if (await _collectionManager.DeleteAsync(key, userData))
            {
                return new ApiResponse($"Record with Id: {key} sucessfully deleted.", true);
            }
            else
            {
                throw new ApiProblemDetailsException($"Record with Id: {key} does not exist.", Status404NotFound);
            }
        }



        //public IEnumerable<CollectionQueryResponse> Get([FromQuery] UrlQueryParameters urlQueryParameters)
        //{
        //    //var queryString = Request.Query;
        //    var data = _collectionManager.GetCollections(urlQueryParameters);
        //    var collections = _mapper.Map<IEnumerable<CollectionQueryResponse>>(data.Collections);

        //    return collections;
        //}



        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CollectionQueryResponse>), Status200OK)]
        //public async Task<IEnumerable<CollectionQueryResponse>> GetAll()
        //{
        //    var data = await _collectionManager.GetAllAsync();
        //    var collections = _mapper.Map<IEnumerable<CollectionQueryResponse>>(data);

        //    return collections;
        //}

    }
}