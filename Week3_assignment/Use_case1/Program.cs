using System;

using Order_Processing;
public class Program
{
    /// <summary>
    /// Is processed to know about salary proccessed status
    /// </summary>
    /// <param name="message"></param>
    private static void IsProcessed(string message)
    {
        Console.WriteLine($"Msg : {message}");
    }
    /// <summary>
    /// Starting Point to Order_processing of Employees
    /// </summary>
    public static void Main()
    {
        Notify func=IsProcessed;//Deleagate var
        Service s1=new Service();
        string name,address,role;
        decimal salary;
        int id;
        int choice=-1;//choice to take val from user to do
        bool Flag=true;//Controls the loop
        while (Flag)
        {
            //Displaying of Menu
            Console.WriteLine("Choose one option from menu given below: ");
            Console.WriteLine("Enter 1 for add Fulltime Employee!");
            Console.WriteLine("Enter 2 for add Contract Employee!");
            Console.WriteLine("Enter 3 to get Total Fulltime Employees Salary!");
            Console.WriteLine("Enter 4 to get Total Contract Employees Salary!");
            Console.WriteLine("Enter 5 to get Fulltime employees details");
            Console.WriteLine("Enter 6 to get Contaract employees details");
            Console.WriteLine("Enter 7 to set salary of Fulltime Employee");
            Console.WriteLine("Enter 8 to set salary of Contarct Employee");

            Console.WriteLine("Enter 0 to Exit");
            Console.Write("Enter Choice: ");
            choice=int.TryParse(Console.ReadLine(),out choice)?choice:-1;

            switch(choice)
            {
                
                case 1:
                    Console.Write("Enter Name: ");
                    name=Console.ReadLine();
                    Console.Write("Enter Id: ");
                    id=int.TryParse(Console.ReadLine(),out id)?id:0;
                    Console.Write("Enter Role: ");
                    role=Console.ReadLine();
                    Console.Write("Enter Address: ");
                    address=Console.ReadLine();
                    FulltimeEmployee emp1=new FulltimeEmployee(name,id,address,role);
                    Process1.FulltimeEmployees.Add_Entry(emp1);
                    break;
                case 2:
                    Console.Write("Enter Name: ");
                    name=Console.ReadLine();
                    Console.Write("Enter Id: ");
                    id=int.TryParse(Console.ReadLine(),out id)?id:0;
                    Console.Write("Enter Role: ");
                    role=Console.ReadLine();
                    Console.Write("Enter Address: ");
                    address=Console.ReadLine();
                    ContractEmployee emp2=new ContractEmployee(name,id,address,role);
                    Process1.ContractEmployees.Add_Entry(emp2);
                    break;
                case 3:
                    Console.WriteLine( $" Total Salary is: {s1.Calculate_Salary(Process1.FulltimeEmployees)}");
                    break;
                case 4:
                    Console.WriteLine( $" Total Salary is: {s1.Calculate_Salary(Process1.ContractEmployees)}");
                    break;
                case 5:
                    Console.WriteLine("Employees details are: ");
                    foreach(var v in Process1.FulltimeEmployees.Employees)
                    {
                        v.Getdetails();
                        Console.WriteLine("----------------------------------------------");
                    }
                    break;
                case 6:
                
                    foreach(var v in Process1.ContractEmployees.Employees)
                    {
                        v.Getdetails();
                        Console.WriteLine("----------------------------------------------");
                    }
                    break;
                case 7:
                    Console.Write("Enter the Employee id to set ");
                    id=int.TryParse(Console.ReadLine(),out id)?id:0;
                    Console.Write("Enter the Amount: ");
                    salary=decimal.TryParse(Console.ReadLine(),out salary)?salary:0;
                    s1.Salary_processing(id,salary,Process1.FulltimeEmployees,func);
                    break;
                case 8:
                    Console.Write("Enter the Employee id to set ");
                    id=int.TryParse(Console.ReadLine(),out id)?id:0;
                    Console.Write("Enter the Amount: ");
                    salary=decimal.TryParse(Console.ReadLine(),out salary)?salary:0;
                    s1.Salary_processing(id,salary,Process1.ContractEmployees,func);
                    break;

                case 0:
                    Flag=false;
                    Console.WriteLine("Thankyou for using our Service.");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;


                 }
        }
        
    }
}
