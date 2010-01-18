using System;
using System.Collections.Generic;

namespace SimpleWebsite.Core
{
    public class UserMoviePreference : IEntity
    {
        public int Id { get; set; }
        public IEnumerable<int> SortOrder { get; set; }
    }
}