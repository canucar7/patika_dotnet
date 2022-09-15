using FluentValidation;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.GenreOperations.Queries;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{

    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {

      public GetAuthorDetailQueryValidator()
      {
        RuleFor(query => query.AuthorId).GreaterThan(0);
      }
        
      
    }
}