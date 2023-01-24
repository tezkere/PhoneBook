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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contacts = await _contactService.GetAll();
                return await Task.FromResult(Ok(contacts));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var contact = await _contactService.GetById(id);

                if (contact is null)
                {                    
                    return NotFound();
                }
                else
                {
                    return Ok(contact);
                }                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRequest createRequest)
        {
            try
            {
                var result = await _contactService.Create(createRequest);
                return CreatedAtRoute("ContactId", new { id = result });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _contactService.Delete(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
