namespace Nibo.Business.Interfaces
{
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Connecting the database access provider
        /// </summary>
        string ConnectionString { get; }
    }
}
