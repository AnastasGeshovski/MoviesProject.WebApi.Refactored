using SEDC.Movies.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.Services.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterRequestModel requestModel);
        UserModel Authenticate(LoginRequestModel requestModel);
    }
}
