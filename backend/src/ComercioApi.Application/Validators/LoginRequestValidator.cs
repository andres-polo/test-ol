using ComercioApi.Application.DTOs;
using FluentValidation;

namespace ComercioApi.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es obligatorio")
            .EmailAddress().WithMessage("Formato de correo inválido");
        RuleFor(x => x.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria");
    }
}
