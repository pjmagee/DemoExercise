using FluentValidation;
using PurpleCubed.Domain.Entities;

namespace PurpleCubed.UI.Validation
{
    public class TeamValidator : AbstractValidator<Team>
    {
        public TeamValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}