using MovieApp.Data;
using MovieApp.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MovieApp.UI
{
    class Program
    {
        static void Adder()
        {
            var context = new MovieContext();
            //var a1 = context.Actors.Where(a => a.Name == "Japp Friske").FirstOrDefault();
            var a1 = new Actor
            {
                Name = "Japp Friske",
                Birthday = new DateTime(1954, 11, 15),
                Nationality = "British"
            };

            var a2 = new Actor
            {
                Name = "Karl Agaton",
                Birthday = new DateTime(1943, 10, 5),
                Nationality = "Swedish"
            };

            var a3 = new Actor
            {
                Name = "Millred Banks",
                Birthday = new DateTime(1944, 1, 11),
                Nationality = "Danish"
            };


            var actors1 = new List<Actor> { a1, a2 };
            var actors2 = new List<Actor> { a1, a3 };

            var movie1 = new Movie
            {
                Title = "Raka spåret till Skövde",
                ReleaseDate = new DateTime(1999, 3, 3),
                Actors = actors1
            };
            var movie2 = new Movie
            {
                Title = "Snökaos i Roskilde",
                ReleaseDate = new DateTime(1987, 11, 13),
                Actors = actors2
            };
            context.Movies.Add(movie1);
            context.Movies.Add(movie2);
            context.SaveChanges();
        }


        static void Main(string[] args)
        {
            //Adder();
            //GetMovies();
            //ParameterizedEntitySQL();
            CountEntitySql();
        }

        private static void CountEntitySql()
        {
            using (MovieContext context = new MovieContext())
            {
                ObjectParameter compDate = new ObjectParameter("compDate", new DateTime(2000, 1, 1));
                string query = @"Select Count(Movies.Id) from MovieContext.Movies as Movies WHERE Movies.ReleaseDate < @compDate";
                var adapter = (IObjectContextAdapter)context;
                var objContext = adapter.ObjectContext;
                objContext.Connection.Open();
                ObjectQuery<DbDataRecord> objQuery = new ObjectQuery<DbDataRecord>(query, objContext);
                objQuery.Parameters.Add(compDate);
                foreach(DbDataRecord item in objQuery)
                {
                    int? count = item[0] as int?;
                    Console.WriteLine(count);
                }
            }
        }

        private static void ParameterizedEntitySQL()
        {
            //Linq to Entity
            using (MovieContext context = new MovieContext())
            {
                var movies = (from Movies in context.Movies
                              where Movies.Id > 0 && Movies.Id < 2
                              select Movies).ToList();
                Console.WriteLine("Linq to Entity");
                foreach(var movie in movies)
                {
                    Console.WriteLine(movie.Id + " " + movie.Title);
                }
            }

            //Entity SQL
            //Tänk att följande två variabler får sitt värde på annan plats i applikationen
            ObjectParameter minValue = new ObjectParameter("minValue", 0);
            ObjectParameter maxValue = new ObjectParameter("maxValue", 2);
            //...
            using (MovieContext context = new MovieContext())
            {
                string query = @"Select VALUE Movies from MovieContext.Movies as Movies WHERE Movies.Id > @minValue AND Movies.Id < @maxValue";
                var adapter = (IObjectContextAdapter)context;
                var objContext = adapter.ObjectContext;
                objContext.Connection.Open();
                ObjectQuery<Movie> objQuery = new ObjectQuery<Movie>(query, objContext);
                objQuery.Parameters.Add(minValue);
                objQuery.Parameters.Add(maxValue);
                List<Movie> movies = objQuery.ToList();
                Console.WriteLine("\n\nEntity SQL");
                foreach(var movie in movies)
                {
                    Console.WriteLine(movie.Id + " " + movie.Title);
                }
            }
        }

        private static void GetMovies()
        {
            //Linq To Entity
            using (var context = new MovieContext())
            {
                var movies = (from Movies in context.Movies select Movies).ToList();

                Console.WriteLine("Linq to Entity");
                foreach(var movie in movies)
                {
                    Console.WriteLine(movie.Title);
                }
            }

            //Entity SQL
            using (var context = new MovieContext())
            {
                string query = @"Select VALUE Movies from MovieContext.Movies as Movies";
                var adapter = (IObjectContextAdapter)context;
                var objContext = adapter.ObjectContext;
                objContext.Connection.Open();

                ObjectQuery<Movie> objQuery = new ObjectQuery<Movie>(query, objContext);
                List<Movie> movies = objQuery.ToList();
                Console.WriteLine("\n\nEntity SQL");
                foreach(var movie in movies)
                {
                    Console.WriteLine(movie.Title);
                }
            }
        }
    }
}
