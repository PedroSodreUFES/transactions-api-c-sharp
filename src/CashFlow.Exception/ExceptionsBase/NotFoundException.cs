using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException : CashFlowException
{
    public NotFoundException(string message) : base(message)
    {
        // base é um super(), que já constrói o atributo Message interno.
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}