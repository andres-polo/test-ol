namespace ComercioApi.Application.DTOs;

public record LoginResponse(string Token, string Nombre, string Rol, DateTime ExpiraEn);
