namespace TaskVault.API.Dtos.DashBoardDto
{
    public class DashboardSummaryDto
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
    }

}
