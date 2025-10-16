using CashFlow.Communication.Requests;

public interface IChangeUserPasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}