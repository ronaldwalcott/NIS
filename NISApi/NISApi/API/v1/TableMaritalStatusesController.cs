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

    [Route("api/v1/TableMaritalStatuses")]
    [ApiController]
    public class TableMaritalStatusesController : ControllerBase
    {

        private readonly ILogger<TableMaritalStatusesController> _logger;
        private readonly ITableMaritalStatusManager _maritalStatusManager;
        private readonly IMapper _mapper;

        public TableMaritalStatusesController(ITableMaritalStatusManager maritalStatusManager, IMapper mapper, ILogger<TableMaritalStatusesController> logger)
        {
            _maritalStatusManager = maritalStatusManager;
            _mapper = mapper;
            _logger = logger;
        }


        [EnableQuery]
        [HttpGet]
        public IEnumerable<MaritalStatusQueryResponse> Get()
        {
            var data = _maritalStatusManager.GetMaritalStatuses();
            var maritalStatuses = _mapper.Map<IEnumerable<MaritalStatusQueryResponse>>(data);

            return maritalStatuses;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreateMaritalStatusRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var maritalStatus = _mapper.Map<TableMaritalStatus>(createRequest);
            maritalStatus.CreatedBy = userData.UserName;
            maritalStatus.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _maritalStatusManager.CreateAsync(maritalStatus), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateMaritalStatusRequest updateRequest)
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

            var maritalStatus = _mapper.Map<TableMaritalStatus>(updateRequest);
            maritalStatus.ModifiedBy = userData.UserName;
            maritalStatus.ModifiedById = userData.UserId;

            if (await _maritalStatusManager.UpdateAsync(maritalStatus))
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

            if (await _maritalStatusManager.DeleteAsync(key, userData))
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