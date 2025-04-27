// ReSharper disable InconsistentNaming
namespace MyTaskList.WebApi.Options;

public class ConnectionStringOptions
{
    public const string ConnectionStrings = nameof(ConnectionStrings);

    public string AZURE_SQL_CONNECTIONSTRING { get; init; } = string.Empty;
    
}