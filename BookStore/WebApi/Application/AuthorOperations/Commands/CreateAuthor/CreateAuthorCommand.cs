using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model {get; set;}

    public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        
    }


    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Name == Model.Name && x.LastName == Model.LastName);
        if(author is not null)
            throw new InvalidOperationException("Yazar zaten mevcut");

        author = _mapper.Map<Author>(Model);

        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();
        
    }

    }

    public class CreateAuthorModel
    {
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth  { get; set; }

    }

}