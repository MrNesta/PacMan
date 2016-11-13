namespace PacMan.Infrastructure
{
    public interface IDialogService
    {
        bool Open();
        string Message { get; }
    }
}
