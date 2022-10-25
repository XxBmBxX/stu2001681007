namespace stu2001681007
{
    /// <summary>
    /// This class represents an employee of the organization
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public override string ToString()
        {
            return $"Индентификатор: {Id}  Име: {FirstName}  Фамилия: {LastName}  Department: {Department}";
        }
    }

    /// <summary>
    /// This class represents the Adaptee class. 
    /// Assuming this is a legacy code and it maintains a list of employee and have only one 
    /// functionality of return all the employees to the caller
    /// </summary>
    public class RecordServer
    {
        private List<Employee> _employees = new List<Employee>();
        public RecordServer()
        {
            InitializeEmployees();
        }
        public List<Employee> GetEmployees()
        {
            return _employees;
        }
        private void InitializeEmployees()
        {
            _employees = new List<Employee>
        {
            new Employee { Id = 1001, FirstName = "Michael" , LastName = "Lawson", Department = "Management"},
            new Employee { Id = 1002, FirstName = "Lindsay" , LastName = "Ferguson", Department = "Developer"},
            new Employee { Id = 1003, FirstName = "Tobias" , LastName = "Funke", Department = "IT"},
            new Employee { Id = 1004, FirstName = "Byron" , LastName = "Fields", Department = "IT"},
            new Employee { Id = 1004, FirstName = "George" , LastName = "Edwards", Department = "Developer"}
        };
        }
    }

    /// <summary>
    /// Represents the IAdapter
    /// This interface will allow the client to fetch an employee using employeeId
    /// </summary>
    public interface IEmployeeService
    {
        Employee GetEmployee(int employeeId);
    }

    /// <summary>
    /// Represents the Adapter class.
    /// This class create the instance of Adaptee class and serves the client via composition
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        #region Private member
        private readonly RecordServer recordServer;
        #endregion
        #region Constructor
        public EmployeeService()
        {
            recordServer = new RecordServer();
        }
        #endregion
        #region IAdapter - Implemented Member
        /// <summary>
        /// This method fetches the list of employee from the record server
        /// and return the employee based on the employeeId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Employee GetEmployee(int employeeId)
        {
            var allEmployees = recordServer.GetEmployees();
            return allEmployees.FirstOrDefault(e => e.Id == employeeId);
        }
        #endregion
    }

    public class MainEmployee
    {
        public static void Main(string[] args)
        {
            IEmployeeService service = new EmployeeService();
            var employee = service.GetEmployee(1001);
            employee = service.GetEmployee(1004);
            employee = service.GetEmployee(1020);
            employee = service.GetEmployee(1002);
            Console.Read();
        }
    }
}

