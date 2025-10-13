using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase {

        private readonly VideoGameDbContext _context;
        public VideoGameController(VideoGameDbContext context) {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAll() {
            return Ok(await _context.VideoGames.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGamebyId(int id) {
            var game = await _context.VideoGames.FindAsync(id);
            if (game == null) {
                return NotFound();
            }
            return Ok(game);

        }
        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame) {
            if (newGame is null) {
                return BadRequest();
            }
            //Starts tracking the entity and marks it for insertion into the database when SaveChangesAsync is called
            
            _context.VideoGames.Add(newGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVideoGamebyId), new { id = newGame.Id }, newGame); //returns  201 Created response with location header and the created object
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateVideoGame(int id, VideoGame updatedGame) {
            var existingGame = await _context.VideoGames.FindAsync(id);
            if (existingGame == null) {
                return NotFound();
            }

            existingGame.Title = updatedGame.Title;
            existingGame.Platform = updatedGame.Platform;
            existingGame.Developer = updatedGame.Developer;
            existingGame.Publisher = updatedGame.Publisher;
            await _context.SaveChangesAsync();
            return NoContent(); //returns 204 No Content response


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id) {

            var existingGame = await _context.VideoGames.FindAsync(id);
            if (existingGame == null) {
                return NotFound();
            }
            _context.VideoGames.Remove(existingGame);
            await _context.SaveChangesAsync();
            return NoContent(); //returns 204 No Content response

        }

    }


}
