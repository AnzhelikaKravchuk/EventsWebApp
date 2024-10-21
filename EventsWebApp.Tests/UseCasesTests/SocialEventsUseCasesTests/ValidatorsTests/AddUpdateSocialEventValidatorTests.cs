using EventsWebApp.Application.Validators;
using FluentAssertions;
using FluentValidation.Results;
namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.ValidatorsTests
{
    public class AddUpdateSocialEventValidatorTests
    {
        [Fact]
        public async Task AddUpdateSocialEventValidatorTests_IsValid()
        {
            //Arrange
            AddUpdateSocialEventRequest command = new AddUpdateSocialEventRequest
            {
                EventName = "Title",
                Description = "Description",
                Category = "Category",
                Date = "2100-10-10 00:00:00",
                MaxAttendee = 12,
                Place = "Place"
            };
            AddUpdateSocialEventValidator<AddUpdateSocialEventRequest> validator = new AddUpdateSocialEventValidator<AddUpdateSocialEventRequest>();

            //Act
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
        }

        [Theory]
        [InlineData("", "Description", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData("Title", "", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData("Title", "Description", "", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData("Title", "Description", "Category", "2000-10-10 00:00:00", 12, "Place")]
        [InlineData("Title", "Description", "Category", "2100-10-10 00:00:00", -1, "Place")]
        [InlineData("Title", "Description", "Category", "2100-10-10 00:00:00", 12, "")]
        [InlineData("Title111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", "Description", "Category", "2100-10-10 00:00:00", 12, "Place")]
        [InlineData("Title", "Description", "Category", "2100-10-10 00:00:00", 12, "Title111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public async Task AddUpdateSocialEventValidatorTests_IsNotValid(string name, string desc, string category, string date, int max, string place)
        {
            //Arrange
            AddUpdateSocialEventRequest command = new AddUpdateSocialEventRequest
            {
                EventName = name,
                Description = desc,
                Category = category,
                Date = date,
                MaxAttendee = max,
                Place = place
            };
            AddUpdateSocialEventValidator<AddUpdateSocialEventRequest> validator = new AddUpdateSocialEventValidator<AddUpdateSocialEventRequest>();

            //Act
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(false);
        }
    }
}
