namespace domain.shared;

public class Updater
{
    public static T UpdateProperty<T>(T currentValue, T? newValue)
    {
        // Reject the update if the new value is the string "string"
        if (typeof(T) == typeof(string) && string.Equals(newValue as string, "string", StringComparison.OrdinalIgnoreCase))
        {
            return currentValue;
        }

        return newValue is not null && !EqualityComparer<T>.Default.Equals(currentValue, newValue)
            ? newValue
            : currentValue;
    }
}
