using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    public Task Add(Expense expense);
    /// <summary>
    /// This function returns TRUE if the deletion was succesfull. Otherwise, returns false.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> Delete(long id);
}