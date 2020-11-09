using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollService
{
    public class FindSumAvgMinMaxSalary
    {
        public char gender
        {
            get;
            set;
        }
        public int count
        {
            get;
            set;
        }
        public decimal totalSum
        {
            get;
            set;
        }
        public decimal avg
        {
            get;
            set;
        }
        public decimal min
        {
            get;
            set;
        }
        public decimal max
        {
            get;
            set;
        }
    }
}