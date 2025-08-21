using FirstAPI.Data;
using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    // This attribute sets the base route for all actions in this controller.
    // [controller] is a placeholder that will be replaced with the controller name minus "Controller".
    // For example, BooksController → "books", so the base route becomes "/api/books".
    [Route("api/[controller]")]

    // This tells ASP.NET Core that this class is a Web API controller.
    // It automatically enables features like model binding, validation, and API-specific behaviors.
    [ApiController]

    // The controller class itself. All API actions related to "Book" resources go here.
    // Inherits from ControllerBase, which provides helper methods like Ok(), NotFound(), etc.
    public class BooksController : ControllerBase
    {
        static private List<Book> books = new List<Book>
        {
            //new Book
            //{
            //    Id = 1,
            //    Title = "The Great Gatsby",
            //    Author = "F. Scott Fitzgerald",
            //    YearPublished = "1925"
            //},
            //new Book
            //{
            //    Id = 2,
            //    Title = "Moby-Dick",
            //    Author = "Herman Melville",
            //    YearPublished = "1851"
            //}
        };

        // Injects the EF Core database context (FirstAPIContext) so the controller can access the database via _context.
        private readonly FirstAPIContext _context;
        public BooksController(FirstAPIContext context)
        {
            _context = context;
        }
        [HttpGet]
        // Async GET endpoint that returns a list of books inside ActionResult.
        // Task = asynchronous (like a Promise in JavaScript).
        // ActionResult lets you return data or HTTP status codes.
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book != null ? Ok(book) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();
                // Return 201 Created, include the Location header pointing to the new book's URL, and return the book in the response body.
                return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
            }
        }

        [HttpPut("{id}")]
        // IActionResult: can return any HTTP response.
        // ActionResult<T>: can return a typed object (T) or any HTTP response.
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
