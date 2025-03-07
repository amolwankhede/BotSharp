namespace BotSharp.Core.Hooks;

public class RoutingHook : AgentHookBase
{
    public RoutingHook(IServiceProvider services, AgentSettings settings)
        : base(services, settings)
    {
    }

    public override bool OnInstructionLoaded(string template, Dictionary<string, object> dict)
    {
        var router = _services.GetRequiredService<IAgentRouting>();
        dict["routing_records"] = router.GetRoutingRecords()
            .Where(x => !x.Disabled)
            .ToList();
        return true;
    }
}
