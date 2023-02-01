using SQL_EntityFramework.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// Логика взаимодействия для EditProject.xaml
    /// </summary>
    public partial class EditProject : Window
    {
        int ID = 0;
        public EditProject(Project project)
        {
            InitializeComponent();
            fillLabels(project);
            ID = project.Project_ID;
        }

        private void fillLabels(Project project)
        {
            textName.Text = project.Project_Name;
            textCompanyClient.Text = project.Project_ClientCompany;
            textCompanyExecutor.Text = project.Project_ExecutorCompany;
            timeStartDate.Text = project.Project_StartDate;
            timeEndDate.Text = project.Project_EndDate;
            textPriority.Text = project.Project_Priority.ToString();

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)// Запрет на ввод букв для приоритета
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSaveProject_Click(object sender, RoutedEventArgs e)
        {
            int priority = -1;
            try
            {
                priority = int.Parse(textPriority.Text);
            }
            catch { };

            Project project = Logic.createProject(textName.Text, textCompanyClient.Text, textCompanyExecutor.Text, timeStartDate.Text, timeEndDate.Text, priority);

            setTextColor(project);

            if (project.Project_Name != "" && project.Project_ClientCompany != "" && project.Project_ExecutorCompany != "" && project.Project_StartDate != "" && project.Project_EndDate != "" && project.Project_Priority != -1) // Проверка на заполненность полей
            {
                Logic.updateElement(ID, project, null);
                MessageBox.Show("Сотрудник успешно обновлен", "Уведомление");
                this.Close();
            }
            else
            {
                textError.Visibility = Visibility.Visible;
                setTextColor(project);
                MessageBox.Show("Пустой ввод", "Ошибка");
            }

        }

        private void setTextColor(Project project)
        {
            if (project.Project_Name == "")
            {
                labelName.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelName.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (project.Project_ClientCompany == "")
            {
                labelClient.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelClient.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (project.Project_ExecutorCompany == "")
            {
                labelExecutor.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelExecutor.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (project.Project_StartDate == "")
            {
                labelStartDate.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelStartDate.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (project.Project_EndDate == "")
            {
                labelEndDate.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelEndDate.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (project.Project_Priority == -1)
            {
                labelPriority.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelPriority.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
    
}
