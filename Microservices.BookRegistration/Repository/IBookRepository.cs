﻿using Microservices.BookRegistration.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.BookRegistration.Repository
{
    public interface IBookRepository
    {
        Task CreateBookAsync(Book book);
    }
}
