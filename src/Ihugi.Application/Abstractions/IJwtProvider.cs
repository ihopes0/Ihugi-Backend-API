using Ihugi.Domain.Entities;

namespace Ihugi.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}