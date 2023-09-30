namespace Unix_Sort;

public class StringNumericComparer: IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        return Compare(x, y, StringComparison.InvariantCulture);
    }

    public int Compare(string? x, string? y, StringComparison comparisonType)
    {
        int xNumber;
        int yNumber;
        bool isNumericX = int.TryParse(x, out xNumber);
        bool isNumericY = int.TryParse(y, out yNumber);

        if (isNumericX && isNumericY)
            return xNumber.CompareTo(yNumber);
        if (isNumericX)
            return -1;
        if (isNumericY)
            return 1;
        return 0;
    }
}