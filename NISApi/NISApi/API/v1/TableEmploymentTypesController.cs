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

    [Route("api/v1/TableEmploymentTypes")]
    [ApiController]
    public class TableEmploymentTypesController : ControllerBase
    {

        private readonly ILogger<TableEmploymentTypesController> _logger;
        private readonly ITableEmploymentTypeManager _employmentTypeManager;
        private readonly IMapper _mapper;

        public TableEmploymentTypesController(ITableEmploymentTypeManager employmentTypeManager, IMapper mapper, ILogger<TableEmploymentTypesController> logger)
        {
            _employmentTypeManager = employmentTypeManager;
            _mapper = mapper;
            _logger = logger;
        }


        [EnableQuery]
        [HttpGet]
        public IEnumerable<EmploymentTypeQueryResponse> Get()
        {
            var data = _employmentTypeManager.GetEmploymentTypes();
            var employmentTypes = _mapper.Map<IEnumerable<EmploymentTypeQueryResponse>>(data);

            return employmentTypes;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreateEmploymentTypeRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var employmentType = _mapper.Map<TableEmploymentType>(createRequest);
            employmentType.CreatedBy = userData.UserName;
            employmentType.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _employmentTypeManager.CreateAsync(employmentType), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateEmploymentTypeRequest updateRequest)
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

            var employmentType = _mapper.Map<TableEmploymentType>(updateRequest);
            employmentType.ModifiedBy = userData.UserName;
            employmentType.ModifiedById = userData.UserId;

            if (await _employmentTypeManager.UpdateAsync(employmentType))
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

            if (await _employmentTypeManager.DeleteAsync(key, userData))
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
