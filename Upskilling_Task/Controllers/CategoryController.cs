using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upskilling_Task.DTOs;
using Upskilling_Task.Models;

namespace Upskilling_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly LibraryContext libraryContext;

        public CategoryController(LibraryContext _libraryContext)
        {
            libraryContext = _libraryContext;
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {
            
            var categories = libraryContext.Categories.Select(c=> new CategoryDTO { Id=c.CategoryId,Name=c.Name,Description=c.Description}).ToList();
            
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = libraryContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Id = category.CategoryId,
                Name = category.Name,
                Description=category.Description
            };
            if (category == null)
                return NotFound();

            return Ok(categoryDTO);
        }

        [HttpPost]
        public IActionResult AddCategory(CreateCategoryDTO category)
        {
            Category category1 = new Category
            {   
                Name = category.Name,
                Description = category.Description
            };
            libraryContext.Categories.Add(category1);
            libraryContext.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category1.CategoryId }, category1);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(CategoryDTO categoryFromRequest)
        {
            var categoryFromDB = libraryContext.Categories.FirstOrDefault(c => c.CategoryId == categoryFromRequest.Id);
            if (categoryFromDB != null)
            {

                categoryFromDB.Name = categoryFromRequest.Name;
                categoryFromDB.Description = categoryFromRequest.Description;
                
                libraryContext.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryFromDB = libraryContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (categoryFromDB != null)
            {
                
                libraryContext.Categories.Remove(categoryFromDB);
                libraryContext.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
