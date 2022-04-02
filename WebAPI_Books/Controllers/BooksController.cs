using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using WebAPI_Books.Model;
using WebAPI_Books.Model.Dto;
using WebAPI_Books.Service;

namespace WebAPI_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;
        IHttpClientFactory _client;
        public BooksController(IHttpClientFactory client)
        {
            _client = client; 
            bookService = new BookService(client);
        }
        
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var books = bookService.getBooks().Result;
                if(books == null) throw new System.ArgumentNullException(nameof(books));

                return Ok(new
                {
                    data = books,
                    statusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getById/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var books = bookService.getBooksById(id).Result;
                if (books == null) throw new System.ArgumentNullException(nameof(books));

                return Ok(new
                {
                    data=books,
                    statusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost]
        [Route("postBook")]
        public IActionResult postBook(Books book)
        {
            try
            {
                var books = bookService.postBook(book).Result;
                if (books == null) throw new System.ArgumentNullException(nameof(books));
                
                return Created("",new
                {
                    data=books,
                    message="Registro insertado correctamente",
                    statusCode = (int)HttpStatusCode.Created
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("putBook/{id:int}")]
        public IActionResult putBook(int id, BookDto bookDto)
        {
            try
            {
                var books = bookService.putBook(id, bookDto).Result;
                if (books == null) throw new ArgumentNullException(nameof(books));

                return Ok(new
                {
                    data = books,
                    message = "Registro actualizado correctamente",
                    statusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("deleteBook/{id:int}")]
        public IActionResult deleteBook(int id)
        {
            try
            {
                var books = bookService.deleteBook(id);
                if (books == null) throw new ArgumentNullException(nameof(books));

                return Ok(new
                {
                    message = "Registro eliminado correctamente",
                    statusCode = (int)HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
