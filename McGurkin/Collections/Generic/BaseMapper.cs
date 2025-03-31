namespace McGurkin.Collections.Generic;

/// <summary>
/// Enables the mapping between two sets of strings. Classes which inherit this should use nameing such as DbToFieldNameMappings where Db is left and Field is right.
/// </summary>
public class BaseMapper
{
    private Dictionary<string, string> leftToRightMappings;
    private Dictionary<string, string> rightToLeftMappings;

    /// <summary>
    /// Initializes a new instance with the left to right mappings. The inverse, right to left will automatically be created.
    /// </summary>
    /// <param name="mappings">A Dictionary of left to right mappings.</param>
    public BaseMapper(Dictionary<string, string> mappings)
    {
        leftToRightMappings = new Dictionary<string, string>(mappings, StringComparer.OrdinalIgnoreCase);
        rightToLeftMappings = leftToRightMappings.ToDictionary(kvp => kvp.Value, kvp => kvp.Key, StringComparer.OrdinalIgnoreCase);
    }

    public string GetLeftName(string rightName)
    {
        if (rightToLeftMappings.TryGetValue(rightName, out var returnValue))
            return returnValue;
        throw new Exception($"{rightName} was not found in the right to left mappings.");
    }

    public string[] GetLeftNames(string[] rightNames)
    {
        return rightNames.Select(GetLeftName).ToArray();
    }

    public string GetRightName(string leftName)
    {
        if (leftToRightMappings.TryGetValue(leftName, out var returnValue))
            return returnValue;
        throw new Exception($"{leftName} was not found in the left to right mappings.");
    }

    public string[] GetRightNames(string[] leftNames)
    {
        return leftNames.Select(GetRightName).ToArray();
    }
}
