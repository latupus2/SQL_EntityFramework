﻿using SQL_EntityFramework.Classes;
using SQL_EntityFramework.WPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQL_EntityFramework
{
    public partial class MainWindow : Window
    {

        string openTable = "Project";
        int selectedItemID = -1;

        public MainWindow()
        {
            InitializeComponent();
            updateGrid();
            updateMainFilters();
        }

        private void updateGrid(string mainFilter = null, string filter = null, string search = "")
        {
            if(openTable == "Project")
            {
                MainGrid.ItemsSource = Logic.getProjectsForMainGrid(mainFilter, filter, search);
            }
            else
            {
                MainGrid.ItemsSource = Logic.getEmployeesForMainGrid(mainFilter, filter, search);
            }
    
        }
        private void updateMainFilters()
        {
            comboFilter.Items.Clear();
            if (openTable == "Project")
            {
                comboFilter.Items.Add("Все");
                comboFilter.Items.Add("Название");
                comboFilter.Items.Add("Компания-заказчик");
                comboFilter.Items.Add("Компания-исполнитель");
                comboFilter.Items.Add("Приоритет");
            }
            else
            {
                comboFilter.Items.Add("Все");
                comboFilter.Items.Add("Имя");
                comboFilter.Items.Add("Фамилия");
                comboFilter.Items.Add("Отчество");
                comboFilter.Items.Add("Email");
            }
        }

        private void updateFiltersSource(string selectedFilter)
        {
            comboFilterSource.SelectedItem = null;
            comboFilterSource.ItemsSource = Logic.getItemForFilters(selectedFilter, openTable);
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (openTable == "Project")
                {
                    Project project = (Project)MainGrid.SelectedItem;
                    selectedItemID = project.Project_ID;
             

                }else
                {
                    Employee employee = (Employee)MainGrid.SelectedItem;
                    selectedItemID = employee.Employee_ID;
                }
            }
            catch
            {
                selectedItemID = -1;
            }
        }

        private void MainGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            if (openTable == "Project")
            {
                MainGrid.Columns[0].Visibility = Visibility.Hidden;
                MainGrid.Columns[1].Header = "Название";
                MainGrid.Columns[2].Header = "Компания-заказчик";
                MainGrid.Columns[3].Header = "Компания-исполнитель";
                MainGrid.Columns[4].Header = "Дата начала";
                MainGrid.Columns[5].Header = "Дата окончания";
                MainGrid.Columns[6].Header = "Приоритет";
                MainGrid.Columns[7].Visibility = Visibility.Hidden;
            }
            else
            {
                MainGrid.Columns[0].Visibility = Visibility.Hidden;
                MainGrid.Columns[1].Header = "Имя";
                MainGrid.Columns[2].Header = "Фамилия";
                MainGrid.Columns[3].Header = "Отчество";
                MainGrid.Columns[4].Header = "Email";
                MainGrid.Columns[5].Visibility = Visibility.Hidden;
            }
        }

        private void buttonShowProjects_Click(object sender, RoutedEventArgs e)
        {
            openTable = "Project";
            buttonProjectEmployee.Visibility = Visibility.Visible;
            updateGrid(null, null, Search.Text);
            updateMainFilters();
        }

        private void buttonShowEmployee_Click(object sender, RoutedEventArgs e)
        {
            openTable = "Employee";
            buttonProjectEmployee.Visibility = Visibility.Hidden;
            updateGrid(null, null, Search.Text);
            updateMainFilters();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            if(openTable == "Project")
            {
                CreateProject window = new CreateProject();
                window.Closing += (o,s) => { updateGrid(); };
                window.ShowDialog();  
                
            }else
            {
                CreateEmployee window = new CreateEmployee();
                window.Closing += (o, s) => { updateGrid(); };
                window.ShowDialog();
            }
        }

        private void buttonDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if(selectedItemID != -1)
            {
                Logic.deleteItem(selectedItemID, openTable);
                try
                {
                    var mainFilter = comboFilter.SelectedItem.ToString();
                    var filter = comboFilterSource.SelectedItem.ToString();
                    updateGrid(comboFilter.SelectedItem.ToString(), comboFilterSource.SelectedItem.ToString(), Search.Text);
                }
                catch
                {
                    updateGrid(null, null, Search.Text);
                };
            }
            else
            {
                MessageBox.Show("Нужно выбрать элемент", "Ошибка");
            }
        }

        private void buttonEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItemID != -1)
            {
                if(openTable == "Project")
                {
                    EditProject window = new EditProject((Project)MainGrid.SelectedItem);
                    window.Closing += (o, s) => { updateGrid(); };
                    window.ShowDialog();
                }
                else
                {
                    EditEmployee window = new EditEmployee((Employee)MainGrid.SelectedItem);
                    window.Closing += (o, s) => { updateGrid(); };
                    window.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Нужно выбрать элемент", "Ошибка");
            }
        }

        private void buttonProjectEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItemID != -1)
            {
                ProjectEmployeeWindow window = new ProjectEmployeeWindow(((Project)MainGrid.SelectedItem).Project_ID);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нужно выбрать элемент", "Ошибка");
            }

        }

        private void comboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                updateFiltersSource(comboBox.SelectedItem.ToString());
            }
            catch 
            {
                updateFiltersSource(null);
            };
            
        }

        private void comboFilterSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                updateGrid(comboFilter.SelectedItem.ToString(),comboBox.SelectedItem.ToString(),Search.Text);
            }
            catch 
            { 
                updateGrid(null, null, Search.Text);
            };

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = Search.Text;
            try
            {
                var mainFilter = comboFilter.SelectedItem.ToString();
                var filter = comboFilterSource.SelectedItem.ToString();
                updateGrid(comboFilter.SelectedItem.ToString(), comboFilterSource.SelectedItem.ToString(), search);
            }
            catch
            {
                updateGrid(null, null, search);
            };
        }
    }
}
