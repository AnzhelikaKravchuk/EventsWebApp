using EventsWebApp.Application.UseCases.SocialEvents.Commands;
using EventsWebApp.Application.Validators;
using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Tests.UseCasesTests.SocialEventsUseCasesTests.ValidatorsTests
{
    public class CreateSocialEventValidatorTests
    {
        [Fact]
        public void CreateSocialEventValidatorTests_IsValid()
        {
            //Arrange
            CreateSocialEventCommand command = new CreateSocialEventCommand
            {
                EventName = "Title",
                Description = "Description",
                Category = "Category",
                Date = "2100-10-10 00:00:00",
                MaxAttendee = 12,
                Place = "Place"
            };
            CreateSocialEventValidator validator = new CreateSocialEventValidator();

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
        public void CreateSocialEventValidatorTests_IsNotValid(string name, string desc, string category, string date, int max, string place)
        {
            //Arrange
            CreateSocialEventCommand command = new CreateSocialEventCommand
            {
                EventName = name,
                Description = desc,
                Category = category,
                Date = date,
                MaxAttendee = max,
                Place = place
            };
            CreateSocialEventValidator validator = new CreateSocialEventValidator();

            //Act
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(false);
        }
    }
}
