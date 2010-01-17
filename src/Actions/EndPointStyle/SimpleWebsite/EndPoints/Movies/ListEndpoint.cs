using System.Collections.Generic;
using SimpleWebsite.Core;

namespace SimpleWebsite.EndPoints.Movies
{
    public class ListEndpoint
    {
        private readonly IRepository _repository;

        public ListEndpoint(IRepository repository)
        {
            _repository = repository;
        }

        public ListMoviesViewModel Get()
        {
            return new ListMoviesViewModel { Movies = _repository.Query<Movie>() };
        }
    }

    public class ListMoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}