using SimpleWebsite.Core;

namespace SimpleWebsite.Handlers.Movies
{
    public class RemoveHandler
    {
        private readonly IRepository _repository;

        public RemoveHandler(IRepository repository)
        {
            _repository = repository;
        }

        public AjaxResponse Execute(RemoveMovieInput model)
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