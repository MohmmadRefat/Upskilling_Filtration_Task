using Microsoft.AspNetCore.Mvc;
using Upskilling_Task.DTOs;
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

        var books = libraryContext.Books.Select(B =>  new GetBooksDTO { Name = B.Name,
                                                                        Description = B.Description,
                                                                        Price = B.Price,
                                                                        Author = B.Author,
                                                                        BookId = B.BookId,
                                                                        CategoryId = B.CategoryId }).ToList();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBookById(int id)
    {
        var book = libraryContext.Books.FirstOrDefault(b => b.BookId == id);
        if (book == null)
            return NotFound();
        GetBooksDTO bookDTO = new GetBooksDTO
        {
            BookId = book.BookId,
            Name = book.Name,
            Description = book.Description,
            Author = book.Author,
            Price = book.Price,
            Stock = book.Stock,
            CategoryId = book.CategoryId
        };
        return Ok(bookDTO);
    }

    [HttpPost]
    public IActionResult AddBook(BookDTO bookDTO)
    {
        Book book= new Book
        {
            Name = bookDTO.Name,
            Description = bookDTO.Description,
            Author = bookDTO.Author,
            Price = bookDTO.Price,
            Stock = bookDTO.Stock,
            CategoryId = bookDTO.CategoryId
        };

        libraryContext.Books.Add(book);
        libraryContext.SaveChanges();

        return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook( GetBooksDTO bookFromRequest)
    {
        Book? bookFromDB = libraryContext.Books.FirstOrDefault(D => D.BookId == bookFromRequest.BookId);
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
