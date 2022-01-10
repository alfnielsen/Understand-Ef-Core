using EfCoreDiProblem.Context;
using EfCoreDiProblem.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDiProblem.Service;

public class TestService: ITestService
{
    private readonly TestDbContext _context;
    
    public bool SameContext(TestDbContext context) => context == _context;
    
    public TestService(TestDbContext context)
    {
        _context = context;
    }
    public void TestAddHandler(ItemList list)
    {
        var item = new Item()
        {
            Text = "Test - service 1"
        };
        list.Items.Add(item);
    }
    
    // public void TestAddHandlerToDbSet(ItemList list, TestDbContext context)
    // {
    //     var sameContext = context.Equals(_context);
    //     
    //     var item = new Item()
    //     {
    //         Text = "Test - service 2"
    //     };
    //     list.Items.Add(item);
    //     _context.Items.Add(item);
    // }
    public void TestAddHandlerToDbSet(ItemList list)
    {
        var item = new Item()
        {
            Text = "Test - service 3"
        };
        list.Items.Add(item);
        _context.Items.Add(item);
    }
    public void TestAddHandlerToDbSetAndSave(ItemList list)
    {
        var item = new Item()
        {
            Text = "Test - service 4"
        };
        //_context.Items.Add(item);
        //item.ItemListId = list.Id;
        list.Items.Add(item); // <- the list need to add the item, so its actually there when we return to the other scope!
        _context.SaveChanges();
    }
}