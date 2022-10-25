namespace stu2001681007
{
    /// <summary>
    /// Device type enums
    /// </summary>
    public enum DeviceType
    {
        NONE,
        LAPTOP,
        COMPUTER
    }

    /// <summary>
    /// Device model enums
    /// </summary>
    public enum DeviceModel
    {
        NONE,
        HP,
        DELL,
        ASUS,
    }

    /// <summary>
    /// Abstract factory class
    /// </summary>
    public abstract class DeviceFactory
    {
        public abstract Device CreateDevice();
    }

    /// <summary>
    /// Concrete factory class for Laptop
    /// </summary>
    public class LaptopFactory : DeviceFactory
    {
        public override Device CreateDevice()
        {
            return new Laptop(DeviceType.LAPTOP, DeviceModel.DELL, 16, false, 3400);
        }
    }


    /// <summary>
    /// Concrete factory class for Computer
    /// </summary>
    public class ComputerFactory : DeviceFactory
    {
        public override Device CreateDevice()
        {
            return new Computer(DeviceType.COMPUTER, DeviceModel.ASUS, 8, true, 700);
        }
    }

    /// <summary>
    /// Another level of abstraction to the abstract factory class
    /// </summary>
    public class DeviceShop
    {
        public static Device OrderDevice(DeviceFactory deviceFactory)
        {
            return deviceFactory.CreateDevice();
        }
    }

    /// <summary>
    /// Main abstract object
    /// </summary>
    public abstract class Device
    {
        #region Private fields

        private readonly DeviceType _deviceType;
        private readonly DeviceModel _deviceModel;
        private readonly int _ram;
        private readonly bool _hasExternalGpu;

        #endregion

        #region Public fields

        public DeviceType DeviceType { get => _deviceType; }
        public DeviceModel DeviceModel { get => _deviceModel; }
        public int Ram { get => _ram; }
        public bool HasExternalGpu { get => _hasExternalGpu; }

        #endregion

        #region Constructors

        public Device()
        {
            _deviceType = DeviceType.NONE;
            _deviceModel = DeviceModel.NONE;
            _ram = 0;
            _hasExternalGpu = false;
        }

        public Device(DeviceType deviceType, DeviceModel deviceModel, int ram, bool hasExternalGpu)
        {
            _deviceType = deviceType;
            _deviceModel = deviceModel;
            _ram = ram;
            _hasExternalGpu = hasExternalGpu;
        }

        #endregion

        public static string BoolToWord(bool boolToConvert)
        {
            return boolToConvert ? "Да" : "Не";
        }
    }

    /// <summary>
    /// Laptop class
    /// </summary>
    public class Laptop : Device
    {
        #region Private fields

        private readonly int _batteryCapacity;

        #endregion

        #region Public fields

        public int BatteryCapacity { get => _batteryCapacity; }

        #endregion

        #region Constructors

        public Laptop() : base()
        {
            _batteryCapacity = 0;
        }

        public Laptop(DeviceType deviceType, DeviceModel deviceModel, int ram, bool hasExternalGpu, int batteryCapacity)
            : base(deviceType, deviceModel, ram, hasExternalGpu)
        {
            _batteryCapacity = batteryCapacity;
        }

        #endregion

        public override string ToString()
        {
            return
                $"Устройство:\n " +
                $"Тип: {DeviceType}\n " +
                $"Модел: {DeviceModel}\n " +
                $"Рам: {Ram}\n " +
                $"Външна видео карта: {BoolToWord(HasExternalGpu)}\n " +
                $"Батерия: {BatteryCapacity}mAh";
        }
    }

    /// <summary>
    /// Computer class
    /// </summary>
    public class Computer : Device
    {
        #region Private fields

        private readonly int _powerSupplyCapacity;

        #endregion

        #region Public fields

        public int PowerSupplyCapacity { get => _powerSupplyCapacity; }

        #endregion

        #region Constructors

        public Computer() : base()
        {
            _powerSupplyCapacity = 0;
        }

        public Computer(DeviceType deviceType, DeviceModel deviceModel, int ram, bool hasExternalGpu, int powerSupplyCapacity)
        : base(deviceType, deviceModel, ram, hasExternalGpu)
        {
            _powerSupplyCapacity = powerSupplyCapacity;
        }

        #endregion

        public override string ToString()
        {
            return
                $"Устройство:\n " +
                $"Тип: {DeviceType}\n " +
                $"Модел: {DeviceModel}\n " +
                $"Рам: {Ram}\n " +
                $"Външна видео карта: {BoolToWord(HasExternalGpu)}\n " +
                $"Захранване: {PowerSupplyCapacity}W";
        }
    }

    /// <summary>
    /// Main program entry point
    /// </summary>
    public class MainDeviceFactoryApp
    {
        public static void MainDeviceFactory()
        {
            LaptopFactory laptopFactory = new();
            ComputerFactory computerFactory = new();

            Console.WriteLine(DeviceShop.OrderDevice(laptopFactory));
            Console.WriteLine(DeviceShop.OrderDevice(computerFactory));

            Console.ReadKey();
        }
    }
}
