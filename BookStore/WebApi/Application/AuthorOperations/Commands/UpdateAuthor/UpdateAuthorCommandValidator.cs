using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{

    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {

      public UpdateAuthorCommandValidator()
      {

        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);

        RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(4);
       
      }
      
    }
}