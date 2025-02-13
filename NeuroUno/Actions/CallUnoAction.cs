using NeuroSdk.Actions;
using NeuroSdk.Json;
using NeuroSdk.Websocket;
using UNO;

namespace NeuroUno.Actions;

public sealed class CallUnoAction : NeuroAction
{
    public override string Name  => "call_uno";
    protected override string Description => Strings.CallUnoAction_Description;

    protected override JsonSchema? Schema => null;

    protected override ExecutionResult Validate(ActionJData actionData)
    {
        if (GameManager.GetInstance().MainPlayer.CheckCanCallUno()) return ExecutionResult.Success();
        return ExecutionResult.Failure("You can't call UNO right now.");
    }

    protected override void Execute()
    {
        GameManager.GetInstance().MainPlayer.CallUno();
    }
}
