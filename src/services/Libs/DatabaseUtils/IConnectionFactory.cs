using System.Data;

namespace DatabaseUtils
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}