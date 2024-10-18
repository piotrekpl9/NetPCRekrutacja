using FluentValidation;

namespace Application.Contacts.Command.Create;

public class CreateContactsCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactsCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .Length(1, 100).WithMessage("Surname must be between 1 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(1, 50).WithMessage("Category name must be between 1 and 50 characters.");

        RuleFor(x => x.SubcategoryName)
            .Length(0, 50).WithMessage("Subcategory name must not exceed 50 characters.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.")
            .LessThan(DateTime.Today).WithMessage("Birth date must be in the past.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international format.");
    }
}