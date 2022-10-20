using TodoManager.Common.Attributes;
using TodoManager.Common.Models.Enums;

namespace TodoManager.Common.Models.Todos;

public class TodoUpdateRequest
{
    [EmptyOrStringLength(Minimum = 2, Maximum = 50, ErrorMessage = "Must be either empty or contain between 2 and 50 characters")]
    public string Title { get; set; } = string.Empty;

    [EmptyOrStringLength(Minimum = 2, Maximum = 50, ErrorMessage = "Must be either empty or contain between 2 and 250 characters")]
    public string? Description { get; set; }

    [EmptyOrRange(Minimum = 1, Maximum = 5, ErrorMessage = "Must be either empty or in range 1-5")]
    public Status Status { get; set; } = 0;

}
