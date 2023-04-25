using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace RTEStyleFormatter;

public class RegisterTinyMCEConfig : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddHostedService<LoadTinyMCEConfig>();
            
    }
}