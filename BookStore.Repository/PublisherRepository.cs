using BookStore.Models.Models;
using BookStore.Models.ViewModels;

namespace BookStore.Repository
{
    public class PublisherRepository : BaseRepository
    {
        public ListResponse<Publisher> GetPublisher(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Publishers.Where(p => keyword == null || p.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Publisher> publisher = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Publisher>()
            {
                Results = publisher,
                TotalRecords = totalReocrds,
            };
        }

        public Publisher GetPublisher(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(c => c.Id == id);
            if (publisher == null)
                return null;
            return publisher;
        }

        public Publisher AddPublisher(Publisher Publisher)
        {
            var entry = _context.Publishers.Add(Publisher);
            _context.SaveChanges();
            return entry.Entity;
        }
        public Publisher UpdatePublisher(Publisher Publisher)
        {
            var entry = _context.Publishers.Update(Publisher);
            _context.SaveChanges();
            return entry.Entity;
        }
        public bool DeletePublisher(int id)
        {
            var Publisher = _context.Publishers.FirstOrDefault(c => c.Id == id);
            if (Publisher == null)
                return false;

            _context.Publishers.Remove(Publisher);
            _context.SaveChanges();
            return true;
        }
    }
}
