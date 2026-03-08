using ComercioApi.Application.DTOs;
using FluentValidation;

namespace ComercioApi.Application.Validators;

public class ComercianteCreateUpdateValidator : AbstractValidator<ComercianteCreateUpdateDto>
{
    public ComercianteCreateUpdateValidator()
    {
        RuleFor(x => x.NombreRazonSocial)
            .NotEmpty().WithMessage("El nombre o razón social es obligatorio")
            .MaximumLength(200).WithMessage("Máximo 200 caracteres");
        RuleFor(x => x.MunicipioId)
            .GreaterThan(0).WithMessage("Debe seleccionar un municipio");
        RuleFor(x => x.Telefono)
            .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Telefono));
        RuleFor(x => x.Correo)
            .EmailAddress().WithMessage("Formato de correo inválido")
            .When(x => !string.IsNullOrEmpty(x.Correo));
        RuleFor(x => x.FechaRegistro)
            .NotEmpty().WithMessage("La fecha de registro es obligatoria");
        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("El estado es obligatorio")
            .Must(e => e == "Activo" || e == "Inactivo")
            .WithMessage("Estado debe ser 'Activo' o 'Inactivo'");
    }
}
