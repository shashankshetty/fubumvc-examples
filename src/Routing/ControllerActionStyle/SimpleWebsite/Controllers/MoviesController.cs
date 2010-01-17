using System.Collections.Generic;
using SimpleWebsite.Core;
using FubuMVC.Core;

namespace SimpleWebsite.Controllers
{
    public class MoviesController
    {
        private readonly IRepository _repository;

        public MoviesController(IRepository repository)
        {
            _repository = repository;
        }

        public ListMoviesViewModel List()
        {
            return new ListMoviesViewModel {Movies = _repository.Query<Movie>()};
        }

        public AjaxResponse Add(AddMovieInput model)
        {
            if (model.Title.IsEmpty()) return new AjaxResponse {Success = false};
            var movie = new Movie{Title = model.Title};
            _repository.Save(movie);
            return new AjaxResponse
                {
                    Success = true,
                    Item = movie
                };
        }

        public AjaxResponse Remove(RemoveMovieInput model)
        {
            var movie = _repository.Find<Movie>(model.Id);
            _repository.Delete(movie);
            return new AjaxResponse
                {
                    Success = true
                };
        }
    }

    public class RemoveMovieInput
    {
        public int Id { get; set; }
    }

    public class AddMovieInput
    {
        public string Title { get; set; }
    }

    public class AjaxResponse
    {
        public bool Success { get; set; }
        public object Item { get; set; }
    }

    public class ListMoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}