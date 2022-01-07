using EfCoreDiProblem.Context;
using EfCoreDiProblem.Model;
using EfCoreDiProblem.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDiProblem.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{

    private readonly ILogger<TestController> _logger;
    private readonly TestDbContext _context;
    private readonly ITestService _testService;

    public TestController(ILogger<TestController> logger, TestDbContext context, ITestService testService)
    {
        _logger = logger;
        _context = context;
        _testService = testService;
    }

    [HttpGet("/TestPart1")]
    public IActionResult TestPart1()
    {
        var list = new ItemList()
        {
            Name = "test-item-list"
        };
        _context.ItemLists.Add(list); // Create the list
        _context.SaveChanges();
        return Ok("TestPart1");
    }
    
    
    
    
    [HttpGet("/TestPart2")]
    public IActionResult TestPart2()
    {
        var list = _context.ItemLists.First();
        
        var item = new Item()
        {
            Id = Guid.NewGuid(),
            Text = "Test2"
        };
        list.Items.Add(item);// Add to list (Not dbSet)  // NOT WORKING
        var state = _context.Entry(list.Items[0]).State;
        _context.SaveChanges();
        
        if(state == EntityState.Added)
        {
            return Ok("TestPart2");
        }
        else
        {
            return BadRequest("State is not added but " + state);
        }
    }

        
    [HttpGet("/TestPart3")]
    public IActionResult TestPart3()
    {
        var list = _context.ItemLists.First();
        
        var item = new Item()
        {
            Id = Guid.NewGuid(),
            Text = "Test3"
        };
        _context.Items.Add(item); // only add to dbSet  // NOT WORKING OR...!

        var state = _context.Entry(item).State;
        _context.SaveChanges();
        if(state == EntityState.Added)
        {
            return Ok("TestPart3");
        }
        else
        {
            return BadRequest("State is not added but " + state);
        }
    }

    
    [HttpGet("/TestPart4")]
    public IActionResult TestPart4()
    {
        var list = _context.ItemLists.First();
        
        var item = new Item()
        {
            Id = Guid.NewGuid(),
            Text = "Test4"
        };
        list.Items.Add(item);
        _context.Items.Add(item); // also add to dbSet

        var state = _context.Entry(item).State;
        _context.SaveChanges();

        if(state == EntityState.Added)
        {
            return Ok("TestPart4");
        }
        else
        {
            return BadRequest("State is not added but " + state);
        }
    }
    
    
        
    [HttpGet("/TestPart5")]
    public IActionResult TestPart5()
    {
        var list = _context.ItemLists.First();
        
        var item = new Item()
        {
            Id = Guid.NewGuid(),
            Text = "Test5",
            ItemList = list // add list ref only
        };
        _context.Items.Add(item);

        var state = _context.Entry(item).State;
        _context.SaveChanges();
        
        if(state == EntityState.Added)
        {
            return Ok("TestPart5");
        }
        else
        {
            return BadRequest("State is not added but " + state);
        }
    }
    
    
    [HttpGet("/TestPart6")]
    public IActionResult TestPart6()
    {
        var list = _context.ItemLists.First();
        
        var item = new Item()
        {
            Id = Guid.NewGuid(),
            Text = "Test6",
            ItemListId = list.Id // add list ref ID only
        };
        _context.Items.Add(item);

        var state = _context.Entry(item).State;
        _context.SaveChanges();

        if(state == EntityState.Added)
        {
            return Ok("TestPart6");
        }
        else
        {
            return BadRequest("State is not added but " + state);
        }
    }
    
    
    [HttpGet("/TestPart7")]
    public IActionResult TestPart7()
    {
        var list = _context.ItemLists.First();
        
        var item = new Item()
        {
            Id = Guid.NewGuid(),
            Text = "Test7",
            ItemListId = list.Id // add list ref ID only
        };
        // NO ADDING to any list

        var state = _context.Entry(item).State;
        _context.SaveChanges();

        if(state == EntityState.Added)
        {
            return Ok("TestPart7");
        }
        else
        {
            return BadRequest("State is not added but " + state);
        }
    }

    
    [HttpGet("/TestPartService1")]
    public IActionResult TestPartService1()
    {
        var list = _context.ItemLists.First();
        
        _testService.TestAddHandler(list);
        
        var state = _context.Entry(list.Items[0]).State; // NOT WORKING - the tracking is lost!

        if(state != EntityState.Added)
        {
            return BadRequest("State is not added but " + state);
        }
        
        _context.SaveChanges();
        return Ok("TestPart-service1");
    }

    // [HttpGet("/TestPartService2")]
    // public IActionResult TestPartService2()
    // {
    //     var list = _context.ItemLists.First();
    //     
    //     _testService.TestAddHandlerToDbSet(list, _context);
    //     
    //     var state = _context.Entry(list.Items[0]).State; //WORKING
    //
    //     if(state == EntityState.Added)
    //     {
    //         _context.SaveChanges();
    //         return Ok("TestPart-service2");
    //     }
    //     
    //     return BadRequest("State is not added but " + state);
    //
    // }
    
    
    [HttpGet("/TestPartService22")]
    public IActionResult TestPartService22()
    {
        var list = _context.ItemLists.First();
        
        _testService.TestAddHandlerToDbSet(list);
        
        var state = _context.Entry(list.Items[0]).State; // NOT WORKING - the tracking is lost!

        if (state != EntityState.Added)
        {
            return BadRequest("State is not added but " + state);
        }
        
        _context.SaveChanges();
        return Ok("TestPart-service2");

    }
    
    [HttpGet("/TestPartService3")]
    public IActionResult TestPartService3()
    {
        var list = _context.ItemLists.First();
        
        _testService.TestAddHandlerToDbSetAndSave(list); // The services does the saving...
        
        // var state = _context.Entry(list.Items[0]).State; 

        return Ok("inside service..");
    }
    
    [HttpGet("/GetItems")]
    public IActionResult GetItem()
    {
        var list = _context
            .ItemLists
            .Include(x=>x.Items)
            .First();
        
        return Ok(list.Items);

    }
}