using BookStore.Models.Models;
using BookStore.Models.ViewModels;

namespace BookStore.Repository
{
    public class BookRepository  : BaseRepository
    {
        public ListResponse<Book> GetBooks( int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Where(b => keyword == null || b.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new ListResponse<Book>()
            {
                Results = books,
                TotalRecords = totalRecords
            };
        }
        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}
