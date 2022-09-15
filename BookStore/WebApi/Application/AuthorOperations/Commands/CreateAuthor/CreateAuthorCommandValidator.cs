using System;
using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{

    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {

      public CreateAuthorCommandValidator()
      {

        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(3);
    
        
      }
      
    }
}