using AutoMapper;
using AutoWrapper.Wrappers;
//using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NISApi.Contracts;
using NISApi.Data.Entity.Tasks;
using NISApi.Data.Entity.User;
using NISApi.DTO.Request.Users;
using NISApi.DTO.Response.Users;
using NISApi.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NISApi.API.v1
{

    [Route("api/v1/PersonTasks")]
    [ApiController]
    public class PersonTasksController : ControllerBase
    {

        private readonly ILogger<PersonTasksController> _logger;
        private readonly IPersonTaskManager _personTaskManager;
        private readonly IMapper _mapper;

        public PersonTasksController(IPersonTaskManager personTaskManager, IMapper mapper, ILogger<PersonTasksController> logger)
        {
            _personTaskManager = personTaskManager;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<PersonTaskQueryResponse> Get()
        {
            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var data = _personTaskManager.GetByUserAsync(userData.UserId);
            var personTasks = _mapper.Map<IEnumerable<PersonTaskQueryResponse>>(data);

            return personTasks;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreatePersonTaskRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var personTask = _mapper.Map<PersonTask>(createRequest);
            personTask.CreatedBy = userData.UserName;
            personTask.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _personTaskManager.CreateAsync(personTask), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        //public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateCountryRequest updateRequest)
        public async Task<ApiResponse> Put(long id, [FromBody] UpdatePersonTaskRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiProblemDetailsException(ModelState);
            }

            if (id != updateRequest.ID)
            {
                throw new ApiProblemDetailsException($"Problem accessing task with Title: {updateRequest.Title}.", Status400BadRequest);
            }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var personTask = _mapper.Map<PersonTask>(updateRequest);
            personTask.ModifiedBy = userData.UserName;
            personTask.ModifiedById = userData.UserId;

            if (await _personTaskManager.UpdateAsync(personTask))
            {
                return new ApiResponse($"Task with Title: {updateRequest.Title} sucessfully updated.", true);
            }
            else
            {
                throw new ApiProblemDetailsException($"Task with Id: {id} does not exist.", Status404NotFound);
            }
        }

        [Route("{id:long}")]
        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        //       public async Task<ApiResponse> Delete([FromODataUri] long key)
        public async Task<ApiResponse> Delete(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiProblemDetailsException(ModelState);
            }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            if (await _personTaskManager.DeleteAsync(id, userData))
            {
                return new ApiResponse($"Task with Id: {id} sucessfully deleted.", true);
            }
            else
            {
                throw new ApiProblemDetailsException($"Task with Id: {id} does not exist.", Status404NotFound);
            }
        }
    }
}