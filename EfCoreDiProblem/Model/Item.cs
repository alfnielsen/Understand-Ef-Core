using System.Text.Json.Serialization;

namespace EfCoreDiProblem.Model;

public class Item
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    
    public Guid ItemListId { get; set; }
    
    [JsonIgnore]
    public virtual ItemList ItemList { get; set; }
}