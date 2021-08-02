using MassTransit;
using Microservices.BookRegistration.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.BookRegistration
{
    public class BookSubmittedEventConsumer : IConsumer<Book>
    {
        public async Task Consume(ConsumeContext<Book> context)
        {
            Console.WriteLine("Order Submitted: {0}", context.Message.ID);
        }
    }
}
