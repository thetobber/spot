using Microsoft.AspNet.SignalR;
using Spot.Models.Post;

namespace Spot
{
    public class PostHub : Hub
    {
        [Authorize(Roles = "Admin")]
        public void Send(PostModel post)
        {
            Clients.All.addNewMessageToPage(post);
        }
    }
}