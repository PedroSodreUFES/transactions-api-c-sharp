using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    public Task Add(Expense expense);
    /// <summary>
    /// This function deletes one expense
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task Delete(long id);
}