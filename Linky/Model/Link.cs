using System;

namespace Linky.Model
{
    public class Link
    {
        public Guid LinkId { get; set; }
        public string Name { get; set; }
        public Uri Uri { get; set; }
    }
}