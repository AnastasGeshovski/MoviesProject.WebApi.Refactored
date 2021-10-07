using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.RequestModels
{
    public class RegisterRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
