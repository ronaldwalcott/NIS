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

    [Route("api/v1/TablePostalCodes")]
    [ApiController]
    public class TablePostalCodesController : ControllerBase
    {

        private readonly ILogger<TablePostalCodesController> _logger;
        private readonly ITablePostalCodeManager _postalCodeManager;
        private readonly IMapper _mapper;

        public TablePostalCodesController(ITablePostalCodeManager postalCodeManager, IMapper mapper, ILogger<TablePostalCodesController> logger)
        {
            _postalCodeManager = postalCodeManager;
            _mapper = mapper;
            _logger = logger;
        }


        [EnableQuery]
        [HttpGet]
        public IEnumerable<PostalCodeQueryResponse> Get()
        {
            var data = _postalCodeManager.GetPostalCodes();
            var postalCodes = _mapper.Map<IEnumerable<PostalCodeQueryResponse>>(data);

            return postalCodes;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreatePostalCodeRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var postalCode = _mapper.Map<TablePostalCode>(createRequest);
            postalCode.CreatedBy = userData.UserName;
            postalCode.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _postalCodeManager.CreateAsync(postalCode), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdatePostalCodeRequest updateRequest)
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

            var postalCode = _mapper.Map<TablePostalCode>(updateRequest);
            postalCode.ModifiedBy = userData.UserName;
            postalCode.ModifiedById = userData.UserId;

            if (await _postalCodeManager.UpdateAsync(postalCode))
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

            if (await _postalCodeManager.DeleteAsync(key, userData))
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