using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public System.DateTime Now => DateTime.Now;
}