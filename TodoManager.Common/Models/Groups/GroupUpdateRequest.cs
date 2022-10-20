using TodoManager.Common.Attributes;

namespace TodoManager.Common.Models.Groups;

public class GroupUpdateRequest
{
    [EmptyOrStringLength(Minimum = 3, Maximum = 20, ErrorMessage = "Must be either empty or contain between 3 and 20 characters")]
    public string? Name { get; set; }
}
