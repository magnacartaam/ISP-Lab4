namespace _253504_Patrebka_23;

public partial class CurrencyConverter : ContentPage
{
    private readonly IRateService rateService;
    public CurrencyConverter(IRateService rateService)
    {
        this.rateService = rateService;
        InitializeComponent();
    }
    private void CurrencyPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime selectedDate = datePicker.Date;

       
        string selectedCurrency = currencyPicker.SelectedItem as string;
        string TempSelectedCurrency = currencyPicker.SelectedItem as string;
        if (selectedCurrency == "Российский рубль") TempSelectedCurrency = "Российских рублей";
        if (selectedCurrency == "Китайский юань") TempSelectedCurrency = "Китайских юаней";

        IEnumerable<Rate> rates = rateService.GetRates(selectedDate);

        Rate selectedRate = rates.FirstOrDefault(rate => rate.Cur_Name == selectedCurrency || rate.Cur_Name == TempSelectedCurrency);
        rateLabel.Text = selectedRate.Cur_OfficialRate.ToString();
    }

    private void NumberEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (!double.TryParse(e.NewTextValue, out _))
            {
                numberEntry.Text = e.OldTextValue ?? string.Empty;
            }
        }
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (e.NewDate > DateTime.Today)
        {
            DisplayAlert("Внимание!", "Ошибка", "OK");
            datePicker.Date = DateTime.Today;
        }
        else
        {
            DateTime selectedDate = datePicker.Date;

            string selectedCurrency = currencyPicker.SelectedItem as string;
            string TempSelectedCurrency = currencyPicker.SelectedItem as string;
            if (selectedCurrency == "Российский рубль") TempSelectedCurrency = "Российских рублей";
            if (selectedCurrency == "Китайский юань") TempSelectedCurrency = "Китайских юаней";

            IEnumerable<Rate> rates = rateService.GetRates(selectedDate);

            Rate selectedRate = rates.FirstOrDefault(rate => rate.Cur_Name == selectedCurrency || rate.Cur_Name == TempSelectedCurrency);
            rateLabel.Text = selectedRate.Cur_OfficialRate.ToString();
        }
    }

    private void CalculateButton_Clicked(object sender, EventArgs e)
    {
        string numberText = numberEntry?.Text;

        if (currencyPicker.SelectedItem is null)
        {
            DisplayAlert("Внимание!", "Выберите валюту.", "Ок");
            return;
        }
        else if (string.IsNullOrEmpty(numberText))
        {
            DisplayAlert("Внимание!", "Нечего конвертировать.", "OK");
            return;
        }
        else
        {
            double num = double.Parse(numberEntry.Text);
            DateTime selectedDate = datePicker.Date;

            string selectedCurrency = currencyPicker.SelectedItem as string;
            string TempSelectedCurrency = currencyPicker.SelectedItem as string;
            if (selectedCurrency == "Российский рубль") TempSelectedCurrency = "Российских рублей";
            if (selectedCurrency == "Китайский юань") TempSelectedCurrency = "Китайских юаней";

            IEnumerable<Rate> rates = rateService.GetRates(selectedDate);

            Rate selectedRate = rates.FirstOrDefault(rate => rate.Cur_Name == selectedCurrency || rate.Cur_Name == TempSelectedCurrency);
            rateLabel.Text = selectedRate.Cur_OfficialRate.ToString();
            result.Text = (num * double.Parse(selectedRate.Cur_OfficialRate.ToString())).ToString()+" Byn";
        }
    }
}