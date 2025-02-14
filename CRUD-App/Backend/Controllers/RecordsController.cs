using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            var records = DatabaseHelper.GetAllRecords();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public IActionResult GetRecordById(int id)
        {
            var record = DatabaseHelper.GetRecordById(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public IActionResult CreateRecord([FromBody] Record record)
        {
            if (record == null)
            {
                return BadRequest();
            }

            var id = DatabaseHelper.CreateRecord(record);
            return CreatedAtAction(nameof(GetRecordById), new { id = id }, record);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRecord(int id, [FromBody] Record record)
        {
            if (record == null || id != record.Id)
            {
                return BadRequest();
            }

            DatabaseHelper.UpdateRecord(record);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecord(int id)
        {
            DatabaseHelper.DeleteRecord(id);
            return NoContent();
        }
    }
}
