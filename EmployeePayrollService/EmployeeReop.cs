using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string connectionstring = @"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=new_payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionstring);

        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();

                using (this.connection)
                {
                    string query = "select * from emp_payroll";
                    SqlCommand sql = new SqlCommand(query, this.connection);
                    this.connection.Open();

                    SqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.EmployeeID = dr.GetInt32(0);
                            model.EmployeeName = dr.GetString(1);
                            model.PhoneNumber = dr.GetString(2);
                            model.Address = dr.GetString(3);
                            model.Department = dr.GetString(4);
                            model.Gender = Convert.ToChar(dr.GetString(5));
                            model.BasicPay = (dr.GetDecimal(6));
                            model.Deductions = (dr.GetDecimal(7));
                            model.TaxablePay = (dr.GetDecimal(8));
                            model.Tax = (dr.GetDecimal(9));
                            model.NetPay = (dr.GetDecimal(10));
                            model.StartDate = (dr.GetDateTime(11));
                            model.City = dr.GetString(12);
                            model.Country = dr.GetString(13);

                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", (model.EmployeeID), model.EmployeeName, model.PhoneNumber, model.Address, model.Department, model.Gender);
                        }
                        dr.Close();
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}