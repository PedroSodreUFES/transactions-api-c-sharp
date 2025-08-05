using CashFlow.Exception.ExceptionsBase;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase()
{
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        this.Validate(request);

        return new ResponseRegisteredExpenseJson
        {
            Title=request.Title,
        };
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        // deu um erro do fluent validator
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}