using System.Text.Json.Serialization;

namespace Company_Expense_Tracker.Dtos;

public class ActionDto
{
    [JsonIgnore]
    public int Id { get; set; }
}