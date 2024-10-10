namespace DiscriminatorSamples.Business;
public enum EmployeeType
{
    Registered,
    Contract
}

public abstract class Employee : Person
{
    public DateTime RegistrationDate { get; set; }
    public EmployeeType Type { get; set; }

}
public class ContractEmployee : Employee
{
    public decimal Charge { get; set; }
    public decimal BonusRate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Contract { get; set; }

}

public class RegisteredEmployee : Employee
{
    public string RegistrationNumber { get; set; }
    public decimal Salary { get; set; }

}
