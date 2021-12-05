
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

    private Supervisor GenerateRandomSupervisor(List<string> existingEmails)
    {
        // Name is build up of 1 first name and 1 surname
        Random r = new Random();
        Supervisor supervisor = new Supervisor();
        List<string> firstNames = new List<string> { 
            "Jørgen", "Julius", "Valdemar", "Claus", "Marcus", "Danny", "Gabriel",  "Frans",  "Ivan",
            "Karl", "Cathrine", "Asta", "Alice", "Kamilla", "Freja", "Sille", "Bianca", "Frida", "Agnes"
            };
        List<string> lastNames = new List<string> { 
            "Petersen", "Birch", "Dahl", "Eskildsen", "Lund", "Andresen", "Høj", "Nygaard",
             "Bang", "Johannesen", "Mouritsen", "Andersen", "Munch", "Fuglsang", "Laursen" };
        List<string> emailLastPart = new List<string> { "@live.dk", "@hotmail.com", "@outlook.com", "@gmail.com",};
        List<string> fullName = new List<string>();

        fullName.Add(firstNames[r.Next(0, firstNames.Count - 1)]);
        fullName.Add(lastNames[r.Next(0, lastNames.Count - 1)]);

        supervisor.name = string.Join(" ", fullName);

        // Email is build up of firstname's first letter + first surname + @hotmail.com
        var emailName = fullName[0].Substring(0, 1) + fullName[1];

        while(existingEmails.Contains(emailName)){
            emailName += r.Next(1, 99);
        }
        existingEmails.Add(emailName);

        emailName += emailLastPart[r.Next(0, emailLastPart.Count - 1)];
        supervisor.Email = emailName;

        return supervisor;
    }
}
