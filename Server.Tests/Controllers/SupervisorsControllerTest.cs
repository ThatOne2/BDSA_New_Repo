using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Tests.Controllers;

public class SupervisorsControllerTest
{

       private readonly DataContext context;

        public readonly SupervisorController repo;
 
        

        public SupervisorsControllerTest()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlite(connection);
            var context = new DataContext(builder.Options);
            context.Database.EnsureCreated();

            var logger = new Mock<ILogger<SupervisorController>>();

            repo = new SupervisorController(logger.Object, context);

        }


    [Fact]
    public async Task Test()
    {
        //Arrange
    
        
        //Act
   

        //Assert
      
    } 

    
}