namespace stu2001681007
{
    public class MainFuelPriceObserverApp
    {
        public enum FuelType
        {
            Gasoline,
            Diesel,
            Ethanol
        }

        public interface IObserver<T>
        {
            void Update(T observedSubject);
        }

        public class GasStationObserver : IObserver<GasStation>
        {
            private string _observerName;

            public string ObserverName { get => _observerName; set => _observerName = value; }

            public GasStationObserver(string observerName)
            {
                _observerName = observerName;
            }   

            public void Update(GasStation observedSubject)
            {
                Console.WriteLine($"{ObserverName} беше уведомен, че {observedSubject.StationName} промени цените на горивата си на:\n{observedSubject}");
            }
        }

        public abstract class ObservedSubject<T> 
            where T : ObservedSubject<T>
        {
            private readonly List<IObserver<T>> _observers = new();

            public List<IObserver<T>> Observers { get => _observers; }

            public void AttachObserver(IObserver<T> observer)
            {
                _observers.Add(observer);
            }

            public void DetachObserver(IObserver<T> observer)
            {
                _observers.Remove(observer);
            }

            protected void NotifyChange()
            {
                _observers.ForEach((observer) => observer.Update((T) this));
            }
        }

        public class GasStation : ObservedSubject<GasStation>
        {
            private const string Unknown = "unknown";
            private string _stationName;
            private (FuelType FuelType, double Price) _gasoline;
            private (FuelType FuelType, double Price) _diesel;
            private (FuelType FuelType, double Price) _ethanol;
           
            public string StationName { get => _stationName; set => _stationName = value; }
            public (FuelType FuelType, double Price) Gasoline { get => _gasoline; }
            public (FuelType FuelType, double Price) Diesel { get => _diesel; }
            public (FuelType FuelType, double Price) Ethanol { get => _ethanol; }

            public GasStation()
            {
                _stationName = Unknown;
                _gasoline = (FuelType.Gasoline, 0);
                _diesel = (FuelType.Diesel, 0);
                _ethanol = (FuelType.Ethanol, 0);
            }

            public GasStation(string stationName, double gasolinePrice, double dieselPrice, double ethanolPrice)
            {
                _stationName = stationName;
                _gasoline = (FuelType.Gasoline, gasolinePrice);
                _diesel = (FuelType.Diesel, dieselPrice);
                _ethanol = (FuelType.Ethanol, ethanolPrice);
            }

            public void ChangeFuelPrice(FuelType fuelType, int price)
            {
                switch (fuelType)
                {
                    case FuelType.Gasoline: _gasoline.Price = price; break;
                    case FuelType.Diesel: _diesel.Price = price; break;
                    case FuelType.Ethanol: _ethanol.Price = price; break;
                }

                NotifyChange();
            }

            public override string ToString()
            {
                return $"Цени:\n Бензин: {Gasoline.Price}\n Дизел: {Diesel.Price}\n Етанол: {Ethanol.Price}\n";
            }
        }

        public static void Main()
        {
            GasStation gasStation = new("Lukoil", 2, 3, 5);

            GasStationObserver gasStationObserver1 = new("Gosho");
            GasStationObserver gasStationObserver2 = new("Pesho");

            gasStation.AttachObserver(gasStationObserver1);
            gasStation.AttachObserver(gasStationObserver2);

            gasStation.ChangeFuelPrice(FuelType.Gasoline, 5);

            Console.ReadKey();
        }
    }
}
