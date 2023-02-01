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
    public partial class CreateEmployee : Window
    {
        public CreateEmployee()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = Logic.createEmployee(textName.Text, textSurname.Text, textPatronymic.Text, textEmail.Text);

            setTextColor(employee);

            if (employee.Employee_Name != "" && employee.Employee_Surname != "" && employee.Employee_Patronymic != "" && employee.Employee_Email != "") // Проверка на заполненность полей
            {
                Logic.createElement(null, employee);
                MessageBox.Show("Новый сотрудник успешно создан", "Уведомление");
                this.Close();
            }
            else
            {
                textError.Visibility = Visibility.Visible;
                setTextColor(employee);
                MessageBox.Show("Пустой ввод", "Ошибка");
            }

        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void setTextColor(Employee employee)
        {
            if (employee.Employee_Name == "")
            {
                labelName.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelName.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (employee.Employee_Surname == "")
            {
                labelSurname.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelSurname.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (employee.Employee_Patronymic == "")
            {
                labelPatronymic.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelPatronymic.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (employee.Employee_Email == "")
            {
                labelEmail.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                labelEmail.Foreground = new SolidColorBrush(Colors.Black);
            }

        }

        
    }
}
