using SEDC.Movies.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.RequestModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Token { get; set; }
        public List<MovieModel> MovieList { get; set; }
    }
}
