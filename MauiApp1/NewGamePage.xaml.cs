namespace MauiApp1;

public partial class NewGamePage : ContentPage
{
	public NewGamePage()
	{
		InitializeComponent();
	}
    private async void OnClickedNavigateToNewGamePage(object sender, EventArgs e)
    {
        int idx = picker.SelectedIndex + 5;
        await Navigation.PushAsync(new GamePage(idx));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}