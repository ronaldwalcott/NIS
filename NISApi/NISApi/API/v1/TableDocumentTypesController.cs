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

    [Route("api/v1/TableDocumentTypes")]
    [ApiController]
    public class TableDocumentTypesController : ControllerBase
    {

        private readonly ILogger<TableDocumentTypesController> _logger;
        private readonly ITableDocumentTypeManager _documentTypeManager;
        private readonly IMapper _mapper;

        public TableDocumentTypesController(ITableDocumentTypeManager documentTypeManager, IMapper mapper, ILogger<TableDocumentTypesController> logger)
        {
            _documentTypeManager = documentTypeManager;
            _mapper = mapper;
            _logger = logger;
        }


        [EnableQuery]
        [HttpGet]
        public IEnumerable<DocumentTypeQueryResponse> Get()
        {
            var data = _documentTypeManager.GetDocumentTypes();
            var documentTypes = _mapper.Map<IEnumerable<DocumentTypeQueryResponse>>(data);

            return documentTypes;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreateDocumentTypeRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            UserClaim userClaim = new UserClaim();
            UserData userData = userClaim.Claims(User);

            var documentType = _mapper.Map<TableDocumentType>(createRequest);
            documentType.CreatedBy = userData.UserName;
            documentType.CreatedById = userData.UserId;

            return new ApiResponse("Record successfully created.", await _documentTypeManager.CreateAsync(documentType), Status201Created);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        public async Task<ApiResponse> Put([FromODataUri] long key, [FromBody] UpdateDocumentTypeRequest updateRequest)
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

            var documentType = _mapper.Map<TableDocumentType>(updateRequest);
            documentType.ModifiedBy = userData.UserName;
            documentType.ModifiedById = userData.UserId;

            if (await _documentTypeManager.UpdateAsync(documentType))
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

            if (await _documentTypeManager.DeleteAsync(key, userData))
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