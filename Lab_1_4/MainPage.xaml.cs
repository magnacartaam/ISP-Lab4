namespace _253504_Patrebka_23;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Agreed {count} time";
		else
			CounterBtn.Text = $"Agreed {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}