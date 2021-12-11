using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

namespace TrialProject.Server;
public class DataContextFactory : IDesignTimeDbContextFactory<Controllers.DataContext>
{
    public Controllers.DataContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("BDSA");

        var optionsBuilder = new DbContextOptionsBuilder<Controllers.DataContext>()
            .UseSqlServer(connectionString);

        return new Controllers.DataContext(optionsBuilder.Options);
    }

    public static void Seed(Controllers.DataContext context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.ProjectTag");
        context.Database.ExecuteSqlRaw("DELETE dbo.Tag");
        context.Database.ExecuteSqlRaw("DELETE dbo.Projects");
        context.Database.ExecuteSqlRaw("DELETE dbo.Supervisors");
        context.Database.ExecuteSqlRaw("DELETE dbo.Students");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Projects', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Supervisors', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Students', RESEED, 0)");


        var Tag1 = new Tag { Name = "Engineering" };
        var Tag2 = new Tag { Name = "Programming" };
        var Tag3 = new Tag { Name = "Security" };
        var Tag4 = new Tag { Name = "Database" };

        
        var Project1 = new Project {    name = "Photoscanning Thesis", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1 }, 
                                        shortDescription = "Photoscanning. What is it? I don't know! Help me find out! PLEASE!",
                                        longDescription = "I'm serious. I have no idea! Please help me find out. If you, or someone you know, is familiar with photoscanning, then please come and help me! I'm a desperate professor in need of assistance!"
                                    };
        var Project2 = new Project {    name = "Database Efficiency Study", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag2, Tag3 },
                                        shortDescription = "Come with me and help me see how far we can go with databases!",
                                        longDescription = "In this project idea, I am hoping to work with somebody that is able to help me solve the mysteries behind how fast one can go in database management." 
                                        };
        var Project3 = new Project {    name = "Frontend Research Project", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1, Tag4 },
                                        shortDescription = "Frontend has been evolving violently - but how does this affect the economy?",
                                        longDescription = "The recent breakthroughs in frontend development, as well as higher salaries, are the reason more and more labourers are quitting their dayjobs to get into coding."
                                    };
        var Project4 = new Project {    name = "RESTful Github Activity", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag3 },
                                        shortDescription = "It's kinda like regular Github, but RESTful",
                                        longDescription = "We're all familiar with Github, and most of us know what a RESTful API is. Now we want to get the best of both worlds."
                                        };

        var Project5 = new Project {    name = "Making a proper GoLang tutorial - the project", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag4 },
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

        
        var Supervisor1 = new Supervisor {  isSupervisor = true,
                                            name = "Bjørn Sørensen",
                                            Projects = new List<Project> { Project1 },
                                            Email = "bsørensen@FakeMail.com" };
        var Supervisor2 = new Supervisor {  isSupervisor = true,
                                            name = "Rasmus Rasmussen",
                                            Projects = new List<Project> { Project2 },
                                            Email = "rrasmussen@FakeMail.com" };
        var Supervisor3 = new Supervisor {  isSupervisor = true,
                                            name = "Karl Karlsson",
                                            Projects = new List<Project> { Project3 },
                                            Email = "kkarlsson@FakeMail.com" };
        var Supervisor4 = new Supervisor {  isSupervisor = true,
                                            name = "Niels Nielson",
                                            Projects = new List<Project> { Project4, Project5 },
                                            Email = "nielsnson@FakeMail.com" };

        var Student1 = new Student {  isSupervisor = false,
                                            name = "Jens Jensen",
                                            Email = "jjensen@FakeMail.com" };
        var Student2 = new Student {  isSupervisor = false,
                                            name = "Søren Sørensen",
                                            Email = "ssørensen@FakeMail.com" };
        var Student3 = new Student {  isSupervisor = false,
                                            name = "Lars Larsen",
                                            Email = "llarsen@FakeMail.com" };

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

    }

    private Project GenerateRandomProject(Supervisor supervisor){
        Random rand = new Random();
        Project project = new Project();

        List<string> adjectives = new List<string> { 
            ""
        };
        List<string> domain = new List<string> { 
            ""
        };
        List<string> projectType = new List<string> { 
            ""
        };
        List<string> name = new List<string>();

        name.Add(adjectives[rand.Next(0, adjectives.Count - 1)]);
        name.Add(adjectives[rand.Next(0, adjectives.Count - 1)]);
        name.Add(domain[rand.Next(0, domain.Count - 1)]);
        name.Add(projectType[rand.Next(0, projectType.Count - 1)]);

        project.name = string.Join(" ", name);

        project.shortDescription = GenerateRandomShortDescription();
        project.longDescription = GenerateRandomLongDescription();

        List<string> tags = new List<string> { 
            ""
        };

        int i = rand.Next(1, 5);
        while(project.Tags!.Count > i){
            Tag tag = new Tag{ Name = tags[rand.Next(0, tags.Count - 1)] };
            if(!project.Tags.Contains(tag)){
                project.Tags.Add(tag);
            }
        }

        return project;
    }

    private string GenerateRandomLongDescription(){
        throw new NotImplementedException();
    }

    private string GenerateRandomShortDescription(){
        throw new NotImplementedException();
    }

    private Student GenerateRandomStudent(List<string> existingEmails){
        Random rand = new Random();
        Student student = new Student();
        student.isSupervisor = false;

        List<string> firstNames = new List<string> { 
            "Fritz", "Hjalte", "Bjørn", "Vagn", "Peter", "Michael", "Gabriel", "Benjamin", "Ronnie", "Ernst", 
            "Dagmar", "Dorthe", "Maibritt", "Viktoria", "Karoline", "Helene", "Hertha", "Elin", "Alice", "Ester"
            };
        List<string> lastNames = new List<string> { 
            "Aagard", "Therkelsen", "Johanessen", "Hviid", "Juul", "Mouritsen", "Frost", "Mikkelsen",
             "Toft", "Loretzen"};
        List<string> emailLastPart = new List<string> { "@live.dk", "@hotmail.com", "@outlook.com", "@gmail.com",};
        List<string> fullName = new List<string>();

        fullName.Add(firstNames[rand.Next(0, firstNames.Count - 1)]);
        fullName.Add(lastNames[rand.Next(0, lastNames.Count - 1)]);

        student.name = string.Join(" ", fullName);

        var emailName = fullName[0].Substring(0, 1) + fullName[1];

        while(existingEmails.Contains(emailName)){
            emailName += rand.Next(1, 99);
        }
        existingEmails.Add(emailName);

        emailName += emailLastPart[rand.Next(0, emailLastPart.Count - 1)];
        student.Email = emailName;

        return student;
    }

    private Supervisor GenerateRandomSupervisor(List<string> existingEmails)
    {
        Random rand = new Random();
        Supervisor supervisor = new Supervisor();
        supervisor.isSupervisor = true;
        List<string> firstNames = new List<string> { 
            "Jørgen", "Julius", "Valdemar", "Claus", "Marcus", "Danny", "Gabriel",  "Frans",  "Ivan",
            "Karl", "Cathrine", "Asta", "Alice", "Kamilla", "Freja", "Sille", "Bianca", "Frida", "Agnes"
            };
        List<string> lastNames = new List<string> { 
            "Petersen", "Birch", "Dahl", "Eskildsen", "Lund", "Andresen", "Høj", "Nygaard",
             "Bang", "Johannesen", "Mouritsen", "Andersen", "Munch", "Fuglsang", "Laursen" };
        List<string> emailLastPart = new List<string> { "@live.dk", "@hotmail.com", "@outlook.com", "@gmail.com",};
        List<string> fullName = new List<string>();

        fullName.Add(firstNames[rand.Next(0, firstNames.Count - 1)]);
        fullName.Add(lastNames[rand.Next(0, lastNames.Count - 1)]);

        supervisor.name = string.Join(" ", fullName);

        var emailName = fullName[0].Substring(0, 1) + fullName[1];

        while(existingEmails.Contains(emailName)){
            emailName += rand.Next(1, 99);
        }
        existingEmails.Add(emailName);

        emailName += emailLastPart[rand.Next(0, emailLastPart.Count - 1)];
        supervisor.Email = emailName;

        return supervisor;
    }
}
