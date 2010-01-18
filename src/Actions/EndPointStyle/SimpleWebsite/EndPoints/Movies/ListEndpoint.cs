using System;
using System.Collections.Generic;
using SimpleWebsite.Core;
using System.Linq;

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
            var userPreference = _repository.Query<UserMoviePreference>().FirstOrDefault(); //hey, its a sample
            var allMovies = _repository.Query<Movie>();
            var sortedMovies = userPreference == null ? allMovies : sortMoviesBasedOnUserPreference(allMovies, userPreference);

            return new ListMoviesViewModel { Movies = sortedMovies };
        }

        public AjaxResponse Post(UpdateMovieListOrder input)
        {
            var idsInOrder = input.SortOrder.Split(',').Select(id => Convert.ToInt32(id));
            var userPreference = _repository.Query<UserMoviePreference>().FirstOrDefault() ?? new UserMoviePreference();
            userPreference.SortOrder = idsInOrder;
            _repository.Save(userPreference);
            return new AjaxResponse {Success = true};
        }

        private static IEnumerable<Movie> sortMoviesBasedOnUserPreference(IQueryable<Movie> allMovies, UserMoviePreference userPreference)
        {
            var moviesById = allMovies.ToDictionary(m => m.Id);
            var sortedMovies = new List<Movie>();
            foreach (var id in userPreference.SortOrder)
            {
                if (!moviesById.ContainsKey(id)) continue;
                sortedMovies.Add(moviesById[id]);
                moviesById.Remove(id);
            }
            foreach (var remainingMovieId in moviesById.Keys.OrderBy(id => id))
            {
                sortedMovies.Add(moviesById[remainingMovieId]);
            }
            return sortedMovies;
        }
    }

    public class UpdateMovieListOrder
    {
        public string SortOrder { get; set; }
    }

    public class ListMoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}