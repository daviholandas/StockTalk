using FluentValidation;

namespace StockTalk.Application.Commands.Auth.Validators;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}

public class LoginUserCommandValidation : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidation()
    {
        RuleFor(x => x.Username)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}