using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Data.DBContext;
using FitnessApp.Domain.Entitities;
using FitnessApp.API;
using FitnessApp.Domain.Entities.Base;

namespace FitnessApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FitnessAppContext _context;

        public UsersController(FitnessAppContext context)
        {
            _context = context;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName
                })
                .ToListAsync();

            return Ok(users);
        }


        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.SportActivities)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                SportActivities = user.SportActivities.Select(sa => new SportActivityDTO
                {
                    Id = sa.Id,
                    Distance = sa.Distance,
                    TimeTaken = sa.TimeTaken,
                    ActivityDate = sa.ActivityDate,
                    Feeling = sa.Feeling,
                    ActivityType = sa.ActivityType,
                    StartTime = sa.StartTime,
                    EndTime = sa.EndTime,
                    UserId = sa.UserId
                }).ToList()
            };

            return Ok(userDTO);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = userDTO.UserName;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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



        // POST: api/Users/{id}/activities
        [HttpPost("{id}/activities")]
        public async Task<IActionResult> AddActivity(int id, [FromBody] SportActivityDTO activityDTO)
        {
            var user = await _context.Users.Include(u => u.SportActivities).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var activity = new SportActivity
            {
                Id = Guid.NewGuid(),
                Distance = activityDTO.Distance,
                TimeTaken = activityDTO.TimeTaken,
                ActivityDate = activityDTO.ActivityDate,
                Feeling = activityDTO.Feeling,
                ActivityType = activityDTO.ActivityType,
                StartTime = activityDTO.StartTime,
                EndTime = activityDTO.EndTime,
                UserId = id
            };

            user.SportActivities.Add(activity);
            await _context.SaveChangesAsync();

            var createdActivityDTO = new SportActivityDTO
            {
                Id = activity.Id,
                Distance = activity.Distance,
                TimeTaken = activity.TimeTaken,
                ActivityDate = activity.ActivityDate,
                Feeling = activity.Feeling,
                ActivityType = activity.ActivityType,
                StartTime = activity.StartTime,
                EndTime = activity.EndTime,
                UserId = activity.UserId
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdActivityDTO);
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.SportActivities)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private static UserDTO UserToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                SportActivities = user.SportActivities.Select(activity => new SportActivityDTO
                {
                    Id = activity.Id,
                    Distance = activity.Distance,
                    TimeTaken = activity.TimeTaken,
                    ActivityDate = activity.ActivityDate,
                    Feeling = activity.Feeling,
                    ActivityType = activity.ActivityType,
                    StartTime = activity.StartTime,
                    EndTime = activity.EndTime,
                    SensorData = activity.SensorData
                }).ToList()
            };
        }
    }
}
