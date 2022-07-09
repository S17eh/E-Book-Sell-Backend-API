using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
        [Route("api/publisher")]
    public class PublisherController : ControllerBase
    {
        readonly PublisherRepository _publisherRepository = new();

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListResponse<PublisherModel>), (int)HttpStatusCode.OK)]

        public IActionResult GetPublisher(string? keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            var categories = _publisherRepository.GetPublisher(pageIndex, pageSize, keyword);
            ListResponse<PublisherModel> listResponse = new()
            {
                Results = categories.Results.Select(c => new PublisherModel(c)),
                TotalRecords = categories.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ListResponse<PublisherModel>), (int)HttpStatusCode.OK)]

        public IActionResult GetPublisher(int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);
            if (publisher == null)
                return (BadRequest("Publisher not found !!"));
            PublisherModel publisherModel = new(publisher);

            return Ok(publisherModel);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddPublisher(PublisherModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Publisher publisher = new()
            { 
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,

        };
            var response = _publisherRepository.AddPublisher(publisher);
            PublisherModel publisherModel = new(response);

            return Ok(publisherModel);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Publisher publisher = new()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };
            var response = _publisherRepository.UpdatePublisher(publisher);
            PublisherModel publisherModel = new(response);

            return Ok(publisherModel);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)] 
        public IActionResult DeletePublisher(int id)
        {
            if (id == 0)
                return BadRequest("id is null");

            var response = _publisherRepository.DeletePublisher(id);
            return Ok(response);
        }
        
    }
}
