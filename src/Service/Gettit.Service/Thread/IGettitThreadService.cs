using Gettit.Data.Models;
using Gettit.Service.Models;

namespace Gettit.Service.Thread
{
    public interface IGettitThreadService : IGenericService<GettitThread, GettitThreadServiceModel>
    {
        Task<CommentServiceModel> CreateCommentOnThread(string threadId, CommentServiceModel commentServiceModel);
    }
}
