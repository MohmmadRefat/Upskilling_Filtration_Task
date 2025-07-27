using Microsoft.AspNetCore.Mvc;
using Upskilling_Task.Models;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryContext libraryContext;

    public BookController(LibraryContext _libraryContext)
    {
        libraryContext = _libraryContext;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = libraryContext.Books.ToList();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBookById(int id)
    {
        var book = libraryContext.Books.FirstOrDefault(b => b.BookId == id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook(Book book)
    {
        libraryContext.Books.Add(book);
        libraryContext.SaveChanges();

        return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, Book bookFromRequest)
    {
        Book? bookFromDB = libraryContext.Books.FirstOrDefault(D => D.BookId == id);
        if (bookFromDB != null)
        {

            bookFromDB.Name = bookFromRequest.Name;
            bookFromDB.Description = bookFromRequest.Description;
            bookFromDB.Price = bookFromRequest.Price;
            bookFromDB.Author = bookFromRequest.Author;
            bookFromDB.Stock = bookFromRequest.Stock;
            bookFromDB.CategoryId = bookFromRequest.CategoryId;
            libraryContext.SaveChanges();
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        Book? bookFromDB = libraryContext.Books.FirstOrDefault(B => B.BookId == id);
        if (bookFromDB != null)
        {
            libraryContext.Books.Remove(bookFromDB);
            libraryContext.SaveChanges();
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
