using SEDC.Movies.DataAccess.Context;
using SEDC.Movies.DataAccess.Repositories.Interfaces;
using SEDC.Movies.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.DataAccess.Repositories.Classes
{
    public class UserRepository : IRepository<User>
    {
        private readonly MoviesAppDbContext _context;
        public UserRepository(MoviesAppDbContext context)
        {
            _context = context;
        }
        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
