using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        string connectionstring = @"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=new_payroll_service;Integrated Security=True";

        SqlConnection connection;

        public void GetAllEmployee()
        {
            try
            {
                connection = new SqlConnection(connectionstring);

                EmployeeModel model = new EmployeeModel();

                using (connection)
                {
                    string query = "select * from emp_payroll";
                    SqlCommand sql = new SqlCommand(query, connection);
                    connection.Open();

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
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    dr.Close();
                    //this.connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// UC2 Add Employee Using Stored Procedures
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (connection)
                {
                    connection = new SqlConnection(connectionstring);
                    SqlCommand command = new SqlCommand("Sp_AddEmployee", connection);
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpName", model.EmployeeName);
                    command.Parameters.AddWithValue("@EmpPhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@EmpAddress", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@Country", model.Country);

                    var result = command.ExecuteNonQuery();
                    //this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public int UpdateEmployee(SalaryDetailModel model)
        {
            int salary = 0;
            try
            {
                using (connection)
                {
                    connection = new SqlConnection(connectionstring);
                    SalaryDetailModel displayModel = new SalaryDetailModel();

                    SqlCommand command = new SqlCommand("Sp_UpdateEmployee_Payroll", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@S_id", model.SalaryId);
                    //command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    //command.Parameters.AddWithValue("@Designation", model.Designation);
                    command.Parameters.AddWithValue("@salary", model.Salary);
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@E_id", model.EmployeeId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            displayModel.EmployeeId = reader.GetInt32(0);
                            displayModel.EmployeeName = reader.GetString(1);
                            displayModel.Designation = reader.GetString(2);
                            displayModel.Month = reader.GetString(3);
                            displayModel.SalaryId = reader.GetInt32(4);
                            displayModel.Salary = reader.GetInt32(5);

                            salary = displayModel.Salary;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
                    return salary;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// UC5
        /// </summary>
        /// <param name="model"></param>
        public void RetrieveEmployeeBasedOnStartDate(EmployeeModel model)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("Sp_RetrieveEmployeeBasedOnStartDate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StartDate", model.StartDate);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.EmployeeID = reader.GetInt32(0);
                        model.EmployeeName = reader.GetString(1);
                        model.Address = reader.GetString(2);
                        model.Department = reader.GetString(3);
                        model.BasicPay = Convert.ToDecimal(reader.GetInt32(4));
                        model.StartDate = reader.GetDateTime(5);
                        Console.WriteLine("{0},{1},{2},{3},{4},{5}", model.EmployeeID, model.EmployeeName, model.Address, model.Department, model.BasicPay, model.StartDate);
                    }
                }
                else
                {
                    Console.WriteLine("No rows Found");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}