using FubuMVC.Core;
using SimpleWebsite.Core;

namespace SimpleWebsite.EndPoints.Movies
{
    public class AddEndpoint
    {
        private readonly IRepository _repository;

        public AddEndpoint(IRepository repository)
        {
            _repository = repository;
        }

        public AjaxResponse Post(AddMovieInput model)
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