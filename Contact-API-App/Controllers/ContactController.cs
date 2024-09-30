using Contact_API_App.Services;
using Contact_API_App.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact_API_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        // GET: api/<ContactController>
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contacts = await _contactService.GetContactsAsync();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(id);

                if (contact == null)
                {
                    return NotFound(new { message = $"Contact with ID {id} not found." });
                }

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] Contact newContact)
        {
            try
            {
                var GetContact = await _contactService.CreateContactAsync(newContact);
                return CreatedAtAction(nameof(GetContact), new { id = GetContact.Id }, GetContact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact updatedContact)
        {
            if (id != updatedContact.Id)
            {
                return BadRequest(new { message = "ID mismatch." });
            }

            try
            {
                var contact = await _contactService.UpdateContactAsync(id, updatedContact);

                if (contact == null)
                {
                    return NotFound(new { message = $"Contact with ID {id} not found." });
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _contactService.DeleteContactAsync(id);

                if (!success)
                {
                    return NotFound(new { message = $"Contact with ID {id} not found." });
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
