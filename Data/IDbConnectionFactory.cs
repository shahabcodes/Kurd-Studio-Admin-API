using System.Data;

namespace KurdStudio.AdminApi.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
