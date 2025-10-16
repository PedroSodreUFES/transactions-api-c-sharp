using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesUpdateOnlyRepository
{
    public void Update(Expense expense);
    public Task<Expense?> GetById(Entities.User user ,long id);
}