using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Domain
{
    public class Actor
    {
        public Actor()
        {
            Quotes = new List<Quote>();
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
