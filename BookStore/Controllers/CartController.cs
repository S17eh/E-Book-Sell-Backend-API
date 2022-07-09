using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
        [ApiController]
        [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository = new();

        public CartRepository CartRepository => _cartRepository;


        [HttpGet("list2")]
        public IActionResult GetCartItems(string? keyword)
        {
            List<Cart> carts = CartRepository.GetCartItems(keyword);
            IEnumerable<CartModel> cartModels = carts.Select(c => new CartModel(c));
            return Ok(cartModels);
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListResponse<CartModelResponse>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItem2(int UserId)
        {


            var cartitem = _cartRepository.GetCartListall(UserId);
            ListResponse<CartModelResponse> listResponce = new()
            {
                Results = cartitem.Results.Select(c => new CartModelResponse(c)),
                TotalRecords = cartitem.TotalRecords,
            };

            return Ok(listResponce);
        }

        [HttpPost("add")]
        public IActionResult AddCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new()
            {
                Id = model.Id,
                Quantity = 1,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = CartRepository.AddCart(cart);
            if(cart == null)
            {
                return StatusCode(500);
            }

            return Ok(new CartModel(cart));
        }

        [HttpPut("update")]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = CartRepository.UpdateCart(cart);

            return Ok(new CartModel(cart));
        }

        
        [HttpDelete("delete")]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
                return BadRequest();

            bool response = CartRepository.DeleteCart(id);
            return Ok(response);
        }
    }
}
