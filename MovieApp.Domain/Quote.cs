using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Actor TheActor { get; set; }
        public int ActorId { get; set; }
    }
}
