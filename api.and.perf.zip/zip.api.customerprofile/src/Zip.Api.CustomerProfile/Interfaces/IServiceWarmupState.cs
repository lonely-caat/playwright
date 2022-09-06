namespace Zip.Api.CustomerProfile.Interfaces
{
    public interface IServiceWarmupState
    {
        bool IsReady { get; }
        void StartTask();

        void MarkTaskAsComplete();
    }
}