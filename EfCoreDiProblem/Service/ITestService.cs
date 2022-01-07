using EfCoreDiProblem.Context;
using EfCoreDiProblem.Model;

namespace EfCoreDiProblem.Service;

public interface ITestService
{
    void TestAddHandler(ItemList list);

    void TestAddHandlerToDbSet(ItemList list);
    //void TestAddHandlerToDbSet(ItemList list, TestDbContext context);
    void TestAddHandlerToDbSetAndSave(ItemList list);
}