namespace Domain.Common.Result;

public class Error
{
    public Error(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; }
    public string Name { get; }
    
    public static Error None => new ("COMMON_0", string.Empty);
    public static Error NullSuccess => new ("COMMON_1", "NullSuccessValue");
    public static Error Unknown => new ("COMMON_2", "Unknown");
}