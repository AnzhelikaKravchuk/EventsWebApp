using EventsWebApp.Application.SocialEvents.Commands;
using FluentAssertions;
using FluentValidation.Results;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.ValidatorsTests
{
    public class UpdateSocialEventValidatorTests
    {
        [Fact]
        public void UpdateSocialEventValidatorTests_IsValid()
        {
            //Arrange
            UpdateSocialEventCommand command = new UpdateSocialEventCommand
            {
                Id = Guid.NewGuid(),
                EventName = "Title",
                Description = "Description",
                Category = "Category",
                Date = "2100-10-10 00:00:00",
                MaxAttendee = 12,
                Place = "Place"
            };
            UpdateSocialEventValidator validator = new UpdateSocialEventValidator();

            //Act
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
        }

        [Theory]
        [InlineData(false, "Title", "Description", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData(true, "", "Description", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData(true, "Title", "", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData(true, "Title", "Description", "", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData(true, "Title", "Description", "Category", "2000-10-10 00:00:00", 12, "Place")]
        [InlineData(true, "Title", "Description", "Category", "2100-10-10 00:00:00", -1, "Place")]
        [InlineData(true, "Title", "Description", "Category", "2100-10-10 00:00:00", 12, "")]
        [InlineData(true, "Title111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", "Description", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData(true, "Title", "Description", "Category", "2100-10-10 00:00:00", 12, "Title111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void UpdateSocialEventValidatorTests_IsNotValid(bool hasId, string name, string desc, string category, string date, int max, string place)
        {
            //Arrange
            UpdateSocialEventCommand command = new UpdateSocialEventCommand
            {
                Id= hasId? Guid.NewGuid() : Guid.Empty,
                EventName = name,
                Description = desc,
                Category = category,
                Date = date,
                MaxAttendee = max,
                Place = place
            };
            UpdateSocialEventValidator validator = new UpdateSocialEventValidator();

            //Act
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(false);
        }
    }
}
