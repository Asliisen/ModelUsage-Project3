using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;



namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    {
        private readonly BookStoreDbContext _context;

        public BookController (BookStoreDbContext context)
        { 
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")] 
            public IActionResult GetById (int id)
            {
                BookDetailViewModel result;
                try
                {
                    GetBookDetailQuery query = new GetBookDetailQuery(_context);
                    query.BookId = id;
                    result = query.Handle();
                }
                catch(Exception ex)
                {   
                  return BadRequest(ex.Message);
                }
              
                return Ok(result);
            }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel model)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model=model;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
            public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
            {

              try{
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;
                command.Handle();

              }
              catch(Exception ex){

                return BadRequest(ex.Message);

              }
                return Ok();
                
            }

        [HttpDelete("{id}")]//DELETE [FromBody] :: api/books/id
            public IActionResult DeleteBook(int id)
            {

              try{

                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                command.Handle();

              }
              catch(Exception ex){

                return BadRequest(ex.Message);

              }
                return Ok();
            }
    }
}