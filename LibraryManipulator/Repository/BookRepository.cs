using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using LibraryManipulator.Entities;
using Microsoft.Extensions.Options;

namespace LibraryManipulator.Repository
{
    public class BookRepository : DbContext
    {
        public BookRepository(IOptions<LibrarySettings> settings) : base(settings.Value.ConnectionString){ }

        public async Task InsertAsync(Book book)
        {
            Books.Add(book);
            await SaveChangesAsync();
        }

        public async Task InsertAsync(IEnumerable<Book> books)
        {
            foreach (var book in books)
                Books.Add(book);
            await SaveChangesAsync();
        }

        public async Task<Book> GetAsync(string bookName)
        {
            foreach (var book in Books)
                if (book.Name == bookName)
                    return await Books.FindAsync(book);
            return null;
        }

        public void Delete(string bookName)
        {
            foreach (var book in Books)
                if (book.Name == bookName)
                {
                    Books.Remove(book);
                    return;
                }
        }
        
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await Books.ToArrayAsync();
        }

        private DbSet<Book> Books { get; set; }
    }
}