namespace WarframeMod.Common;

public static class TooltipHelper
{
    // Returns the <i, N> of line with form "TooltipN", maximum N
    // i is the index in the list
    // Returns (-1, -1) if nothing found
    public static Tuple<int, int> GetIndexOfLastItemTooltip(List<TooltipLine> tooltips)
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
}