
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using TrialProject.Shared;
public class DataContextFactory : IDesignTimeDbContextFactory<Server.DataContext>
{
    public Server.DataContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("BDSA");

        var optionsBuilder = new DbContextOptionsBuilder<Server.DataContext>()
            .UseSqlServer(connectionString);

        return new Server.DataContext(optionsBuilder.Options);
    }

    public static void Seed(Server.DataContext context)
    {
        /*  var Tag1 = new Tag { Name = "Important" };
        var Tag2 = new Tag { Name = "Not-important" };
        var Tag3 = new Tag { Name = "Boring" };
        var Tag4 = new Tag { Name = "Super Fun" };

        var Task1 = new Task { Title = "Make Notes", TaskState = Core.State.Resolved, Tags = new[] { Tag1 } };
        var Task2 = new Task { Title = "Be at meeting", TaskState = Core.State.Closed, Tags = new[] { Tag2, Tag3 } };
        var Task3 = new Task { Title = "Code", TaskState = Core.State.Active, Tags = new[] { Tag1, Tag4 } };
        var Task4 = new Task { Title = "Phone call", TaskState = Core.State.New, };
        var Task5 = new Task { Title = "Sleep", TaskState = Core.State.New, Tags = new[] { Tag4 } };

        var Tag5 = new Tag { Name = "Relevant", Tasks = new[] { Task1, Task3 } };

        context.Users.AddRange(
            new User
            {
                Name = "Bob The Builder",
                Email = "bob@thebuilder.com",
                Tasks = new[]{
                        Task1, Task2
                    }
            },
            new User
            {
                Name = "Alice",
                Email = "Alice@worker.com",
                Tasks = new[]{
                        Task3
                    }
            }
        );

        context.Tasks.AddRange(
            Task4,
            Task5
        );

        context.SaveChanges(); */
    }

    private static string GenerateCompanyName()
    {
        Random rand = new Random();
        List<string> companyName = new List<string>();

        List<string> initialWords = new List<string> {
            "Smart", "Electro", "Seed", "Hercules", "Pearl", "Development", "Outstanding", "Elite", "Lion",
            "Solstice", "Dream", "Mars", "Pluto", "Explo", "Crystal", "Future", "Titan", "Immortal"
        };
        
        List<string> lastParts = new List<string>() {"Co", "Inc", "ApS", "AAT", "LLC"};

        while (companyName.Count < 3)
        {
            string word = initialWords[rand.Next(0, initialWords.Count - 1)];
            if (!companyName.Contains(word))
            {
                companyName.Add(word);
            }
        }

        companyName.Add(" " + lastParts[rand.Next(0, lastParts.Count - 1)]);

        return string.Join("", companyName);
    }

}
