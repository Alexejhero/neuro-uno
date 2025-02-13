using System.Collections.Generic;
using System.Linq;
using NeuroSdk;
using NeuroSdk.Actions;
using NeuroSdk.Json;
using NeuroSdk.Websocket;
using Newtonsoft.Json.Linq;
using UNO;

namespace NeuroUno.Actions;

public sealed class PlayCardAction : NeuroAction<Card>
{
    public override string Name => "play_card";
    protected override string Description => Strings.PlayCardAction_Description;

    protected override JsonSchema Schema => QJS.WrapObject(new Dictionary<string, JsonSchema>
    {
        ["card"] = QJS.Enum(GetValidCards().Keys)
    });

    protected override ExecutionResult Validate(ActionJData actionData, out Card? card)
    {
        string? cardName = actionData.Data?["card"]?.Value<string>();

        if (cardName == null)
        {
            card = null;
            return ExecutionResult.Failure(NeuroSdkStrings.ActionFailedMissingRequiredParameter.Format("card"));
        }

        Dictionary<string, Card> cards = GetValidCards();
        if (!cards.TryGetValue(cardName, out card))
        {
            return ExecutionResult.Failure(NeuroSdkStrings.ActionFailedInvalidParameter.Format("card"));
        }

        return ExecutionResult.Success();
    }

    protected override void Execute(Card? card)
    {
        GameManager.GetInstance().MainPlayer.DoPlayCard(card);
    }

    private static Dictionary<string, Card> GetValidCards()
    {
        return GameManager.GetInstance().MainPlayer.cards.ToArray()
            .Where(c => GameRuleController.Instance.checkCurrentCardPlayable(c.Def))
            .GroupBy(c => c.Def.DescribeForNeuro())
            .ToDictionary(g => g.Key, g => g.First());
    }
}
