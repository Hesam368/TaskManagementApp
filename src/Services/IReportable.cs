namespace TaskManagementApp.Services
{
    public interface IReportable<T>
    {
        string GenerateReport(T entity);
    }
}
