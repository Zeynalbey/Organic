////using Organic.Areas.Admin.Hubs;
//using Organic.Contracts.Alert;
//using Organic.Contracts.Identity;
//using Organic.Database;
//using Organic.Services.Abstracts;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;

//namespace Organic.Services.Concretes
//{
//    public class NotificationService : INotificationService
//    {
//        //private readonly IHubContext<AlertHub> _hubContext;
//        private readonly IUserService _userService;
//        private readonly DataContext _dataContext;

//        public NotificationService(
//            //IHubContext<AlertHub> hubContext,
//            IUserService userService,
//            DataContext dataContext)
//        {
//            //_hubContext = hubContext;
//            _userService = userService;
//            _dataContext = dataContext;
//        }

//        public async Task SenOrderCreatedToAdmin(string trackingCode)
//        {
//            //NOTIFICATION TO MODERATORS


//            foreach (var user in await _dataContext.Users.Where(u => u.Role.Name == RoleNames.ADMIN).ToListAsync())
//            {
//                await _hubContext.Clients
//                    .Group(user.Id.ToString())
//                    .SendAsync("Notify", new
//                    {
//                        Title = AlertMessages.ORDER_CREATED_TITLE_TO_MODERATOR,
//                        Content = AlertMessages.ORDER_CREATED_CONTENT_TO_MODERATOR
//                                    .Replace("{first_name}", _userService.CurrentUser.FirstName)
//                                    .Replace("{last_name}", _userService.CurrentUser.LastName)
//                                    .Replace("{tracking_code}", trackingCode)  //string builder should be used
//                    });
//            }


//            //NOTIFICATION TO ORDER'S OWNER

//            await _hubContext.Clients
//                   .Group(_userService.CurrentUser.Id.ToString())
//                   .SendAsync("Notify", new
//                   {
//                       Title = AlertMessages.ORDER_CREATED_TITLE_TO_OWNER,
//                       Content = AlertMessages.ORDER_CREATED_CONTENT_TO_OWNER
//                                    .Replace("{tracking_code}", trackingCode)  //string builder should be used
//                   });
//        }
//    }
//}
