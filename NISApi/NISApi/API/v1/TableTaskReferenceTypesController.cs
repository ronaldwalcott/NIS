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

    [Route("api/v1/TableTaskReferenceTypes")]
    [ApiController]
    public class TableTaskReferenceTypesController : ControllerBase
    {

        private readonly ILogger<TableTaskReferenceTypesController> _logger;
        private readonly ITableTaskReferenceTypeManager _taskReferenceTypeManager;
        private readonly IMapper _mapper;

        public TableTaskReferenceTypesController(ITableTaskReferenceTypeManager taskReferenceTypeManager, IMapper mapper, ILogger<TableTaskReferenceTypesController> logger)
        {
            _taskReferenceTypeManager = taskReferenceTypeManager;
            _mapper = mapper;
            _logger = logger;
        }


        [EnableQuery]
        [HttpGet]
        public IEnumerable<TaskReferenceTypeQueryResponse> Get()
        {
            var data = _taskReferenceTypeManager.GetTaskReferenceTypes();
            var taskReferenceTypes = _mapper.Map<IEnumerable<TaskReferenceTypeQueryResponse>>(data);

            return taskReferenceTypes;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreateTaskReferenceTypeRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var taskReferenceType = _mapper.Map<TableTaskReferenceType>(createRequest);
            taskReferenceType.CreatedBy = userData.UserName;
            taskReferenceType.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _taskReferenceTypeManager.CreateAsync(taskReferenceType), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateTaskReferenceTypeRequest updateRequest)
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

            var taskReferenceType = _mapper.Map<TableTaskReferenceType>(updateRequest);
            taskReferenceType.ModifiedBy = userData.UserName;
            taskReferenceType.ModifiedById = userData.UserId;

            if (await _taskReferenceTypeManager.UpdateAsync(taskReferenceType))
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

            if (await _taskReferenceTypeManager.DeleteAsync(key, userData))
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