namespace WarframeMod.Common;

public static class TooltipHelper
{
    // Returns the <i, N> of line with form "TooltipN", maximum N
    // i is the index in the list
    // Returns (-1, -1) if nothing found
    static Tuple<int, int> GetIndexOfLastItemTooltip(List<TooltipLine> tooltips)
    {
        int index = -1;
        int maxLineNumber = -1;
        for (int i = 0; i < tooltips.Count; i++)
        {
            var name = tooltips[i].Name;
            if (name.StartsWith("Tooltip"))
            {
                int lineNumber;
                if (!int.TryParse(name.Remove(0, "Tooltip".Length), out lineNumber))
                    continue;
                if (lineNumber > maxLineNumber)
                {
                    index = i;
                    maxLineNumber = lineNumber;
                }
            }
        }

        return new(index, maxLineNumber);
    }

    static Tuple<int, int> GetNextTooltipLine(List<TooltipLine> tooltips)
    {
        var (index, lineNumber) = GetIndexOfLastItemTooltip(tooltips);
        return new(index + 1, lineNumber + 1);
    }

    // Inserts a tooltip line safely with a name "Tooltip#", with # incremented from the existing tooltips
    // to be used from ModItem.ModifyTooltips
    public static void InsertTooltipLine(Mod mod, List<TooltipLine> tooltips, string tooltip)
    {
        var (index, lineNumber) = GetNextTooltipLine(tooltips);
        if (index == 0 || lineNumber == 0)
            mod.Logger.Warn("Failed to add a tooltip!");
        tooltips.Insert(index,
            new TooltipLine(mod, $"Tooltip{lineNumber}", tooltip));
    }
}