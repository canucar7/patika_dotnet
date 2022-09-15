using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId { get; set; } 
        public UpdateAuthorModel Model {get; set;}

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar Bulunamadı ! ");

            author.Name = Model.Name == default ? author.Name : Model.Name ;
            author.LastName = Model.LastName == default ? author.LastName : Model.LastName;
            author.DateOfBirth=Convert.ToDateTime(Model.DateOfBirth);

            _dbContext.Authors.Update(author);
            _dbContext.SaveChanges();

    }


    }
    
    public class UpdateAuthorModel
    {
       
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        
    }
}