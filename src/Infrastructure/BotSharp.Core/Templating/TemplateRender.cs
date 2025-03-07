using BotSharp.Abstraction.Routing.Models;
using BotSharp.Abstraction.Templating;
using Fluid;

namespace BotSharp.Core.Templating;

public class TemplateRender : ITemplateRender
{
    private readonly IServiceProvider _services;
    private readonly ILogger _logger;
    private static readonly FluidParser _parser = new FluidParser();
    private TemplateOptions _options;

    public TemplateRender(IServiceProvider services, ILogger<TemplateRender> logger)
    {
        _services = services;
        _logger = logger;
        _options = new TemplateOptions();
        _options.MemberAccessStrategy.MemberNameStrategy = MemberNameStrategies.SnakeCase;
        _options.MemberAccessStrategy.Register<RoutingRecord>();
    }

    public string Render(string template, Dictionary<string, object> dict)
    {
        if (_parser.TryParse(template, out var t, out var error))
        {
            var context = new TemplateContext(dict, _options);
            template = t.Render(context);
            return template;
        }
        else
        {

            return template;
        }
    }
}
