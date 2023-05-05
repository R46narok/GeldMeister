using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Interfaces;

public interface IBankStatementParser
{
    /// <summary>
    /// Parses a file based on its file extension
    /// </summary>
    /// <param name="stream">The actual contents of the file, coming from memory, HTTP form, etc.</param>
    /// <param name="scheme">The scheme to be used for parsing, properties must be included</param>
    public Task Parse(StreamReader stream, BankScheme scheme);
}