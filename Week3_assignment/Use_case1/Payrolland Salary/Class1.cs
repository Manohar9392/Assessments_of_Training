namespace Order_Processing{
    /// <summary>
    /// Delegate Function To trigger when Salary is processed
    /// </summary>
    /// <param name="Message"></param>

public  delegate void Notify(string Message);

public abstract class Employee
{
    public string Name{get;set;}//name property
    public int Id{get;set;}//id property

    public decimal Salary{get;protected set;}//Protect set to do not access from outside

    public string Address{get;set;}//address property

    public string Role{get;set;}//role property

    public string Employee_Type{get;set;}//Employee_type property

    /// <summary>
    /// Constructer to add the details of employee
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="address"></param>
    /// <param name="role"></param>
    /// <param name="employee_Type"></param>

    protected Employee(string name,int id,string address,string role,string employee_Type)
        {
            this.Name=name;
            this.Id=id;
            this.Address=address;
            this.Role=role;
            this.Employee_Type=employee_Type;
            Console.WriteLine("Employee Details Entered Successfully.");
        }
        /// <summary>
        /// Getdetails method to get details of particular employee
        /// </summary>
    

    public  void Getdetails()
        {
             Console.WriteLine("Employee details are:");
            Console.WriteLine($"Employee Name : {Name}");
            Console.WriteLine($"Employee Id: {Id}");
             Console.WriteLine($"Employee Type  : {Employee_Type}");
              Console.WriteLine($"Employee Salary : {Salary}");
              Console.WriteLine($"Employee Role: {Role}");
        }



}
/// <summary>
/// Class to deal fulltime employees
/// </summary>


public class FulltimeEmployee : Employee
    {

        public FulltimeEmployee(string name,int id,string address,string role):base(name,id,address,role,"Fulltime")
        {
            
        }
        /// <summary>
        /// this method will set salary for employee if succeed then it will send the message.. through callback
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="callback"></param>

        public void Set_Salary(decimal amount,Notify callback)
        {
            Salary=amount;
            
            callback?.Invoke($"For FulltimeEmployee With {Id}. {amount} is Processed ");

        }


        
    }
/// <summary>
/// Class to deal contract employees
/// </summary>
public class ContractEmployee : Employee
    {
        public ContractEmployee(string name,int id,string address,string role):base(name,id,address,role,"Contract")
        {
            
        }
        public void Set_Salary(decimal amount,Notify callback)
        {
            Salary=amount;

            callback?.Invoke($"For ContractEmployee With Id {Id}. {amount} is Processed . ");

        }
    }

/// <summary>
/// custom generic class to store employees
/// </summary>
/// <typeparam name="T"></typeparam>

public class Action<T>
    {
        public List<T> Employees=new List<T>();

        public void Add_Entry(T obj)
        {
            Employees.Add(obj);
        }
        

        
    }
/// <summary>
/// Service class to provide calculate salary and salary proccessing  services
/// </summary>
public class Service
    {
        public decimal Calculate_Salary(Action<FulltimeEmployee> obj)
        {
            decimal Total=0;
            foreach(var v in obj.Employees)
            {
                Total+=v.Salary;
            }
            return Total;
        }

         public decimal Calculate_Salary(Action<ContractEmployee> obj)
        {
            decimal Total=0;
            foreach(var v in obj.Employees)
            {
                Total+=v.Salary;
            }
            return Total;
        }

        public void Salary_processing(int id,decimal amount,Action<FulltimeEmployee> obj,Notify func)
        {
            foreach(var v in obj.Employees)
            {
                if(v.Id==id)
                {
                    v.Set_Salary(amount,func);
                }
            }
        }

        public void Salary_processing(int id,decimal amount,Action<ContractEmployee> obj,Notify func)
        {
            foreach(var v in obj.Employees)
            {
                if(v.Id==id)
                {
                    v.Set_Salary(amount,func);
                }
            }
        }
        
    }
//static class to keep details of employees in inmemory 

public static class Process1
    {
        public static Action<FulltimeEmployee> FulltimeEmployees = new Action<FulltimeEmployee>();
        public static Action<ContractEmployee> ContractEmployees=new Action<ContractEmployee>();

        public static List<Service> Payslips=new List<Service> ();



    }
}
