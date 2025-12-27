using System;
using model;



public class Program
{
    static PatientBill? LastBill=null;
    static bool HasLastBill=false;
    /// <summary>
    /// Starting Point
    /// </summary>
    /// <param name="args"></param>
    
    public static void Main(string[] args)
    {
        int choice=-1;// variable to store user entered choice from menu
        bool flag =true;//variable to control the while loop

        #region Menu bar
        
        
        while(flag){
            //Displaying the menu
        Console.WriteLine("======================MediSure Clinic Billing================================");
        Console.WriteLine("1. Create New Bill (Enter patient Details)");
        Console.WriteLine("2. View Last Bill");
        Console.WriteLine("3. Clear Last Bill");
        Console.WriteLine("4. Exit");

        Console.Write("Enter your option: ");
        choice=int.TryParse(Console.ReadLine(),out choice)?choice:-1;
        

        switch (choice)
        {
            case(1)://Take the New Bill
            LastBill=new PatientBill();
            HasLastBill=true;
            break;
            case(2):                 //Give the details  of Existed Bill if existed otherwise returns given info
            if(HasLastBill)
                {
                    LastBill.Details();
                }
                else
                {
                    Console.WriteLine("No bill available.Please create a new bill first");
                }
            break;
            case(3):// Clear the bill
            LastBill=null;
            HasLastBill=false;
            Console.WriteLine("last bill cleared");
            break;
            case(4)://it will close the application
            flag =false;
            Console.WriteLine("Thank you. Application closed normally.");
            break;
            default:
            Console.WriteLine("Invalid Choice. Try again");
            break;
            


        }
        }
        #endregion

    


        
        
    }
}