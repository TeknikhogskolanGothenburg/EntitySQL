using MovieApp.Domain;
using System.Data.Entity;

namespace MovieApp.Data
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Quote> Quotes { get; set; }

    }

}
