//using Microsoft.AspNetCore.SignalR;
//using System.Security.Claims;

//namespace NotificationApp_New.SignalR
//{
//    public class UserIdProvider : IUserIdProvider
//    {
//        public string? GetUserId(HubConnectionContext connection)
//        {
//            // Extract the user ID from JWT claims
//            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        }
//    }
//}
