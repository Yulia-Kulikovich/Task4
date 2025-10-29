using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Data;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Author>> GetAll() => LibraryStorage.Authors;

        [HttpGet("{id}")]
        public ActionResult<Author> GetById(int id)
        {
            var author = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();
            return author;
        }

        [HttpPost]
        public ActionResult<Author> Create(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                return BadRequest("Author name cannot be empty.");

            author.Id = LibraryStorage.Authors.Count > 0 ? LibraryStorage.Authors.Max(a => a.Id) + 1 : 1;
            LibraryStorage.Authors.Add(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Author updatedAuthor)
        {
            var author = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updatedAuthor.Name))
                return BadRequest("Author name cannot be empty.");

            author.Name = updatedAuthor.Name;
            author.DateOfBirth = updatedAuthor.DateOfBirth;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();

            LibraryStorage.Authors.Remove(author);
            return NoContent();
        }
    }
}
