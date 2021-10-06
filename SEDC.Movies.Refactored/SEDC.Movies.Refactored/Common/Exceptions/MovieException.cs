using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.Common.Exceptions
{
    public class MovieException : Exception
    {
        public int? MovieId { get; set; }
        public int UserId { get; set; }


        public MovieException() : base("There has been an issue with a movie!") { }

        public MovieException(int? movieId, int userId) : base("There has been an issue with a movie!")
        {
            MovieId = movieId;
            UserId = userId;
        }

        public MovieException(int? movieId, int userId, string message) : base(message)
        {
            MovieId = movieId;
            UserId = userId;
        }
    }
}
