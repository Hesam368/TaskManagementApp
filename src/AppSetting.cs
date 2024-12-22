public static class AppSetting{
    public const int MaxSubTasks = 10;
    public static readonly DateTime DefaultDeadline;

    static AppSetting()
    {
        DefaultDeadline = DateTime.UtcNow.AddDays(7);
    }
}