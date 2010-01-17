using System.Collections.Generic;
using SimpleWebsite.Core;

namespace SimpleWebsite.Handlers.Movies
{
    public class ListHandler
    {
        private readonly IRepository _repository;

        public ListHandler(IRepository repository)
        {
            _repository = repository;
        }

        public ListMoviesViewModel Execute()
        {
            return new ListMoviesViewModel { Movies = _repository.Query<Movie>() };
        }
    }

    public class ListMoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}