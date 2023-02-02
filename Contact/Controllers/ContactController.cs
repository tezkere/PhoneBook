using ContactApi.Model.Contact;
using ContactApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactController>
        [HttpGet("getAllContact")]
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

        // GET: api/<ContactController>
        [HttpGet("getReport")]
        public async Task<IActionResult> getReport()
        {
            try
            {
                var report = await _contactService.GetReportInfo();
                return await Task.FromResult(Ok(report));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}", Name = "ContactId")]
        public async Task<IActionResult> GetContactId(Guid id)
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
        [HttpPost("createContact")]
        public async Task<IActionResult> CreateContact([FromBody] CreateRequestForContact createRequest)
        {
            try
            {
                var result = await _contactService.Create(createRequest);
                return CreatedAtRoute("ContactId", new { id = result.UUID }, result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("createMultipleContact")]
        public async Task<IActionResult> CreateMultipleContact([FromBody] List<CreateRequestForContact> createRequest)
        {
            try
            {
                var result = await _contactService.CreateMultiple(createRequest);
                return CreatedAtRoute("ContactIds", new { id = result.First().UUID }, result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("deleteContact/{id}")]
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

        // POST api/<ContactController>
        [HttpPost("createContactInfo")]
        public async Task<IActionResult> CreateContactInfo([FromBody] Model.ContactInfo.CreateRequestForContactInfo createRequest)
        {
            try
            {
                var result = await _contactService.CreateContactInfo(createRequest);
                return Ok(result.UUID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
