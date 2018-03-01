namespace InteractiveLogin.Views
{
    public interface ILoginView
    {
        void ShowSuccess();

        void ShowFail();

        void Reset(bool force = false);
    }
}
