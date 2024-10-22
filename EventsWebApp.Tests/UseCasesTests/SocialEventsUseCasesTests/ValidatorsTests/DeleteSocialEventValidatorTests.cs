using EventsWebApp.Application.UseCases.SocialEvents.Commands;
using EventsWebApp.Application.Validators;
using FluentAssertions;
using FluentValidation.Results;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.ValidatorsTests
{
    public class DeleteSocialEventValidatorTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeleteSocialEventValidatorTests_IsValid(bool hasId)
        {
            //Arrange
            DeleteSocialEventCommand command = new DeleteSocialEventCommand
            {
                Id = hasId ? Guid.NewGuid(): Guid.Empty,
            };
            DeleteSocialEventValidator validator = new DeleteSocialEventValidator();

            //Act
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(hasId);
        }
    }
}
