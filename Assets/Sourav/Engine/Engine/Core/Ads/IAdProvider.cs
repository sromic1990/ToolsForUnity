namespace Sourav.Engine.Engine.Core.Ads
{
    public interface IAdProvider
    {
        void Initialize();
        void ShowBanner();
        void HideBanner();
        void ShowFS();
        void ShowRV();
        void ShowDebugger();
    }
}