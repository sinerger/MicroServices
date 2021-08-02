using MassTransit;
using Microservices.BookRegistration.Entity;
using Microservices.BookRegistration.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.BookRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository _repository;
        private readonly IBusControl _busControl;

        public BookController(IBookRepository repository,IBusControl busControl)
        {
            _repository = repository;
            _busControl = busControl;
        }

        [HttpPost]
        public async Task CreateBook(Book book)
        {
            await _repository.CreateBookAsync(book);
            await _busControl.Publish(book);
        }
    }
}
