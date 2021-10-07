using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.Movies.Common.Exceptions;
using SEDC.Movies.RequestModels;
using SEDC.Movies.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SEDC.Movies.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService noteService)
        {
            _movieService = noteService;
        }

        [HttpPost]
        [Route("CreateMovie")]
        public IActionResult CreateMovie([FromBody] MovieRequestModel requestModel)
        {
            try
            {
                requestModel.UserId = GetAuthorziedUserId();
                _movieService.AddMovie(requestModel);

                Log.Information($"Movie succesffuly created date: {DateTime.UtcNow}");
                return Ok();
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (MovieException ex)
            {
                Log.Error("MOVIE {noteId} for user {userId}: {message}", ex.MovieId, ex.UserId, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }

        [HttpGet]
        [Route("GetMovie")]
        public IActionResult GetMovies()
        {
            try
            {
                var userId = GetAuthorziedUserId();
                var response = _movieService.GetUserMovies(userId);

                Log.Information($"Movie succesffuly retrieved notes on {DateTime.UtcNow}");
                return Ok(response);
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (MovieException ex)
            {
                Log.Error("NOTE {movieId} for user {userId}: {message}", ex.MovieId, ex.UserId, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }

        [HttpGet]
        [Route("GetMovieById")]
        public IActionResult GetNoteById([FromQuery] int id)
        {
            try
            {
                var userId = GetAuthorziedUserId();
                var response = _movieService.GetUserMovieById(userId, id);
                return Ok(response);
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (MovieException ex)
            {
                Log.Error("NOTE {movieId} for user {userId}: {message}", ex.MovieId, ex.UserId, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }


        [HttpDelete]
        [Route("DeleteMovieById")]
        public IActionResult DeleteNoteById([FromQuery] int id)
        {
            try
            {
                var userId = GetAuthorziedUserId();
                _movieService.DeleteMovieById(userId, id);
                return Ok();
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (MovieException ex)
            {
                Log.Error("NOTE {noteId} for user {userId}: {message}", ex.MovieId, ex.UserId, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }


        [HttpPut]
        [Route("UpdateMovie")]
        public IActionResult UpdateNote([FromBody] MovieRequestModel requestModel)
        {
            try
            {
                requestModel.UserId = GetAuthorziedUserId();
                _movieService.UpdateMovie(requestModel);
                return Ok();
            }
            catch (UserException ex)
            {
                Log.Error("USER {userId}.{name}: {message}", ex.UserId, ex.Name, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (MovieException ex)
            {
                Log.Error("MOVIE {noteId} for user {userId}: {message}", ex.MovieId, ex.UserId, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("UNKNOWN Error: {message}", ex.Message);
                return BadRequest("Something went wrong. Please contact support!");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("testLogger")]
        public IActionResult TestLogger()
        {
            Log.Information("text about infgormation log!");

            Log.Warning("text about warning log!");

            Log.Error("text about ERROR log!");

            return Ok();
        }


        private int GetAuthorziedUserId()
        {
            bool userIdExists = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);

            if (!userIdExists)
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new UserException(userId, name, "Name identifier claim does not exist!");
            }

            return userId;
        }
    }
}