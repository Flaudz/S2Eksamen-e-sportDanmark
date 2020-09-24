using System;
using System.Collections.Generic;
using System.Text;

namespace EsportDanmark.Classes
{
    public class Employee
    {
        private int id;
        private string name;
        private int phonenumber;
        private int salary;
        private string jobtype;
        private int judgelevel;

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public int Phonenumber { get => phonenumber; set => phonenumber = value; }
        public int Salary { get => salary; set => salary = value; }
        public string Jobtype { get => jobtype; set => jobtype = value; }
        public int Judgelevel { get => judgelevel; set => judgelevel = value; }

        public Employee(string name, int phonenumber, int salary, string jobtype, int judgelevel)
        {
            Name = name;
            Phonenumber = phonenumber;
            Salary = salary;
            Jobtype = jobtype;
            Judgelevel = judgelevel;
        }
    }
}
