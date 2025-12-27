namespace model
{
    public class PatientBill   //class created with name PatientBill to store patient details
{
    public string BillId{get;set;}
    public string PatientName{get;set;}
    public bool HasInsurance{get;set;}

    public decimal ConsultationFee{get;set;}

    public decimal LabChargess{get;set;}
    public decimal MedicineCharges{get;set;}

    public decimal GrossAmount{get;set;}
    public decimal DiscountAmount{get;set;}
    public decimal FinalPayable{get;set;}

    #region Methods

    /// <summary>
    /// Constructer to Take New bill details......
    /// </summary>

    public PatientBill()
    {
        Console.Write("Enter Bill Id: ");
        BillId=Console.ReadLine();
        Console.Write("Enter patient Name: ");
        PatientName=Console.ReadLine();
        Console.Write("Is the patient insured? (true/false):");
        bool num=bool.TryParse(Console.ReadLine(),out num)?num:false;
        HasInsurance=num;
        Console.Write("Enter Consultation Fee: ");
        decimal num1=decimal.TryParse(Console.ReadLine(),out num1)?num1:0;
        ConsultationFee=num1;
        Console.Write("Enter Lab Charges: ");
        decimal num2=decimal.TryParse(Console.ReadLine(),out num2)?num2:0;
        LabChargess=num2;
        Console.Write("Enter Medicine Charges: ");
        decimal num3=decimal.TryParse(Console.ReadLine(),out  num3)?num3:0;
        MedicineCharges=num3;

        Console.WriteLine("Bill created successfully.");
        GrossAmount=ConsultationFee+LabChargess+MedicineCharges;//Calculation of gross from total charges
        Console.WriteLine($"Gross Amount: {GrossAmount}");
        if (HasInsurance)
        {
            DiscountAmount=GrossAmount*((decimal)0.10); // Giving the  Discount if insurance available
        }
        else
        {
            DiscountAmount=0;
        }
        Console.WriteLine($"Discount Amount: {DiscountAmount}");
        FinalPayable=GrossAmount-DiscountAmount;                  // Total gross-discount
        Console.WriteLine($"Final Amount: {FinalPayable}");
        Console.WriteLine("------------------------------------------------------------------------------");
        
    }

    /// <summary>
    /// Generating The details of Current Bill....
    /// </summary>

    
    public void Details()
    {
        Console.WriteLine("-------------- Last Bill-------------");
        Console.WriteLine($"BillId: {BillId}");
        Console.WriteLine($"patient: {PatientName}");
        if(HasInsurance)
        {
            Console.WriteLine("Insured: Yes");
        }
        else
        {
            Console.WriteLine("Insured: No");
        }
        Console.WriteLine($"Consultation Fee: {ConsultationFee}");
        Console.WriteLine($"Lab Charges: {LabChargess}");
        Console.WriteLine($"Medicine Charges: {MedicineCharges}");
        Console.WriteLine($"Gross Amount: {GrossAmount}");
        Console.WriteLine($"Discount Amount: {DiscountAmount}");
        Console.WriteLine($"Final Payable: {FinalPayable}");
        Console.WriteLine("------------------------------");
        Console.WriteLine("------------------------------------------------------------------------------");
    }
    #endregion
    

}
}



