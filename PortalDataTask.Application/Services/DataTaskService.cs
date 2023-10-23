using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Validators;
using PortalDataTask.Domain.Interfaces.Repositories;
using PortalDataTask.Infra.CrossCutting.Services.Models;
using PortalDataTask.Infra.CrossCutting.Services.Services.Rabbit;
using System.Text;

namespace PortalDataTask.Application.Services;

public class DataTaskService : IDataTaskService
{
    private readonly IDataTaskRepository _dataTaskRepository;
    private readonly ILogger<DataTaskService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IRabbitPublishService _rabbitPublishService;

    public DataTaskService(
        IDataTaskRepository dataTaskRepository,
        IConfiguration configuration,
        IRabbitPublishService rabbitPublishService,
        ILogger<DataTaskService> logger)
    {
        _dataTaskRepository = dataTaskRepository;
        _configuration = configuration;
        _rabbitPublishService = rabbitPublishService;
        _logger = logger;
    }

    public async Task<BaseResponse> GetAllDataTask()
    {
        var response = new BaseResponse();

        _logger.LogInformation(@"{Class} | {Method} | Iniciando", nameof(DataTaskService), nameof(GetAllDataTask));

        var dataTasks = await _dataTaskRepository.GetAllAsync();

        if (dataTasks == null)
        {
            _logger.LogWarning(@"{Class} | {Method} | Data Task não encontrado", nameof(DataTaskService), nameof(GetAllDataTask));

            response.AddError(new ErrorResponse
            {
                Message = Resources.AppMessage.Validation_DataTaskNotFound
            }, System.Net.HttpStatusCode.BadRequest);

            return response;
        }

        response.AddData(MappertToGetAllResponse(dataTasks));
        
        _logger.LogInformation(@"{Class} | {Method} | Finalizando | Id: {id}", nameof(DataTaskService), nameof(GetAllDataTask));

        return response;
    }

    public async Task<BaseResponse> GetDataTaskById(long id)
    {
        var response = new BaseResponse();

        _logger.LogInformation(@"{Class} | {Method} | Iniciando | Id: {id}", nameof(DataTaskService), nameof(GetDataTaskById), id);

        var dataTask = await _dataTaskRepository.GetByIdAsync(id);

        if(dataTask == null)
        {
            _logger.LogWarning(@"{Class} | {Method} | Data Task não encontrado | Id: {id}", nameof(DataTaskService), nameof(GetDataTaskById), id);

            response.AddError(new ErrorResponse
            {
                Message = Resources.AppMessage.Validation_DataTaskNotFoundById
            }, System.Net.HttpStatusCode.BadRequest);

            return response;
        }

        response.AddData(new GetDataTaskResponse
        {
            Id = dataTask.Id,
            Description =dataTask.Description,
            ValidateDate= dataTask.ValidateDate,
            Status= dataTask.Status,
            CreatedAt = dataTask.CreatedAt,
            UpdatedAt = dataTask.UpdatedAt
        });

        _logger.LogInformation(@"{Class} | {Method} | Finalizando | Id: {id}", nameof(DataTaskService), nameof(GetDataTaskById), id);

        return response;        
    }

    public async Task<BaseResponse> GetByFilters(GetDataTasksByFiltersRequest request)
    {
        var response = new BaseResponse();

        _logger.LogInformation(@"{Class} | {Method} | Iniciando", nameof(DataTaskService), nameof(GetByFilters));

        var dataTasks = await _dataTaskRepository.GetByFilters(
            request?.Description,
            request?.ValidateDate,
            request?.FinalDate,
            request?.Status);

        if (dataTasks == null)
        {
            _logger.LogWarning(@"{Class} | {Method} | Dados não encontrado", nameof(DataTaskService), nameof(GetByFilters));

            response.AddError(new ErrorResponse()
            {
                Message = Resources.AppMessage.Validation_DataTaskNotFound

            }, System.Net.HttpStatusCode.BadRequest);

            return response;
        }

        response.AddData(MappertToFiltersResponse(dataTasks));

        _logger.LogInformation(@"{Class} | {Method} | Finalizando", nameof(DataTaskService), nameof(GetByFilters));

        return response;

    }

    public async Task<BaseResponse> CreateDataTaskAsync(CreateDataTaskRequest createDataTaskRequest)
    {
        var response = new BaseResponse();

        _logger.LogInformation(@"{Class} | {Method} | Iniciando", nameof(DataTaskService), nameof(CreateDataTaskAsync));

        var validationResult = await new CreateDataTaskValidator().ValidateAsync(createDataTaskRequest);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning(@"{Class} | {Method} | Request Invalido | Erros: {@errors}", nameof(DataTaskService), nameof(CreateDataTaskAsync), validationResult.Errors);

            foreach(var error in validationResult.Errors)
            {
                response.AddError(new ErrorResponse()
                {
                    Message = error.ErrorMessage
                }, System.Net.HttpStatusCode.BadRequest);
            }

            return response;
        }

        var existsDataTask = await _dataTaskRepository.GetByDescriptionAsync(createDataTaskRequest?.Description);

        if (existsDataTask != null)
        {
            _logger.LogWarning(@"{Class} | {Method} | Existe o data Task com a descrição: {description}", nameof(DataTaskService), nameof(CreateDataTaskAsync), createDataTaskRequest.Description);

            response.AddError(new ErrorResponse()
            {
                Message = Resources.AppMessage.Validation_AlreadyDescription
            }, System.Net.HttpStatusCode.BadRequest);

            return response;
        }

