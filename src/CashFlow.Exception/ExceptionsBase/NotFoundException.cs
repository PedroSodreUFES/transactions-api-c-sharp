namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException : CashFlowException
{
    public NotFoundException(string message) : base(message)
    {
        // base é um super(), que já constrói o atributo Message interno.
    }
}