using CashFlow.Domain.Enums;
using CashFlow.Domain.Payment;

namespace CashFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourcePaymentGenerationMessages.CASH,
            PaymentType.CreditCard => ResourcePaymentGenerationMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourcePaymentGenerationMessages.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourcePaymentGenerationMessages.BANK_TRANSFERENCE,
            _ => string.Empty
        };
    }
}