        var dataTask = new Domain.Entities.DataTask(
            0,
            createDataTaskRequest.Description,
            createDataTaskRequest.ValidateDate.Value,
            createDataTaskRequest.Status.Value,
            DateTime.Now,
            null);

        //await _dataTaskRepository.CreateAsync(dataTask);

        var messagePublish = new MessageSendModel
        {
            Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataTask))
        };

        await _rabbitPublishService.SendMessage(messagePublish, true);

        response.AddData(new CreateDataTaskResponse()
        {
            Id = dataTask.Id
        });

        _logger.LogInformation(@"{Class} | {Method} | Finalizando | Id: {id}", nameof(DataTaskService), nameof(CreateDataTaskAsync), dataTask.Id);

        return response;

    }

    public async Task<BaseResponse> UpdateDataTaskAsync(long id, UpdateDataTaskRequest updateDataTaskRequest)
    {
        var response = new BaseResponse();

        _logger.LogInformation(@"{Class} | {Method} | Iniciando", nameof(DataTaskService), nameof(UpdateDataTaskAsync));

        var validationResult = await new UpdateDataTaskValidator().ValidateAsync(updateDataTaskRequest, 
            _ => {
                ValidatorOptions.Global.LanguageManager.Enabled = false; }
            );

        if (!validationResult.IsValid)
        {
            _logger.LogWarning(@"{Class} | {Method} | Request Invalido | Erros: {@errors}", nameof(DataTaskService), nameof(UpdateDataTaskAsync), validationResult.Errors);

            foreach (var error in validationResult.Errors)
            {
                response.AddError(new ErrorResponse()
                {
                    Message = error.ErrorMessage
                }, System.Net.HttpStatusCode.BadRequest);
            }

            return response;
        }

        var existsDataTask = await _dataTaskRepository.GetByDescriptionAsync(updateDataTaskRequest?.Description);

        if (existsDataTask != null && existsDataTask!.Id != id)
        {
            _logger.LogWarning(@"{Class} | {Method} | Já existe o data Task para outro Id | id: {id} | descrição: {description}", nameof(DataTaskService), nameof(UpdateDataTaskAsync), existsDataTask.Id, existsDataTask.Description);

            response.AddError(new ErrorResponse()
            {
                Message = Resources.AppMessage.Validation_AlreadyDescriptionOtherId
            }, System.Net.HttpStatusCode.BadRequest);

            return response;
        }

        existsDataTask = await _dataTaskRepository.GetByIdAsync(id);

        if(existsDataTask == null)
        {
            response.AddError(new ErrorResponse()
            {
                Message = Resources.AppMessage.Validation_DataTaskNotFoundById
            }, System.Net.HttpStatusCode.BadRequest);

            return response;
        }

        var updateDataTask = new Domain.Entities.DataTask(
            existsDataTask.Id,
            updateDataTaskRequest.Description,
            updateDataTaskRequest.ValidateDate.HasValue ?
                updateDataTaskRequest.ValidateDate.Value :
                existsDataTask.ValidateDate,
            updateDataTaskRequest.Status.HasValue ?
                updateDataTaskRequest.Status.Value :
                existsDataTask.Status,
            existsDataTask.CreatedAt,
            DateTime.Now);

        
        var messagePublish = new MessageSendModel
        {
            Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(updateDataTask))
        };

        await _rabbitPublishService.SendMessage(messagePublish, true);

        //await _dataTaskRepository.UpdateAsync(dataTask);

        response.AddData(new UpdateDataTaskResponse()
        {
            Description = updateDataTask.Description,
            ValidateDate= updateDataTask.ValidateDate,
            Status = updateDataTask.Status,
            CreatedAt = updateDataTask.CreatedAt,
            UpdatedAt = updateDataTask.UpdatedAt
        });

        _logger.LogInformation(@"{Class} | {Method} | Finalizando | Id: {id}", nameof(DataTaskService), nameof(UpdateDataTaskAsync), updateDataTask.Id);

        return response;
    }

    private List<GetDataTasksByFiltersResponse> MappertToFiltersResponse(List<Domain.Entities.DataTask> dataTasks)
    {
        var responses = new List<GetDataTasksByFiltersResponse>();

        foreach (var dataTask in dataTasks)
        {
            var response = new GetDataTasksByFiltersResponse()
            {
                Id = dataTask.Id,
                Description = dataTask.Description,
                ValidateDate = dataTask.ValidateDate,
                Status = dataTask.Status,
                CreatedAt = dataTask.CreatedAt,
                UpdatedAt = dataTask.UpdatedAt
            };

            responses.Add(response);
        }

        return responses;
    }

    private List<GetDataTaskResponse> MappertToGetAllResponse(List<Domain.Entities.DataTask> dataTasks)
    {
        var responses = new List<GetDataTaskResponse>();

        foreach (var dataTask in dataTasks)
        {
            var response = new GetDataTaskResponse()
            {
                Id = dataTask.Id,
                Description = dataTask.Description,
                ValidateDate = dataTask.ValidateDate,
                Status = dataTask.Status,
                CreatedAt = dataTask.CreatedAt,
                UpdatedAt = dataTask.UpdatedAt
            };

            responses.Add(response);
        }

        return responses;
    }
}
