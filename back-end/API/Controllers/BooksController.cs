using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Books")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _bookService;
        private APIMessage ret { get; set; }
        public BooksController(BooksService bookService)
        {
            _bookService = bookService;
            ret = new APIMessage();
        }

        [HttpGet]
        public ActionResult<APIMessage> GetBooks()
        {
            var books = _bookService.All();
            if (books.Count == 0) return NotFound(APIMessage.NotFound(books));
            return Ok(APIMessage.Ok(books));
        }
        [HttpGet("s")]
        public ActionResult<APIMessage> SearchBooks(string term)
        {
            var books = new List<Books>();
            books.AddRange(_bookService.BooksByAuthor(term));
            books.AddRange(_bookService.BooksByTitle(term));
            int id = 0;
            if(int.TryParse(Regex.Replace(term, "[^0-9]", ""), out id))
            {
                books.Add(_bookService.BookById(id));
            }
            if (books.Count == 0) return NotFound(APIMessage.NotFound(books));
            
            return Ok(APIMessage.Ok(books));
        }
        [HttpGet("{id}/GetDeliveryTax")]
        public ActionResult<APIMessage> GetDeliveryTax(int id)
        {
            var book = _bookService.BookById(id);
            if (book == null) return APIMessage.NotFound(book);
            
            decimal tax = book.Price * 0.2m; // Calcula o valor do frete (20% do preço)
            Dictionary<string, decimal> data = new Dictionary<string, decimal>
            {
                { "frete", tax }
            };
            ret.Data = data;
            return Ok(APIMessage.Ok(ret));
        }
    }
}