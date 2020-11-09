using System;

namespace EmployeePayrollService
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Service!");

            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Akash";
            model.PhoneNumber = "9875241630";
            model.Address = "Mumbai";
            model.Department = "SD";
            model.Gender = 'M';
            model.BasicPay = 53000;
            model.Deductions = 2000;
            model.TaxablePay = 500;
            model.Tax = 1000;
            model.NetPay = 50000;
            model.StartDate = DateTime.Now;
            model.City = "Mulund";
            model.Country = "In";

            EmployeeRepo repo = new EmployeeRepo();

            Console.WriteLine("Displaying All Data");
            repo.GetAllEmployee();

            bool res = repo.AddEmployee(model);
            if (res)
            {
                Console.WriteLine("Employee Added Successfully");
                repo.GetAllEmployee();
            }
            else
            {
                Console.WriteLine("Employee isn't Added");
            }


            Console.WriteLine("Retrieving Employee from StartDate");
            EmployeeModel model1 = new EmployeeModel()
            {
                StartDate = DateTime.Parse("2018-06-26")
            };
            repo.RetrieveEmployeeBasedOnStartDate(model1);

        }
    }
}