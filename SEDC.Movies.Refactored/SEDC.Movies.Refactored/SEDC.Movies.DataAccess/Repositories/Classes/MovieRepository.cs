using SEDC.Movies.DataAccess.Context;
using SEDC.Movies.DataAccess.Repositories.Interfaces;
using SEDC.Movies.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.DataAccess.Repositories.Classes
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly MoviesAppDbContext _context;
        public MovieRepository(MoviesAppDbContext context)
        {
            _context = context;
        }
        public void Add(Movie entity)
        {
            _context.Movies.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Movie entity)
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies;
        }

        public void Update(Movie entity)
        {
            _context.Movies.Update(entity);
            _context.SaveChanges();
        }
    }
}
