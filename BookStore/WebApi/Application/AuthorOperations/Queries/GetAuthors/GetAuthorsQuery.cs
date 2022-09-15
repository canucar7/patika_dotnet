using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors 
{
    public class GetAuthorQuery
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;

        public GetAuthorQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public List<AuthorViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x => x.Id);
            List<AuthorViewModel> returnObj  = _mapper.Map<List<AuthorViewModel>>(authors); 
            return returnObj;

        }


    }

    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Book> Books { get; set; }

    }

}