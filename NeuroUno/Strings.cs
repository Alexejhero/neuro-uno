// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Text;
using UNO;

namespace NeuroUno;

public static class Strings
{
    public const string PlayCardAction_Description = "Play a valid card from your hand.";
    public const string CallUnoAction_Description = "Call UNO when you have one card left.";

    public static readonly Dictionary<COLORS, string> Card_ColorNames = new()
    {
        [COLORS.red] = "Red",
        [COLORS.green] = "Green",
        [COLORS.yellow] = "Yellow",
        [COLORS.blue] = "Blue",
        [COLORS.NOColor] = "Wild"
    };

    public static readonly Dictionary<FUNCTIONAL, string> Card_SpecialNames = new()
    {
        [FUNCTIONAL.skip] = "Skip",
        [FUNCTIONAL.reverse] = "Reverse",
        [FUNCTIONAL.draw2] = "Draw 2",
        [FUNCTIONAL.wild] = "", // color is "Wild" already
        [FUNCTIONAL.joker] = "Draw 4",
        [FUNCTIONAL.seven] = "7 (Swap)",
        [FUNCTIONAL.zero] = "0 (Everyone Swap)"
    };

    public static string DescribeForNeuro(this CardDef card)
    {
        StringBuilder result = new();

        if (Card_ColorNames.TryGetValue(card.Color, out string? colorName)) result.Append(colorName);
        else return "Unknown Card";

        result.Append(' ');

        if (card.Functional == FUNCTIONAL.normal) result.Append(card.Value);

        if (Card_SpecialNames.TryGetValue(card.Functional, out string? specialName)) result.Append(specialName);
        else return "Unknown Card";

        return result.ToString();
    }
}
