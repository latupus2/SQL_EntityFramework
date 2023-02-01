using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Deployment.Internal;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SQL_EntityFramework.Classes
{
    public class DataWork
    {

        public static List<Project> getAllProjects()
        {
            var data = new MyDataEntities();
            var projects = data.Project.ToList();
            return projects;
        }

        public static List<Employee> getAllEmployees()
        {
            var data = new MyDataEntities();
            var employees = data.Employee.ToList();
            return employees;
        }

        public static void addNewProject(Project project)
        {
            var data = new MyDataEntities();
            data.Project.Add(project);
            data.SaveChanges();
        }

        public static void updateProject(int ID,Project newProject)
        {
            var data = new MyDataEntities();

            Project project = data.Project
                .Where(c => c.Project_ID == ID)
                .FirstOrDefault();

            project.Project_Name = newProject.Project_Name;
            project.Project_ClientCompany = newProject.Project_ClientCompany;
            project.Project_ExecutorCompany = newProject.Project_ExecutorCompany;
            project.Project_StartDate = newProject.Project_StartDate;
            project.Project_EndDate = newProject.Project_EndDate;
            project.Project_Priority = newProject.Project_Priority;

            data.SaveChanges();
        }
        public static void removeProject(int ID)
        {
            var data = new MyDataEntities();

            data.ProjectEmployee.RemoveRange(data.ProjectEmployee.Where(p => p.ProjectID == ID));
            data.Project.Remove(data.Project.Where(c => c.Project_ID == ID).FirstOrDefault());

            data.SaveChanges();
        }

        public static void addNewEmployee(Employee employee)
        {
            var data = new MyDataEntities();
            data.Employee.Add(employee);
            data.SaveChanges();
        }

        public static void updateEmployee(int ID, Employee newEmployee)
        {
            var data = new MyDataEntities();

            Employee employee = data.Employee
                .Where(c => c.Employee_ID == ID)
                .FirstOrDefault();

            employee.Employee_Name = newEmployee.Employee_Name;
            employee.Employee_Surname = newEmployee.Employee_Surname;
            employee.Employee_Patronymic = newEmployee.Employee_Patronymic;
            employee.Employee_Email = newEmployee.Employee_Email;

            data.SaveChanges();
        }
        public static void removeEmployee(int ID)
        {
            var data = new MyDataEntities();

            data.ProjectEmployee.RemoveRange(data.ProjectEmployee.Where(p => p.EmployeeID == ID));
            data.Employee.Remove(data.Employee.Where(c => c.Employee_ID == ID).FirstOrDefault());

            data.SaveChanges();
        }

        public static List<ProjectEmployee> getProjectEmployee(int projectID)
        {
            var data = new MyDataEntities();

            var pe = data.ProjectEmployee
                .Where(p => p.ProjectID == projectID)
                .ToList();

            return pe;     
        }

        public static List<ProjectEmployee> getAllProjectEmployee()
        {
            var data = new MyDataEntities();

            var pe = data.ProjectEmployee
                .ToList();

            return pe;
        }

        public static void addEmployeeToProject(ProjectEmployee pe)
        {
            var data = new MyDataEntities();
            data.ProjectEmployee.Add(pe);
            data.SaveChanges();
        }

        public static void employeeToManager(int projectID, int employeeID)
        {
            var data = new MyDataEntities();

            ProjectEmployee pe = data.ProjectEmployee
                .Where(c => c.EmployeeID == employeeID && c.ProjectID == projectID)
                .FirstOrDefault();

            pe.IsManager = 1;

            data.SaveChanges();
        }
        public static void managerToEmployee(int projectID, int employeeID)
        {
            var data = new MyDataEntities();

            ProjectEmployee pe = data.ProjectEmployee
                .Where(c => c.EmployeeID == employeeID && c.ProjectID == projectID)
                .FirstOrDefault();

            pe.IsManager = 0;

            data.SaveChanges();
        }

        public static void removeProjectEmployee(int projectID, int employeeID)
        {
            var data = new MyDataEntities();

            data.ProjectEmployee.Remove(data.ProjectEmployee.Where(p => p.ProjectID == projectID && p.EmployeeID == employeeID).FirstOrDefault());

            data.SaveChanges();
        }
    }
}
