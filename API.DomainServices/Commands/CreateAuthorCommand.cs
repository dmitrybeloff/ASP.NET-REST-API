﻿using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.DomainServices.Commands
{
    public class CreateAuthorCommand : IRequest<AuthorModel>
    {
        public string Name { get; private set; }

        public List<int> BookIds { get; private set; }

        public CreateAuthorCommand(string name, List<int> bookIds)
        {
            Name = name;
            BookIds = bookIds;
        }
    }
}
