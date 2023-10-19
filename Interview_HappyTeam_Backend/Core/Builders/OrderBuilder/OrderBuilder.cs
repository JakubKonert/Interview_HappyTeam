using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.Builders.OrderBuilder
{
    public class OrderBuilder
    {
        private static Lazy<OrderBuilder> _instance = new Lazy<OrderBuilder>(() => new OrderBuilder());

        private Order _order;

        private OrderBuilder()
        {
        }

        public static OrderBuilder Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public OrderBuilder StartBuilding()
        {
            _order = new Order();
            return this;
        }

        public OrderBuilder WithLocationStart(string locationStart)
        {
            _order.LocationStart = locationStart;
            return this;
        }
        public OrderBuilder WithLocationEnd(string locationEnd)
        {
            _order.LocationEnd = locationEnd;
            return this;
        }
        public OrderBuilder WithCar(string car)
        {
            _order.Car = car;
            return this;
        }
        public OrderBuilder WithCountry(string country)
        {
            _order.Country = country;
            return this;
        }

        public OrderBuilder WithStartDate(DateTime startDate)
        {
            _order.StartDate = startDate;
            return this;
        }

        public OrderBuilder WithEndtDate(DateTime endDate)
        {
            _order.EndDate = endDate;
            return this;
        }

        public OrderBuilder WithTotalPrice(double totalPrice)
        {
            _order.TotalPrice = Math.Round(totalPrice, 2);
            return this;
        }

        public Order Build()
        {
            return _order;
        }

    }
}
