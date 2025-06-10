using Microsoft.AspNetCore.Mvc;

namespace WebServiceExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LibraryController : ControllerBase
    {
        private static List<Book> books = new List<Book>()
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell" },
            new Book { Id = 2, Title = "Malý princ", Author = "Antoine de Saint-Exupéry" }
        };

        [HttpGet("book/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound("Kniha nebyla nalezena.");
            return Ok(book);
        }

        [HttpPost("addbook")]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            newBook.Id = books.Max(b => b.Id) + 1;
            books.Add(newBook);
            return Ok(newBook);
        }

        [HttpPut("updateauthor")]
        public IActionResult UpdateBookAuthor([FromBody] UpdateAuthorModel update)
        {
            var book = books.FirstOrDefault(b => b.Id == update.Id);
            if (book == null)
                return NotFound("Kniha nebyla nalezena.");

            book.Author = update.NewAuthor;
            return Ok(book);
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public class UpdateAuthorModel
    {
        public int Id { get; set; }
        public string NewAuthor { get; set; }
    }
}
