using Ihugi.Common.Constants;
using Microsoft.Extensions.Options;
using Ihugi.Infrastructure.Authentication;

namespace Ihugi.WebApi.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(JwtConstants.SectionName).Bind(options);
    }
}