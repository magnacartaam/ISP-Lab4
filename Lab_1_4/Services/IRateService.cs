namespace _253504_Patrebka_23;

public interface IRateService
{
    IEnumerable<Rate> GetRates(DateTime date);
}
