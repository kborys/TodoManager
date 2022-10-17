using System.ComponentModel.DataAnnotations;

namespace TodoManager.Common.Attributes;

public class EmptyOrInRangeAttribute : ValidationAttribute
{
    public int Minimum { get; set; } = 0;
    public int Maximum { get; set; } = int.MaxValue;

    public EmptyOrInRangeAttribute()
    {
    }

    public override bool IsValid(object value)
    {
        string strValue = value as string;
        if(!string.IsNullOrEmpty(strValue))
        {
            int length = strValue.Length;
            return length >= Minimum && length <= Maximum;
        }
        return true;
    }
}
