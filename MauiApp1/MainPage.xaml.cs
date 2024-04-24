using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            AnimateTitle();
        }

       private async void AnimateTitle()
        {
            await titleMain.ScaleTo(0.85, 1000, Easing.BounceIn);
            await titleMain.ScaleTo(1.0, 1000, Easing.BounceOut);
            await titleMain.ScaleTo(0.85, 1000, Easing.BounceIn);
            await titleMain.ScaleTo(1.0, 1000, Easing.BounceOut);
            await titleMain.ScaleTo(0.85, 1000, Easing.BounceIn);
            await titleMain.ScaleTo(1.0, 1000, Easing.BounceOut);
        }

        private async void OnClickedNavigateToNewGamePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewGamePage());
        }
        private async void OnClickSavedGame(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    var sr = new StreamReader(result.FullPath);
                    string data = (sr.ReadToEnd());

                    await Navigation.PushAsync(new GamePage(data));
                }

            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
    }

}
