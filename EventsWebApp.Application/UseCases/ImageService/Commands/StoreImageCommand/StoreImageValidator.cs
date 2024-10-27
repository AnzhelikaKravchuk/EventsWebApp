using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.UseCases.ImageService.Commands
{
    public class StoreImageValidator : AbstractValidator<StoreImageCommand>    
    {
        public StoreImageValidator() {
            RuleFor(x => x.Path)
                .NotEmpty();

            RuleFor(x => x.Image)
                .NotEmpty()
                .Must(x => x.IsImage());
        }
    }
}
