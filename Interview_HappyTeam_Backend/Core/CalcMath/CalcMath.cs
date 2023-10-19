using Interview_HappyTeam_Backend.Core.DataTransferObject.Config;

namespace Interview_HappyTeam_Backend.Core.CalcMath
{
    public static class CalcMath
    {
        public static double CalcTotalPrice(string carName, DateTime dateStart, DateTime dateEnd)
        {
            double carPricePerDay = CurrentConfig.carConfig.CarConfig.Cars.Where(car => car.Model == carName).First().PricePerDay;
            int days = (dateEnd - dateStart).Days;
            double totalPrice = days > 1 ? days * carPricePerDay : carPricePerDay;
            return Math.Round(totalPrice, 2);
        }
    }
}
