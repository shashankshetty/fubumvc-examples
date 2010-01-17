using SimpleWebsite.Core;

namespace SimpleWebsite.EndPoints.Movies
{
    public class RemoveEndpoint
    {
        private readonly IRepository _repository;

        public RemoveEndpoint(IRepository repository)
        {
            _repository = repository;
        }

        public AjaxResponse Post(RemoveMovieInput model)
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
}