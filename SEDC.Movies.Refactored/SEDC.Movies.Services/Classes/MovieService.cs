using SEDC.Movies.Common.Exceptions;
using SEDC.Movies.DataAccess.Repositories.Interfaces;
using SEDC.Movies.DomainModels;
using SEDC.Movies.Mapper;
using SEDC.Movies.RequestModels;
using SEDC.Movies.RequestModels.Enums;
using SEDC.Movies.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.Movies.Services.Classes
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public void AddMovie(MovieRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Title))
            {
                throw new MovieException(requestModel.Id, requestModel.UserId, "Title field is required!");
            }

            if (requestModel.Year == null)
            {
                throw new MovieException(requestModel.Id, requestModel.UserId, "Year field is required!");
            }

            var Movie = MovieMapper.MapToMovie(requestModel);
            _movieRepository.Add(Movie);
        }


        public IEnumerable<MovieRequestModel> GetUserMovies(int userId)
        {
            
            var userMovies = _movieRepository.GetAll()
                                             .Where(x => x.UserId == userId);

            if (userMovies == null)
            {
                throw new MovieException();
            }

            var moviesRequestModel = new List<MovieRequestModel>();

            foreach (var movie in userMovies)
            {
                var movieRequestModel = MovieMapper.MapToMovieRequestModel(movie);
                moviesRequestModel.Add(movieRequestModel);
            }

            return moviesRequestModel;

        }


        public MovieRequestModel GetUserMovieById(int userId, int id)
        {
            var movie = _movieRepository.GetAll()
                                        .SingleOrDefault(x => x.UserId == userId && 
                                                              x.Id == id);


            if (movie == null)
            {
                throw new MovieException(id, userId, "Movie not found!");
            }

            return MovieMapper.MapToMovieRequestModel(movie);
        }


        public void DeleteMovieById(int userId, int id)
        {
            var movie = _movieRepository.GetAll()
                                        .SingleOrDefault(x => x.UserId == userId && 
                                                              x.Id == id);

            if (movie == null)
            {
                throw new MovieException(id, userId, "Movie not found!");
            }

            _movieRepository.Delete(movie);
        }


        public void UpdateMovie(MovieRequestModel requestModel)
        {
            var movie = _movieRepository.GetAll()
                                        .SingleOrDefault(x => x.UserId == requestModel.UserId && 
                                                              x.Id == requestModel.Id);

            if (movie == null)
            {
                throw new MovieException(movie.Id, movie.UserId, "Movie not found!");
            }

            if (!string.IsNullOrEmpty(requestModel.Title))
            {
                movie.Title = requestModel.Title;
            }

            if (!string.IsNullOrEmpty(requestModel.Description))
            {
                movie.Description = requestModel.Description;
            }

            if (requestModel.Year != 0)
            {
                movie.Year = (int)requestModel.Year;
            }

            _movieRepository.Update(movie);
        }
    }
}
