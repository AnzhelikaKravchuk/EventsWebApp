using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class IdValidator<T> : AbstractValidator<T> where T : IdRequest
    {
        public IdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
