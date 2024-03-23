using System;
using System.Collections.Generic;
using System.IO;
namespace EmployeeManagementSystem
{    class Program
    {
        static List<Employee> employees = new List<Employee>();
        static string filePath = "employees.txt";
        static void Main(string[] args)
        {
            LoadEmployeesFromFile();

            while (true)
            { // The main menu loop
                Console.WriteLine("\nEmployee Management System");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Save Employees");
                Console.WriteLine("3. View All Employees");
                Console.WriteLine("4. Edit Employee");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddEmployee(); 
                        break;
                    case 2:
                        SaveEmployeesToFile();
                        break;
                    case 3:
                        DisplayEmployees();
                        break;
                    case 4:
                        EditEmployee();
                        break;
                    case 5:
                        SaveEmployeesToFile(); 
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void AddEmployee()
        {
            // Prompt user for employee information
            Console.Write("Enter employee name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid ID. Please enter a positive integer.");
                return;
            }

            // Create a new employee object and add it to the list
            Employee employee = new Employee(name, id);
            employees.Add(employee);

            Console.WriteLine("Employee added successfully.");
        }
        static void EditEmployee()
        {
            // Prompt user for employee ID to edit
            Console.Write("Enter employee ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid ID. Please enter a positive integer.");
                return;
            }
            // Find the index of the employee with the specified ID
            int index = employees.FindIndex(emp => emp.Id == id);
            if (index != -1)
            { // Prompt user for new name and update the employee object
                Employee employee = employees[index];
                Console.Write("Enter new name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    employee.Name = name;
                }// Update the employee object
                employees[index] = employee;

                Console.WriteLine("Employee details updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static void DisplayEmployees()
        { // Display employees and ID
            Console.WriteLine("\nEmployee List:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Name: {employee.Name}, ID: {employee.Id}");
            }
        }

        static void LoadEmployeesFromFile()
        { // Load employees from file if any employess are in file
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length >= 2 && int.TryParse(data[1], out int id))
                        {
                            Employee employee = new Employee(data[0], id);
                            employees.Add(employee);
                        }
                    }
                }
            }
        }

        static void SaveEmployeesToFile()
        {   // Save employees to file
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var employee in employees)
                {
                    sw.WriteLine($"{employee.Name},{employee.Id}");
                }
            }
            Console.WriteLine("Employees saved to file.");
        }

        public struct Employee
        {
            public string Name { get; set; }
            public int Id { get; set; }

            public Employee(string name, int id)
            {
                Name = name;
                Id = id;
            }
        }
    }
}
