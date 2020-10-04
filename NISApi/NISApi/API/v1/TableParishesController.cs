using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NISApi.Contracts;
using NISApi.Data.Entity.SystemTables;
using NISApi.Data.Entity.User;
using NISApi.DTO.Request.SystemTables;
using NISApi.DTO.Response.SystemTables;
using NISApi.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NISApi.API.v1
{

    [Route("api/v1/TableParishes")]
    [ApiController]
    public class TableParishesController : ControllerBase
    {

        private readonly ILogger<TableParishesController> _logger;
        private readonly ITableParishManager _parishManager;
        private readonly IMapper _mapper;

        public TableParishesController(ITableParishManager parishManager, IMapper mapper, ILogger<TableParishesController> logger)
        {
            _parishManager = parishManager;
            _mapper = mapper;
            _logger = logger;
        }


        [EnableQuery]
        [HttpGet]
        public IEnumerable<ParishQueryResponse> Get()
        {
            var data = _parishManager.GetParishes();
            var parishes = _mapper.Map<IEnumerable<ParishQueryResponse>>(data);

            return parishes;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreateParishRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var parish = _mapper.Map<TableParish>(createRequest);
            parish.CreatedBy = userData.UserName;
            parish.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _parishManager.CreateAsync(parish), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateParishRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiProblemDetailsException(ModelState);
            }

            if (key != updateRequest.ID)
            {
                throw new ApiProblemDetailsException($"Problem accessing record with Code: {updateRequest.Code}.", Status400BadRequest);
            }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var parish = _mapper.Map<TableParish>(updateRequest);
            parish.ModifiedBy = userData.UserName;
            parish.ModifiedById = userData.UserId;

            if (await _parishManager.UpdateAsync(parish))
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

            if (await _parishManager.DeleteAsync(key, userData))
            {
                return new ApiResponse($"Record with Id: {key} sucessfully deleted.", true);
            }
            else
            {
                throw new ApiProblemDetailsException($"Record with Id: {key} does not exist.", Status404NotFound);
            }
        }
    }
}