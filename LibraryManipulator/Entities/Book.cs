using System;

namespace LibraryManipulator.Entities
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime BornDate { get; set; } 
    }
}