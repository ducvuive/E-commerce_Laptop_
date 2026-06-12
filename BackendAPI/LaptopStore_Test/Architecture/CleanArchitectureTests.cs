using FluentAssertions;
using NetArchTest.Rules;

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
    }
}
