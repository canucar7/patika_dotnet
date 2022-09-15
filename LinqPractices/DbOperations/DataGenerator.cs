using System.Linq;

namespace LinqPractices.DbOperations
{

    public class DataGenerator
    {
        public static void Initialize()
        {
            using(var context = new LinqDbContext())
            {
                if (context.Students.Any())
                {
                    return;
                }

                context.Students.AddRange(
                    new Student()
                    {
                
                        Name = "Selim",
                        Surname = "Yılmaz",
                        ClassId = 1
                    },
                    new Student()
                    {
                      
                        Name = "Fatma",
                        Surname = "Er",
                        ClassId = 1
                    },
                    new Student()
                    {
                        
                        Name = "Umut",
                        Surname = "Arda",
                        ClassId = 2
                    },
                      new Student()
                    {
                        
                        Name = "Merve",
                        Surname = "Arda",
                        ClassId = 2
                    }
                );


                context.SaveChanges();

            }
        }
    }

}