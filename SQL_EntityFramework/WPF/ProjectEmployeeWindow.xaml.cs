using SQL_EntityFramework.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQL_EntityFramework.WPF
{
    public partial class ProjectEmployeeWindow : Window
    {
        int projectID = -1;
        int employeeID = -1;
        bool manager = false;
        bool updateSelection = true;

        public ProjectEmployeeWindow(int ID)
        {
            InitializeComponent();
            projectID = ID;
            updateGrid();
        }
        private void updateGrid()
        {
            gridManager.ItemsSource = Logic.fillProjectEmployees(projectID, 1);
            gridEmployee.ItemsSource = Logic.fillProjectEmployees(projectID);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee window = new AddEmployee(projectID);
            window.Closing += (o, s) => { updateGrid(); };
            window.ShowDialog();
        }
        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(employeeID != -1)
            {
                Logic.removeEmployeeFromProject(projectID, employeeID);
                updateGrid();
            }
            else
            {
                MessageBox.Show("Нужно выбрать сотрудника", "Ошибка");
            }
        }
        private void ChangeManager_Click(object sender, RoutedEventArgs e)
        {
            if(manager == false && Logic.haveManager(projectID) == true)
            {
                MessageBox.Show("Руководитель проетка уже назначен", "Ошибка");
            }
            else
            {
                Logic.changeManager(projectID, employeeID, manager);
                updateGrid();
            }
            
        }
        private void gridManager_AutoGeneratedColumns(object sender, EventArgs e)
        {
            renameColumns(gridManager);
        }

        private void gridEmployee_AutoGeneratedColumns(object sender, EventArgs e)
        {
            renameColumns(gridEmployee);
        }

        private void renameColumns(DataGrid grid)
        {
            grid.Columns[0].Visibility = Visibility.Hidden;
            grid.Columns[1].Header = "Имя";
            grid.Columns[2].Header = "Фамилия";
            grid.Columns[3].Header = "Отчество";
            grid.Columns[4].Header = "Email";
            grid.Columns[5].Visibility = Visibility.Hidden;

        }

        private void gridManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updateSelection)
            {
                updateSelection= false;
                gridEmployee.SelectedItem= null;
                try
                {
                    Employee employee = (Employee)gridManager.SelectedItem;
                    employeeID = employee.Employee_ID;
                    manager = true;
                }
                catch
                {
                    employeeID = -1;
                }
                buttonChangeManager();
                updateSelection = true;
            }
        }
        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updateSelection)
            {
                updateSelection = false;
                gridManager.SelectedItem = null;
                    try
                {
                    Employee employee = (Employee)gridEmployee.SelectedItem;
                    employeeID = employee.Employee_ID;
                    manager = false;
                }
                catch
                {
                    employeeID = -1;
                }
                buttonChangeManager();
                updateSelection = true;
            }
        }

        private void buttonChangeManager()
        {
            if(employeeID != -1)
            {
                ChangeManager.Visibility = Visibility.Visible;
            }
            else
            {
                ChangeManager.Visibility = Visibility.Hidden;
            }

            if (manager)
            {
                ChangeManager.Content = "Понизить до сотрудника";
            }
            else
            {
                ChangeManager.Content = "Назначить руководителем";
            }

        }
    }
}
