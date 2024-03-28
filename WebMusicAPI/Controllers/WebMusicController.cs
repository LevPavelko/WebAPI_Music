using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMusicAPI.Models;
using WebMusicAPI.ViewModel;

namespace WebMusicAPI.Controllers
{
    [ApiController]
    [Route("api/Music")]
    public class WebMusicController : ControllerBase
    {
        private readonly WebMusicContext _context;

        public WebMusicController(WebMusicContext context)
        {
            _context = context;
        }
        // GET: api/media
        [HttpGet("Media")]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedia()
        {
            return await _context.media.ToListAsync();

        }

        [HttpPost("Media")]
        public async Task<ActionResult<Media>> PostMedia(MediaViewModel m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            Users user = await _context.users.FindAsync(m.Id_User);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            Genre genre = await _context.genre.FirstOrDefaultAsync(g => g.Name == m.Genre);
            if (genre == null)
            {
                return BadRequest("Genre not found.");
            }

            Executor executor = await _context.executor.FirstOrDefaultAsync(e => e.Name == m.Executor);
            if (executor == null)
            {
                return BadRequest("Executor not found.");
            }

           
            Media media = new Media
            {
                Title = m.Title,
                id_Genre = genre.Id, 
                id_Executor = executor.Id, 
                Path = m.Path,
                Id_User = user.Id
            };

            _context.media.Add(media);

            
            await _context.SaveChangesAsync();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var json = JsonSerializer.Serialize(media, options);

            return Ok(json);
        }

        [HttpPut("Media")]
        public async Task<ActionResult<Media>> PutMediae(MediaViewModel m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Users user = await _context.users.FindAsync(m.Id_User);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            Genre genre = await _context.genre.FirstOrDefaultAsync(g => g.Name == m.Genre);
            if (genre == null)
            {
                return BadRequest("Genre not found.");
            }

            Executor executor = await _context.executor.FirstOrDefaultAsync(e => e.Name == m.Executor);
            if (executor == null)
            {
                return BadRequest("Executor not found.");
            }

            var media = await _context.media.FindAsync(m.Id);
            if (media == null)
            {
                return NotFound();
            }

            media.Title = m.Title;
            media.id_Genre = genre.Id;
            media.id_Executor = executor.Id;
            media.Id_User = user.Id;
            media.Path = m.Path;

            _context.Update(media);
            await _context.SaveChangesAsync();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var json = JsonSerializer.Serialize(media, options);

            return Ok(json);
        }
        [HttpDelete("Media/{id}")]
        public async Task<ActionResult<Media>> DeleteMedia(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var media = await _context.media.SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            _context.media.Remove(media);
            await _context.SaveChangesAsync();

            return Ok(media);
        }

        //GET: api/Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await _context.users
            .Select(u => new Users
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Status = u.Status
               
            })
            .ToListAsync();

            return users;
        }
       
        //POST: api/Users/Add
        [HttpPost("Users")]
        public async Task<ActionResult<Users>> PostUser(UserViewModel u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Users user = new Users();
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Email = u.Email;
            user.Login = u.Login;
            user.Password = u.Password;
            user.Salt = u.Salt;
            user.Status = u.Status;
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        //PUT: api/Users
        [HttpPut("Users")]
        public async Task<ActionResult<Users>> PutUser(UserViewModel u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.users.FindAsync(u.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Email = u.Email;
            user.Login = u.Login;
            user.Status = u.Status;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        // DELETE: api/Users/3
        [HttpDelete("User/{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        //GET: api/Genre
        [HttpGet("Genre")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenre()
        {
            return await _context.genre.ToListAsync();
        }

        //POST: api/Genre/Add
        [HttpPost("Genre")]
        public async Task<ActionResult<Genre>> PostGenre(GenreViewModel g)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Genre genre = new Genre();
            //genre.Id = g.Id;
            genre.Name = g.Name;
            _context.genre.Add(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }
        //PUT: api/Genre/Add
        [HttpPut("Genre")]
        public async Task<ActionResult<Genre>> PutGenre(GenreViewModel g)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await _context.genre.FindAsync(g.Id);
            if (genre == null)
            {
                return NotFound();
            }

            genre.Name = g.Name;
           _context.Update(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }
        // DELETE: api/Genre/3
        [HttpDelete("Genre/{id}")]
        public async Task<ActionResult<Genre>> DeleteGenre(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _context.genre.SingleOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.genre.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

    }
}
