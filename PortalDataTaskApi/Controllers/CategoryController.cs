using Microsoft.AspNetCore.Mvc;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;
using PortalDataTask.Domain.ValueObject;

namespace PortalDataTaskApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : BaseController
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Status>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllStatus()
        {
            return Ok(_categoryService.GetAllStatus());
        }
    }
}
