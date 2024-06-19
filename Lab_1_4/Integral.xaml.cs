namespace _253504_Patrebka_23;

public partial class Integral : ContentPage
{
	public Integral()
	{
		InitializeComponent();
		this.CancelButton.IsEnabled = false;
	}
	private CancellationTokenSource? cancellationTokenSource;

    public async void OnClicked(object sender, EventArgs e)
    {
        this.StartButton.IsEnabled = false;
        this.CancelButton.IsEnabled = true;
        if (((Button)sender).Text == "Start"){
            this.progress.Progress = 0;
            this.result.Text = "Calculating...";

            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

			
            var result = await Start(cancellationToken);
            this.result.Text = result == -1 ? "Calculation cancelled" : result.ToString();
            StartButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
        }
        else if (((Button)sender).Text == "Cancel"){
            cancellationTokenSource?.Cancel();
        }
    }

    private async Task<double> Start(CancellationToken cancellationToken)
    {
        double result = 0;
        double step = 0.00000001;
        double progress = 0;
        int counter = 0;

        for (double i = 0; i < 1; i += step){
            if (cancellationToken.IsCancellationRequested){
                this.progress.Progress = 0;
                this.information.Text = "0 %";
                this.result.Text = "Calculation cancelled";
                return -1;
            }

            if (counter == 100000){
                progress += step * 100000;
                await this.progress.ProgressTo(progress, 1, Easing.Linear);
                this.information.Text = (progress * 100).ToString("##.###") + " %";
                counter = 0;
            }
            counter++;
            result += step * Math.Sin(i);
        }
        this.information.Text = "100 %";
        return result;
    }
}