using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/public")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        UserRepository _repository = new();

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            User user = _repository.Login(model);
            if (user == null)
                return NotFound("Invalid User !");

            UserModel userModel = new(user);
            return Ok(userModel);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            User user = _repository.Register(model);
            UserModel userModel = new(user);
            return Ok(userModel);
        }
    }
}
