using EfCoreDiProblem.Context;
using EfCoreDiProblem.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDiProblem.Command;

public record Command(string Name) : IRequest<Unit>;

public class CommandHandler: IRequestHandler<Command>
{
    private readonly ITestService _testService;
    private readonly TestDbContext _context;

    public CommandHandler(ITestService testService, TestDbContext context)
    {
        _testService = testService;
        _context = context;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        var same = _testService.SameContext(_context);
        if(same == false)
        {
            throw new Exception("Contexts are not the same");
        }

        var list = await _context.ItemLists.FirstAsync(cancellationToken: cancellationToken);
        
        _testService.TestAddHandlerToDbSet(list);

        var state = _context.Entry(list.Items[0]).State;
        
        if(state != EntityState.Added)
        {
            throw new Exception("Item dont have state: added");
        }
        
        _context.SaveChanges();
            
        return Unit.Value;
    }
}