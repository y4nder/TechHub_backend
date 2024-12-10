namespace domain.shared;

public static class Updater
{
    public static T UpdateProperty<T>(T currentValue, T? newValue)
    {
        // Check if the value is of type string
        if (typeof(T) == typeof(string))
        {
            var newString = newValue as string;

            // If the new value is "string" (case insensitive), reject the update
            if (string.Equals(newString, "string", StringComparison.OrdinalIgnoreCase))
            {
                return currentValue;
            }
        }

        // If newValue is not null and differs from currentValue, return newValue; otherwise, return currentValue
        return newValue is not null && !EqualityComparer<T>.Default.Equals(currentValue, newValue)
            ? newValue
            : currentValue;
    }
}