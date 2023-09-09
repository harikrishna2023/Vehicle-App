using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;
using VehicleApp.API.Repositories;
using VehicleApp.API.Repositories.IRepositories;
using Microsoft.AspNetCore.Hosting;

namespace VehicleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CategoryController(ICategoryRepository _categoryRepository ,
            IMapper _mapper,
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
                this.categoryRepository = _categoryRepository;
            this.mapper = _mapper;
          this._env=env;
            this.httpContextAccessor = httpContextAccessor;
        }
       
       
        [HttpPost]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] int id)
        {
            try
            {


                var categoryDomainModel = await categoryRepository.DeleteCategory(id);
                if (categoryDomainModel == null)
                {
                    return NotFound();

                }
             //   var categoryDTO = mapper.Map<CategoryDTO>(categoryDomainModel);
                return Ok(categoryDomainModel);
            }
            catch(Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }
        }

        [HttpGet]
        [Route("ListCategory")]
        public async Task<IActionResult> ListCategory()
        {
            try
            {


                //retrieving the list
                var categoryModel = await categoryRepository.ListCategory();

                return Ok(categoryModel);
            }
            catch(Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }
        }
        [HttpPost]
        [Route("AddItem")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddCategories([FromForm] AddCategoryTemp temp)
        {
            try
            {

            
            var categoryData = await categoryRepository.AddCategoryTemp(temp);

            return Ok(categoryData);
        }
            catch(Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }

        }

        [HttpPost]
        [Route("EditItem")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditCategoryItem([FromForm] AddCategoryTemp temp)
        {
            try { 
            var categoryData = await categoryRepository.EditCategoryTemp(temp);

            return Ok(categoryData);
        }
            catch(Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }

        }
        [HttpPost]
        [Route("saveData")]
        public async Task<IActionResult> saveData()
        {
            try { 
            var categoryData = await categoryRepository.submitData();
            return Ok(categoryData);
        }
            catch(Exception ex)
            {
                return BadRequest(new { StatusMessage = ex.Message, StatusCode = 400 });
            }
        }
       



    }
}
