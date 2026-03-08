using ComercioApi.Application.DTOs;
using ComercioApi.Application.Validators;
using Xunit;

namespace ComercioApi.Tests;

public class LoginRequestValidatorTests
{
    private readonly LoginRequestValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_CorreoIsEmpty()
    {
        var model = new LoginRequest("", "password123");
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(LoginRequest.Correo));
    }

    [Fact]
    public void Should_HaveError_When_CorreoIsInvalid()
    {
        var model = new LoginRequest("not-an-email", "password123");
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_NotHaveError_When_CorreoIsValid()
    {
        var model = new LoginRequest("user@comercio.com", "password123");
        var result = _validator.Validate(model);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_HaveError_When_ContrasenaIsEmpty()
    {
        var model = new LoginRequest("user@comercio.com", "");
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
    }
}
