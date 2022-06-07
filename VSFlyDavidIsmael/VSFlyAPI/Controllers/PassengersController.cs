using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VSFlyAPI.Extensions;
using VSFlyAPI.Models;
using VSFlyDavidIsmael;

namespace VSFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly VSFlyContext _context;

        public PassengersController(VSFlyContext context)
        {
            _context = context;
        }

        // GET: api/Passengers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengerM>>> GetPassengerSet()
        {

      var passengerList = await _context.PassengerSet.ToListAsync();
      List<PassengerM> passengerMs = new List<PassengerM>();

      foreach(Passenger p in passengerList)
      {
        var pM=p.convertToPassengerM();
        passengerMs.Add(pM);
      }

      return passengerMs;
        }

        // GET: api/Passengers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
            var passenger = await _context.PassengerSet.FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        // PUT: api/Passengers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return BadRequest();
            }

            _context.Entry(passenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Passengers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(PassengerM passenger)
        {
            var pp = passenger.convertToPassenger();
            _context.PassengerSet.Add(passenger.convertToPassenger());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassenger", new { id = pp.PassengerId }, passenger);
        }

        // DELETE: api/Passengers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            var passenger = await _context.PassengerSet.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.PassengerSet.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerExists(int id)
        {
            return _context.PassengerSet.Any(e => e.PassengerId == id);
        }
    }
}
