using FubuMVC.Core;
using SimpleWebsite.Core;

namespace SimpleWebsite.Handlers.Movies
{
    public class AddHandler
    {
        private readonly IRepository _repository;

        public AddHandler(IRepository repository)
        {
            _repository = repository;
        }

        public AjaxResponse Execute(AddMovieInput model)
        {
            if (model.Title.IsEmpty()) return new AjaxResponse { Success = false };
            var movie = new Movie { Title = model.Title };
            _repository.Save(movie);
            return new AjaxResponse
                {
                    Success = true,
                    Item = movie
                };
        }
    }

    public class AddMovieInput
    {
        public string Title { get; set; }
    }
}