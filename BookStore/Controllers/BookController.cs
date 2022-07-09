using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/book")] 
    public class BookController : ControllerBase
    {
        BookRepository _bookRepository = new();

        [HttpGet("list")]
        public IActionResult GetBooks(string? keyword = "", int pageIndex = 1, int pageSize = 10) 
        {
            var books = _bookRepository.GetBooks(pageIndex, pageSize, keyword);
            ListResponse<BookModel> bookList = new()
            {
                Results = books.Results.Select(b => new BookModel(b)),
                TotalRecords = books.TotalRecords

            };
            return Ok(bookList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
                return NotFound();

            return Ok(new BookModel(book));
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.CategoryId,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookRepository.AddBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.CategoryId,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookRepository.UpdateBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
                return BadRequest("id is null");

            var response = _bookRepository.DeleteBook(id);
            return Ok(response);
        }
    }
}
