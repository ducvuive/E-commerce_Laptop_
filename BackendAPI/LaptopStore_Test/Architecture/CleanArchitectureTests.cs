using FluentAssertions;
using NetArchTest.Rules;
using System.Xml.Linq;

namespace LaptopStore_Test.Architecture
{
    public class CleanArchitectureTests
    {
        [Fact]
        public void Domain_Should_Not_Depend_On_Outer_Layers()
        {
            var result = Types
                .InAssembly(BackendAPI.Domain.AssemblyReference.Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(
                    "BackendAPI.Application",
                    "BackendAPI.Persistence",
                    "BackendAPI.Controllers",
                    "CustomerSite")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_Or_Presentation()
        {
            var result = Types
                .InAssembly(BackendAPI.Application.AssemblyReference.Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(
                    "BackendAPI.Persistence",
                    "BackendAPI.Controllers",
                    "CustomerSite")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Persistence_Should_Not_Depend_On_Presentation()
        {
            var result = Types
                .InAssembly(BackendAPI.Persistence.AssemblyReference.Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(
                    "BackendAPI.Controllers",
                    "CustomerSite")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Project_Should_Not_Reference_Persistence_Or_Api()
        {
            var references = GetProjectReferences(
                "BackendAPI.Application",
                "BackendAPI.Application.csproj");

            references.Should().NotContain(reference =>
                reference.Contains("BackendAPI.Persistence", StringComparison.OrdinalIgnoreCase) ||
                reference.Contains("BackendAPI\\BackendAPI.csproj", StringComparison.OrdinalIgnoreCase) ||
                reference.Contains("BackendAPI/BackendAPI.csproj", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void ProductController_Should_Not_Depend_On_UserDbContext()
        {
            var constructorParameterTypes = typeof(BackendAPI.Controllers.ProductController)
                .GetConstructors()
                .SelectMany(constructor => constructor.GetParameters())
                .Select(parameter => parameter.ParameterType.FullName);

            constructorParameterTypes.Should().NotContain("BackendAPI.Persistence.Data.UserDbContext");
        }

        private static IReadOnlyList<string> GetProjectReferences(string projectDirectory, string projectFile)
        {
            var projectPath = FindProjectFile(projectDirectory, projectFile);
            var document = XDocument.Load(projectPath);

            return document
                .Descendants("ProjectReference")
                .Select(reference => reference.Attribute("Include")?.Value ?? string.Empty)
                .ToList();
        }

        private static string FindProjectFile(string projectDirectory, string projectFile)
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null)
            {
                var candidate = Path.Combine(directory.FullName, projectDirectory, projectFile);
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                candidate = Path.Combine(directory.FullName, "..", projectDirectory, projectFile);
                if (File.Exists(candidate))
                {
                    return Path.GetFullPath(candidate);
                }

                directory = directory.Parent;
            }

            throw new FileNotFoundException($"Could not find {projectFile}.");
        }
    }
}
