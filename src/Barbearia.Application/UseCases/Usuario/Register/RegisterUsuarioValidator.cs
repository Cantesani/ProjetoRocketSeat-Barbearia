using Barbearia.Communication.Request.Usuario;
using Barbearia.Exception;
using FluentValidation;

namespace Barbearia.Application.UseCases.Usuario;

public class RegisterUsuarioValidator: AbstractValidator<RequestRegisterUsuarioJson>
{
    public RegisterUsuarioValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_USUARIO_OBRIGATORIO);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_USUARIO_OBRIGATORIO)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_USUARIO_INVALIDO);

        RuleFor(x => x.Senha).SetValidator(new RegisterSenhaValidator<RequestRegisterUsuarioJson>());
    }
}