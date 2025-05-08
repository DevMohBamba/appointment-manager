using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentManager.Api.Models;
using AppointmentManager.Api.Data;

namespace AppointmentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get() =>
            await _context.Appointments.OrderBy(a => a.Date).ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Appointment>> Post(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Post), new { appointment.Id }, appointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);
            if (appt is null) return NotFound();

            _context.Appointments.Remove(appt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appt)
        {
            if (id != appt.Id) return BadRequest();

            var existing = await _context.Appointments.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Title = appt.Title;
            existing.Description = appt.Description;
            existing.Date = appt.Date;
            existing.Location = appt.Location;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
