using Microsoft.AspNetCore.Builder;

namespace Core.Application.Abstractions;

public interface IApi
{
    void Register(WebApplication app, string baseApiUrl = "");
}