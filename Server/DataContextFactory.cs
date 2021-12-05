
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
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.Supervisors");
        context.Database.ExecuteSqlRaw("DELETE dbo.Students");
        context.Database.ExecuteSqlRaw("DELETE dbo.Tag");
        context.Database.ExecuteSqlRaw("DELETE dbo.Projects");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Projects', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Supervisors', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Students', RESEED, 0)");

        var Supervisor1 = new Supervisor {  isSupervisor = true,
                                            name = "Bjørn Sørensen",
                                            Email = "bsørensen@gmail.com" };
        var Supervisor2 = new Supervisor {  isSupervisor = true,
                                            name = "Rasmus Rasmussen",
                                            Email = "rrasmussen@gmail.com" };
        var Supervisor3 = new Supervisor {  isSupervisor = true,
                                            name = "Karl Karlsson",
                                            Email = "kkarlsson@gmail.com" };
        var Supervisor4 = new Supervisor {  isSupervisor = true,
                                            name = "Niels Nielson",
                                            Email = "nielsnson@gmail.com" };

        var Student1 = new Student {  isSupervisor = false,
                                            name = "Jens Jensen",
                                            Email = "jjensen@gmail.com" };
        var Student2 = new Student {  isSupervisor = false,
                                            name = "Søren Sørensen",
                                            Email = "ssørensen@gmail.com" };
        var Student3 = new Student {  isSupervisor = false,
                                            name = "Lars Larsen",
                                            Email = "llarsen@gmail.com" };

        var Tag1 = new Tag { Name = "Engineering" };
        var Tag2 = new Tag { Name = "Programming" };
        var Tag3 = new Tag { Name = "Security" };
        var Tag4 = new Tag { Name = "Database" };

          context.Supervisors.AddRange(
            Supervisor1,
            Supervisor2,
            Supervisor3,
            Supervisor4
        );

        context.Students.AddRange(
            Student1,
            Student2,
            Student3
        );

         context.SaveChanges();

        var Project1 = new Project {    name = "Photoscanning Thesis", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1 }, 
                                        SupervisorID = 1,
                                        shortDescription = "Photoscanning. What is it? I don't know! Help me find out! PLEASE!",
                                        longDescription = "I'm serious. I have no idea! Please help me find out. If you, or someone you know, is familiar with photoscanning, then please come and help me! I'm a desperate professor in need of assistance!"
                                    };
        var Project2 = new Project {    name = "Database Efficiency Study", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag2, Tag3 },
                                        SupervisorID = 1,
                                        shortDescription = "Come with me and help me see how far we can go with databases!",
                                        longDescription = "In this project idea, I am hoping to work with somebody that is able to help me solve the mysteries behind how fast one can go in database management." 
                                        };
        var Project3 = new Project {    name = "Frontend Research Project", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1, Tag4 },
                                        SupervisorID = 2,
                                        shortDescription = "Frontend has been evolving violently - but how does this affect the economy?",
                                        longDescription = "The recent breakthroughs in frontend development, as well as higher salaries, are the reason more and more labourers are quitting their dayjobs to get into coding."
                                    };
        var Project4 = new Project {    name = "RESTful Github Activity", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag3 },
                                        SupervisorID = 2,
                                        shortDescription = "It's kinda like regular Github, but RESTful",
                                        longDescription = "We're all familiar with Github, and most of us know what a RESTful API is. Now we want to get the best of both worlds."
                                        };

        var Project5 = new Project {    name = "Making a proper GoLang tutorial - the project", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag4 },
                                        SupervisorID = 3,
                                        shortDescription = "It bad - we wanna make it better",
                                        longDescription = "We're trying to make a proper tutorial on how to use GoLang. Join us!"};

      
        context.Projects.AddRange(
            Project1,
            Project2,
            Project3,
            Project4,
            Project5
        );

        context.SaveChanges();
    }

    // private Student GenerateRandomStudent(List<string> existingEmails){
    //     Random rand = new Random();
    //     Student student = new Student();
    //     student.isSupervisor = false;

    //     List<string> firstNames = new List<string> { 
    //         "Fritz", "Hjalte", "Bjørn", "Vagn", "Peter", "Michael", "Gabriel", "Benjamin", "Ronnie", "Ernst", 
    //         "Dagmar", "Dorthe", "Maibritt", "Viktoria", "Karoline", "Helene", "Hertha", "Elin", "Alice", "Ester"
    //         };
    //     List<string> lastNames = new List<string> { 
    //         "Aagard", "Therkelsen", "Johanessen", "Hviid", "Juul", "Mouritsen", "Frost", "Mikkelsen",
    //          "Toft", "Loretzen"};
    //     List<string> emailLastPart = new List<string> { "@live.dk", "@hotmail.com", "@outlook.com", "@gmail.com",};
    //     List<string> fullName = new List<string>();

    //     return null;
    // }

    // private Supervisor GenerateRandomSupervisor(List<string> existingEmails)
    // {
    //     Random rand = new Random();
    //     Supervisor supervisor = new Supervisor();
    //     supervisor.isSupervisor = true;
    //     List<string> firstNames = new List<string> { 
    //         "Jørgen", "Julius", "Valdemar", "Claus", "Marcus", "Danny", "Gabriel",  "Frans",  "Ivan",
    //         "Karl", "Cathrine", "Asta", "Alice", "Kamilla", "Freja", "Sille", "Bianca", "Frida", "Agnes"
    //         };
    //     List<string> lastNames = new List<string> { 
    //         "Petersen", "Birch", "Dahl", "Eskildsen", "Lund", "Andresen", "Høj", "Nygaard",
    //          "Bang", "Johannesen", "Mouritsen", "Andersen", "Munch", "Fuglsang", "Laursen" };
    //     List<string> emailLastPart = new List<string> { "@live.dk", "@hotmail.com", "@outlook.com", "@gmail.com",};
    //     List<string> fullName = new List<string>();

    //     fullName.Add(firstNames[rand.Next(0, firstNames.Count - 1)]);
    //     fullName.Add(lastNames[rand.Next(0, lastNames.Count - 1)]);

    //     supervisor.name = string.Join(" ", fullName);

    //     var emailName = fullName[0].Substring(0, 1) + fullName[1];

    //     while(existingEmails.Contains(emailName)){
    //         emailName += rand.Next(1, 99);
    //     }
    //     existingEmails.Add(emailName);

    //     emailName += emailLastPart[rand.Next(0, emailLastPart.Count - 1)];
    //     supervisor.Email = emailName;

    //     return supervisor;
    // }
}
