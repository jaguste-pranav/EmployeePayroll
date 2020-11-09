using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollService
{
    public class SalaryDetailModel
    {
        public int EmployeeId
        {
            get;
            set;
        }
        public int SalaryId
        {
            get;
            set;
        }
        public string EmployeeName
        {
            get;
            set;
        }
        public string Designation
        {
            get;
            set;
        }
        public int Salary
        {
            get;
            set;
        }
        public string Month
        {
            get;
            set;
        }
    }
}
