using SQL_EntityFramework.WPF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;

namespace SQL_EntityFramework.Classes
{
    public class Logic
    {
        public static string inputText(string input)
        {
            if (input.Replace(" ", "") == "") return "";
            return input;
        }

        public static Project createProject(string name, string clientCompany, string executorCompany, string startDate, string endDate, int priority)
        {
            Project project = new Project
            {
                Project_Name = inputText(name),
                Project_ClientCompany = inputText(clientCompany),
                Project_ExecutorCompany = inputText(executorCompany),
                Project_StartDate = inputText(startDate),
                Project_EndDate = inputText(endDate),
                Project_Priority = priority
            };
            return project;         
        }

        public static Employee createEmployee(string name, string surname, string patronymic, string email)
        {
            Employee employee = new Employee
            {
                Employee_Name = name,
                Employee_Surname = surname,
                Employee_Patronymic = patronymic,
                Employee_Email = email

            };
            return employee;
        }

        public static void createElement(Project project=null, Employee employee = null)
        {
            if (project != null)
            {
                DataWork.addNewProject(project);
            }else if(employee != null)
            {
                DataWork.addNewEmployee(employee);
            }

        }

        public static void updateElement(int ID, Project project = null, Employee employee = null)
        {
            
            if (project != null)
            {
                DataWork.updateProject(ID, project);
            }
            else if (employee != null)
            {
                DataWork.updateEmployee(ID, employee);
            }
        }
        public static void deleteItem(int ID, string table)
        {
            if(table == "Project")
            {
                DataWork.removeProject(ID);
            }
            else
            {
                DataWork.removeEmployee(ID);
            }
        }

        public static List<Employee> fillProjectEmployees(int ID, int Manager = 0) 
        {
            if (Manager != 0 && Manager != 1) Manager = 0;

            var projectEmployee = DataWork.getProjectEmployee(ID);

            List<Employee> employees = new List<Employee>();
            foreach(ProjectEmployee pe in projectEmployee)
            {
                if (pe.IsManager == Manager)
                {
                    employees.Add(pe.Employee);
                }
            }

            return employees;
        }
        public static List<Employee> fillEmployeesToAdding(int ID)
        {
            var projectEmployee = DataWork.getAllProjectEmployee();

            List<Employee> employeesInProject = new List<Employee>();
            foreach (ProjectEmployee pe in projectEmployee)
            {
                if (pe.ProjectID == ID)
                {
                    employeesInProject.Add(pe.Employee);
                }
            }

            var employees = DataWork.getAllEmployees();
            employees.RemoveAll(item => employeesInProject.Any(item2 => item.Employee_ID == item2.Employee_ID));
            return employees;
        }

        public static void createProjectEmployee(int projectID, int employeeID)
        {
            ProjectEmployee pe = new ProjectEmployee
            {
                ProjectID = projectID,
                EmployeeID = employeeID,
                IsManager = 0,
            };

            DataWork.addEmployeeToProject(pe);
        }

        public static void changeManager(int projectID,int employeeID, bool manager)
        {
            if (manager)
            {
                DataWork.managerToEmployee(projectID, employeeID);
            }
            else
            {
                DataWork.employeeToManager(projectID, employeeID);
            }
        }
        public static bool haveManager(int projectID)
        {
            var employees = fillProjectEmployees(projectID, 1);
            if (employees.Count != 0) return true;
            return false;
        }

        public static void removeEmployeeFromProject(int projectID, int employeeID)
        {
            DataWork.removeProjectEmployee(projectID, employeeID);
        }

        public static List<string> getItemForFilters(string selectedFilter, string table)
        {
            List<string> filters = new List<string>();

            if(table == "Project")
            {
                var projects = DataWork.getAllProjects();
                foreach (var project in projects)
                {
                    if (selectedFilter == "Название") filters.Add(project.Project_Name);
                    if (selectedFilter == "Компания-заказчик") filters.Add(project.Project_ClientCompany);
                    if (selectedFilter == "Компания-исполнитель") filters.Add(project.Project_ExecutorCompany);
                    if (selectedFilter == "Приоритет") filters.Add(project.Project_Priority.ToString());
                }
            }
            else
            {
                var employees = DataWork.getAllEmployees();
                foreach (var employee in employees)
                {
                    if (selectedFilter == "Имя") filters.Add(employee.Employee_Name);
                    if (selectedFilter == "Фамилия") filters.Add(employee.Employee_Surname);
                    if (selectedFilter == "Отчество") filters.Add(employee.Employee_Patronymic);
                    if (selectedFilter == "Email") filters.Add(employee.Employee_Email);
                }
            }

            filters = filters.Distinct().ToList();
            return filters;
        }

        public static List<Project> getProjectsForMainGrid(string mainFilter=null,string filter = null, string search = "")
        {
            var projects = DataWork.getAllProjects();

            List<Project> filtered = new List<Project>();
            if (filter != null)
            {
                foreach (var project in projects)
                {
                    if (mainFilter == "Название" && project.Project_Name == filter) filtered.Add(project);
                    if (mainFilter == "Компания-заказчик" && project.Project_ClientCompany == filter) filtered.Add(project);
                    if (mainFilter == "Компания-исполнитель" && project.Project_ExecutorCompany == filter) filtered.Add(project);
                    if (mainFilter == "Приоритет" && project.Project_Priority.ToString() == filter) filtered.Add(project);
                }
            }
            else
            {
                filtered = projects;
            }

            List<Project> result = new List<Project>();
            foreach (var project in filtered)
            {
                if(project.Project_Name.IndexOf(search) != -1 || project.Project_ClientCompany.IndexOf(search) != -1 ||
                    project.Project_ExecutorCompany.IndexOf(search) != -1 || project.Project_Priority.ToString().IndexOf(search) != -1 ||
                    project.Project_StartDate.IndexOf(search) != -1 || project.Project_EndDate.IndexOf(search) != -1)
                {
                    result.Add(project);
                }
            }

            return result;
        }
        public static List<Employee> getEmployeesForMainGrid(string mainFilter = null, string filter = null, string search = "")
        {
            var employees = DataWork.getAllEmployees();

            List<Employee> filtered = new List<Employee>();
            if (filter != null)
            {
                foreach (var employee in employees)
                {
                    if (mainFilter == "Имя" && employee.Employee_Name == filter) filtered.Add(employee);
                    if (mainFilter == "Фамилия" && employee.Employee_Surname == filter) filtered.Add(employee);
                    if (mainFilter == "Отчество" && employee.Employee_Patronymic == filter) filtered.Add(employee);
                    if (mainFilter == "Email" && employee.Employee_Email == filter) filtered.Add(employee);
                }
            }
            else
            {
                filtered = employees;
            }

            List<Employee> result = new List<Employee>();
            foreach (var employee in filtered)
            {
                if (employee.Employee_Name.IndexOf(search) != -1 || employee.Employee_Surname.IndexOf(search) != -1 ||
                    employee.Employee_Patronymic.IndexOf(search) != -1 || employee.Employee_Email.IndexOf(search) != -1)
                {
                    result.Add(employee);
                }
            }

            return result;
        }

     
    }
}
