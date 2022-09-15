using System;
using AutoMapper;
using FluentAssertions;
using Xunit;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using TestSetup;

namespace Application.BookOperations.Commands.CreateBook
{

    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public CreateBookCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
    
        [Theory]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings",0,1)]
        [InlineData("Lord Of The Rings",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("Lor",100,1)]
        [InlineData("Lor",100,0)]
        [InlineData("Lord",0,1)]
        [InlineData(" ",100,1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel()
            {
                Title = title, 
                PageCount = pageCount, 
                PublishDate = DateTime.Now.Date.AddYears(-1), 
                GenreId = genreId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]

        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings", 
                PageCount = 100, 
                PublishDate = DateTime.Now.Date, 
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

             result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]

        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnError()
        {

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings", 
                PageCount = 100, 
                PublishDate = DateTime.Now.Date.AddYears(-2), 
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

             result.Errors.Count.Should().Equals(0);

        }



    }
}