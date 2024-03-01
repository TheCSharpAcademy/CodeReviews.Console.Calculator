namespace NumApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.Window.Height = 470;
        this.Window.Width = 300;

        this.Window.MinimumHeight = 470;
        this.Window.MinimumWidth = 300;

        Navigation.PushAsync(new CalculatorPage());
    }
}
