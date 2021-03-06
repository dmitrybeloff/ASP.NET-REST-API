﻿using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public int BookId { get; private set; }

        public string Title { get; private set; }

        public UpdateBookCommand(int bookId, string title)
        {
            BookId = bookId;
            Title = title;
        }
    }
}
