using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using FluentValidation;
using FluentValidation.Results;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;



namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;


        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //*********** Second version 2nd ****************
        /*[HttpGet]

        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;

        }*/

        /*[HttpGet("{id}")]

        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id==id).SingleOrDefault();
            return book;

        }*/
        

        /*[HttpPost]

        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
                return BadRequest();

            _context.Books.Add(newBook);
            _context.SaveChanges();

            return Ok();

        }*/

        /*[HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
           var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if(book is null)
                return BadRequest();

            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            
            _context.SaveChanges();
            return Ok();
        } */


        /*[HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {

            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();    
        }*/


        //*********** Last version  ****************
        [HttpGet]

        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result); 

        }

        
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            
            BookDetailViewModel result;
            
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);

        }



        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
           
            command.Model = newBook;
            //Validation
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();


            //    if(!result.IsValid)
            //         foreach (var item in result.Errors)
            //             Console.WriteLine("??zellik: " + item.PropertyName + " - Error Message: " + item.ErrorMessage);
            //    else
            //         command.Handle(); 
            
            return Ok();

        }


        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        } 


        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);                
            command.Handle();
            
            return Ok();    

        }



        //*********** First version 1st ****************
        /*private static List<Book> BookList = new List<Book>()
        {

            new Book{
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, // Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12)

            },

            
            new Book{
                Id = 2,
                Title = "Herland",
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010,05,23)

            },

            
            new Book{
                Id = 3,
                Title = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2001,12,21)

            }

        };*/


        /*[HttpGet]

        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;

        }

        [HttpGet("{id}")]

        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id==id).SingleOrDefault();
            return book;

        }*/

        /*[HttpGet] //bunun yerine ilk olan kullan??lmas?? daha mant??kl??

        public Book Get([FromQuery] string id)
        { 
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;

        }*/

        /*[HttpPost]

        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
                return BadRequest();

            BookList.Add(newBook);
            return Ok();

        }

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);

            if(book is null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;   
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        } 


        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {

            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();

            BookList.Remove(book);
            return Ok();    

        }*/






    }
}
