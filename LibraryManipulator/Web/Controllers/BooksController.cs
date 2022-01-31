using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManipulator.Entities;
using LibraryManipulator.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LibraryManipulator.Web.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _repository;
        private readonly ILogger _logger;
        
        public BooksController(BookRepository repository)
        {
            _repository = repository;
            _logger = Log.ForContext<BooksController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            _logger.Information("Got get all books request");
            return await SafeInvoker.InvokeAsync(_repository.GetAllAsync);
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(string bookName)
        {
            _logger.Information("Got get {$bookName} book request", bookName);
            return await SafeInvoker.InvokeAsync(_repository.GetAsync, bookName);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            _logger.Information("Got add book request; book id = {$id}", book.Id);
            return await SafeInvoker.InvokeAsync(_repository.InsertAsync, book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooks(IEnumerable<Book> books)
        {
            var booksList = books as Book[] ?? books.ToArray();
            _logger.Information("Got add {$booksCount} books request", booksList.Length);
            return await SafeInvoker.InvokeAsync(_repository.InsertAsync, booksList);
        }
        
        [HttpDelete]
        public IActionResult DeleteBook(string bookName)
        {
            _logger.Information("Got delete {$bookName} book request", bookName);
            return SafeInvoker.Invoke(_repository.Delete, bookName);
        }
        
    }
}