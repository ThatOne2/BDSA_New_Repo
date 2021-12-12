using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TrialProject.Server.Controllers;
using TrialProject.Shared;
using TrialProject.Shared.DTO;


namespace Server.Tests.Controllers;

public class StudentsControllerTest
{

       private readonly TrialProject.Server.Controllers.DataContext context;

        public readonly StudentsController repo;
 
        

        public StudentsControllerTest()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<TrialProject.Server.Controllers.DataContext>();
            builder.UseSqlite(connection);
            var context = new TrialProject.Server.Controllers.DataContext(builder.Options);
            context.Database.EnsureCreated();

            var logger = new Mock<ILogger<StudentsController>>();

            repo = new StudentsController(logger.Object, context);

            var Stundent = new Student{ID = 1, name = "Bru", Email = "Mart@Brumail.com"};

            context.Students!.Add(Stundent);

            context.SaveChanges();

        }

        [Fact]
    public async Task Student_Reading_Returns_Null()
    {
        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();



        //Act
        var result = repo.ReadStudentDEscById(-1).IsFaulted;

        //Assert
        Assert.True(result);

      
    } 

    [Fact]
    public async Task Student_Reading_By_ID(){

        //Arrange
        var logger = new Mock<ILogger<StudentsController>>();
        var expected = new StudentDescDTO{ID = 1, name = "Bru", Email = "Mart@Brumail.com"};

        //Act
        var project = repo.ReadStudentDEscById(1).Result as OkObjectResult;
        var actual = project.Value;

        //Assert
        Assert.Equal(expected.ToString(), actual.ToString());

    }




    [Fact]
    public async Task Test()
    {
        //Arrange
    
        
        //Act
   

        //Assert
        //Assert.Equal(1,2);
    } 

    
}