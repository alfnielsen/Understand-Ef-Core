namespace EfCoreDiProblem.Model;

public class ItemList
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; } = string.Empty;
    
    public List<Item> Items { get; set; } = new();
    
}