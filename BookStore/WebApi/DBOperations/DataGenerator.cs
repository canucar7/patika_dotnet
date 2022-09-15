using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{

    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }


                context.Authors.AddRange(
                    new Author 
                    {
                        Name = "Fyodor",
                        LastName = "Dostoyevski",
                        DateOfBirth = new DateTime(1705,06,12)
                    
                    },
                    new Author 
                    {
                        Name = "Karl",
                        LastName = "Marx",
                        DateOfBirth = new DateTime(1996,06,12)
                        
                    },
                    new Author 
                    {
                        Name = "Friedrich",
                        LastName = "Nietzsche",
                        DateOfBirth = new DateTime(1844,06,12)
                        
                        
                    }

                );



                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(

                    new Book{
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, 
                        AuthorId=1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)

                    },

            
                    new Book{
                       // Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        AuthorId=2,  
                        PageCount = 250,
                        PublishDate = new DateTime(2010,05,23)

                    },

                    
                    new Book{
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2,
                        AuthorId=3, 
                        PageCount = 540,
                        PublishDate = new DateTime(2001,12,21)

                    }
                );

                context.SaveChanges();
            }
        }
    }
}