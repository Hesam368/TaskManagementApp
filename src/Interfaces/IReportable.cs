namespace TaskManagementApp.Interfaces
{
    public interface IReportable<T>
    {
        string GenerateReport(T entity);
    }
}
