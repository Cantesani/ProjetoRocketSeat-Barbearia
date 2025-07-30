using System.Text.RegularExpressions;
using Barbearia.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace Barbearia.Application.UseCases.Usuario;

public class RegisterSenhaValidator<T>: PropertyValidator<T,string>
{
    const string ERROR_MESSAGE_KEY = "ErrorMessage";
    
    public override string Name => "RegisterSenhaValidator";
    
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }
    
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(string.IsNullOrWhiteSpace(password) ||   
           password.Length < 8 ||
           !Regex.IsMatch(password, @"[A-Z]+") ||
           !Regex.IsMatch(password, @"[a-z]+") ||
           !Regex.IsMatch(password, @"[1-9]+") ||
           !Regex.IsMatch(password, @"[\!\?\@\*]+"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.SENHA_USUARIO_INVALIDO);
            return false;
        }


        return true;

    }
    
    
    
}