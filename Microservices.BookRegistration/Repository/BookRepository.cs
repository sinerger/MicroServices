using Insight.Database;
using Microservices.BookRegistration.Entity;
using System.Data;
using System.Threading.Tasks;

namespace Microservices.BookRegistration.Repository
{
    public class BookRepository : IBookRepository
    {
        private IDbConnection _connection;
        private IBookRepository _repository;

        public BookRepository(IDbConnection connection)
        {
            _connection = connection;
            _repository = _connection.As<IBookRepository>();
        }

        public async Task CreateBookAsync(Book book)
        {
            await _repository.CreateBookAsync(book);
        }
    }
}
