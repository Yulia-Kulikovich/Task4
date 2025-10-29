using Microsoft.AspNetCore.Mvc;
using LibraryApi.Models;
using LibraryApi.Data;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAll() => LibraryStorage.Books;

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Book title cannot be empty.");

            book.Id = LibraryStorage.Books.Count > 0 ? LibraryStorage.Books.Max(b => b.Id) + 1 : 1;
            LibraryStorage.Books.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book updatedBook)
        {
            var book = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updatedBook.Title))
                return BadRequest("Book title cannot be empty.");

            book.Title = updatedBook.Title;
            book.PublishedYear = updatedBook.PublishedYear;
            book.AuthorId = updatedBook.AuthorId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            LibraryStorage.Books.Remove(book);
            return NoContent();
        }
    }
}
