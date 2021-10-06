using SEDC.Movies.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.Services.Interfaces
{
    public interface IMovieService
    {
        void AddMovie(MovieRequestModel requestModel);
        IEnumerable<MovieRequestModel> GetUserMovies(int userId);
        MovieRequestModel GetUserMovieById(int userId, int id);
        void DeleteMovieById(int userId, int id);
        void UpdateMovie(MovieRequestModel requestModel);

    }
}
