using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var categories = libraryContext.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = libraryContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            libraryContext.Categories.Add(category);
            libraryContext.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(int id, Category categoryFromRequest)
        {
            var categoryFromDB = libraryContext.Categories.FirstOrDefault(c => c.CategoryId == id);
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
