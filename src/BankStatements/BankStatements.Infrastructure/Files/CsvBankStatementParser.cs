using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using CsvHelper;

namespace BankStatements.Infrastructure.Files;

public class CsvBankStatementParser : IBankStatementParser
{
    public CsvBankStatementParser()
    {
    }

    private void Init()
    {
        var assemblyName = "newAssembly";
        var moduleName = "newModule";
        var typeName = "typeName";
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.RunAndCollect);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);

        var typeBuilder = moduleBuilder.DefineType(typeName);
        // typeBuilder.DefineProperty("sample", 
        //     PropertyAttributes.None, CallingConventions.Standard)
    }
    
    public async Task Parse(StreamReader stream, BankScheme scheme)
    {
        Init();
        
        var properties = scheme.Properties;
        using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);

        while (await csv.ReadAsync())
        {
            var record = csv.GetRecord<dynamic>();
            foreach (var prop in properties)
            {
                
            }
        }

        csv.ReadHeader();
    }
}