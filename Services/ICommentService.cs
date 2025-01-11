namespace WorkToDo.Services
{
    public interface ICommentService
    {
        void AddComment(int workItemId, string content, int userId);
    }
}
