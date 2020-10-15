using AutoMapper;
using NISApi.Data.Entity;
using NISApi.DTO;
using NISApi.DTO.Response;
using NISApi.DTO.Request;
using NISApi.Data.Entity.SystemTables;
using NISApi.DTO.Response.SystemTables;
using NISApi.DTO.Request.SystemTables;
using NISApi.Data.Entity.Tasks;
using NISApi.DTO.Request.Users;
using NISApi.DTO.Response.Users;

namespace NISApi.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Person, CreatePersonRequest>().ReverseMap();
            CreateMap<Person, UpdatePersonRequest>().ReverseMap();
            CreateMap<Person, PersonQueryResponse>().ReverseMap();

            CreateMap<TableCollection, CreateCollectionRequest>().ReverseMap();
            CreateMap<TableCollection, UpdateCollectionRequest>().ReverseMap();
            CreateMap<TableCollection, CollectionQueryResponse>().ReverseMap();

            CreateMap<TableCountry, CreateCountryRequest>().ReverseMap();
            CreateMap<TableCountry, UpdateCountryRequest>().ReverseMap();
            CreateMap<TableCountry, CountryQueryResponse>().ReverseMap();

            CreateMap<TableDistrict, CreateDistrictRequest>().ReverseMap();
            CreateMap<TableDistrict, UpdateDistrictRequest>().ReverseMap();
            CreateMap<TableDistrict, DistrictQueryResponse>().ReverseMap();

            CreateMap<TableDocumentType, CreateDocumentTypeRequest>().ReverseMap();
            CreateMap<TableDocumentType, UpdateDocumentTypeRequest>().ReverseMap();
            CreateMap<TableDocumentType, DocumentTypeQueryResponse>().ReverseMap();

            CreateMap<TableEmploymentType, CreateEmploymentTypeRequest>().ReverseMap();
            CreateMap<TableEmploymentType, UpdateEmploymentTypeRequest>().ReverseMap();
            CreateMap<TableEmploymentType, EmploymentTypeQueryResponse>().ReverseMap();

            CreateMap<TableIndustry, CreateIndustryRequest>().ReverseMap();
            CreateMap<TableIndustry, UpdateIndustryRequest>().ReverseMap();
            CreateMap<TableIndustry, IndustryQueryResponse>().ReverseMap();

            CreateMap<TableMaritalStatus, CreateMaritalStatusRequest>().ReverseMap();
            CreateMap<TableMaritalStatus, UpdateMaritalStatusRequest>().ReverseMap();
            CreateMap<TableMaritalStatus, MaritalStatusQueryResponse>().ReverseMap();

            CreateMap<TableNationality, CreateNationalityRequest>().ReverseMap();
            CreateMap<TableNationality, UpdateNationalityRequest>().ReverseMap();
            CreateMap<TableNationality, NationalityQueryResponse>().ReverseMap();

            CreateMap<TableOccupation, CreateOccupationRequest>().ReverseMap();
            CreateMap<TableOccupation, UpdateOccupationRequest>().ReverseMap();
            CreateMap<TableOccupation, OccupationQueryResponse>().ReverseMap();

            CreateMap<TableParish, CreateParishRequest>().ReverseMap();
            CreateMap<TableParish, UpdateParishRequest>().ReverseMap();
            CreateMap<TableParish, ParishQueryResponse>().ReverseMap();

            CreateMap<TablePostalCode, CreatePostalCodeRequest>().ReverseMap();
            CreateMap<TablePostalCode, UpdatePostalCodeRequest>().ReverseMap();
            CreateMap<TablePostalCode, PostalCodeQueryResponse>().ReverseMap();

            CreateMap<TablePostOffice, CreatePostOfficeRequest>().ReverseMap();
            CreateMap<TablePostOffice, UpdatePostOfficeRequest>().ReverseMap();
            CreateMap<TablePostOffice, PostOfficeQueryResponse>().ReverseMap();

            CreateMap<TableStreet, CreateStreetRequest>().ReverseMap();
            CreateMap<TableStreet, UpdateStreetRequest>().ReverseMap();
            CreateMap<TableStreet, StreetQueryResponse>().ReverseMap();

            CreateMap<TableTaskStatus, CreateTaskStatusRequest>().ReverseMap();
            CreateMap<TableTaskStatus, UpdateTaskStatusRequest>().ReverseMap();
            CreateMap<TableTaskStatus, TaskStatusQueryResponse>().ReverseMap();

            CreateMap<TableTaskType, CreateTaskTypeRequest>().ReverseMap();
            CreateMap<TableTaskType, UpdateTaskTypeRequest>().ReverseMap();
            CreateMap<TableTaskType, TaskTypeQueryResponse>().ReverseMap();

            CreateMap<TableTaskPriority, CreateTaskPriorityRequest>().ReverseMap();
            CreateMap<TableTaskPriority, UpdateTaskPriorityRequest>().ReverseMap();
            CreateMap<TableTaskPriority, TaskPriorityQueryResponse>().ReverseMap();

            CreateMap<TableTaskReferenceType, CreateTaskReferenceTypeRequest>().ReverseMap();
            CreateMap<TableTaskReferenceType, UpdateTaskReferenceTypeRequest>().ReverseMap();
            CreateMap<TableTaskReferenceType, TaskReferenceTypeQueryResponse>().ReverseMap();


            CreateMap<PersonTask, CreatePersonTaskRequest>().ReverseMap();
            CreateMap<PersonTask, UpdatePersonTaskRequest>().ReverseMap();
            CreateMap<PersonTask, PersonTaskQueryResponse>().ReverseMap();

        }
    }
}
