using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public List<Cart> GetCartItems(string? keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            return query.ToList();
        }

        public Cart GetCarts(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public ListResponse<Cart> GetCartListall(int UserId)
        {

            var query = _context.Carts.Include(c => c.Book).Where(c => c.Userid == UserId).AsQueryable();

            int totalReocrds = query.Count();
            List<Cart> carts = query.ToList();

            return new ListResponse<Cart>()
            {
                Results = carts,
                TotalRecords = totalReocrds,
            };
        }

        public Cart AddCart(Cart cart)
        {
            {
                var query = _context.Carts.FirstOrDefault(c => c.Bookid == cart.Bookid && c.Userid == cart.Userid);
                if (query == null)
                {
                    var entry = _context.Carts.Add(cart);
                    _context.SaveChanges();
                    return entry.Entity;
                }
                else
                {
                    return null;
                }
            }
        }

        public Cart UpdateCart(Cart cart)
        {
            var entry = _context.Carts.Update(cart);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
