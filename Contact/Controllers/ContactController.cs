using ContactApi.Entities;
using ContactApi.Model.Contact;
using ContactApi.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        IContactService _contactService;
        public ContactController(IContactService service)
        {
            _contactService = service;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactService.GetAll();            
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<Contact> Get(Guid id)
        {
            return await _contactService.GetById(id);
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task Post([FromBody] CreateRequest value)
        {
            await _contactService.Create(value);
        }

        //// PUT api/<ContactController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._contactService.Delete(id);
        }
    }
}
