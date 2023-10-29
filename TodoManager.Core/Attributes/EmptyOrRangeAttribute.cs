using System.ComponentModel.DataAnnotations;

namespace TodoManager.Application.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class EmptyOrRangeAttribute : ValidationAttribute
{
    public int Minimum { get; set; } = 0;
    public int Maximum { get; set; } = int.MaxValue;

    public EmptyOrRangeAttribute()
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
            return true;

        int intValue = Convert.ToInt32(value);

        if (intValue >= Minimum && intValue <= Maximum)
            return true;
        return false;
    }
}
