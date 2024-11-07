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

namespace WpfApp6
{
    
    public partial class MainWindow : Window
    {
        EmployeesEntities2 Db = new EmployeesEntities2();
        public MainWindow()
        {
            InitializeComponent();
            Container.ItemsSource = Db.Employees.ToList();
        }

        private void InsertBtn(object sender, RoutedEventArgs e)
        {

            try
            {
            if(statetxt.Text == "" || nametxt.Text == "")
            {
                MessageBox.Show("Some Filed are not Filled");
                return;
            }
            if(idtxt.Text != "")
            {
                MessageBox.Show("You Dont have the permission to Set An Id");
                return;
            }

            Employee emp = new Employee()
            {
                EmpName = nametxt.Text,
                EmpState = statetxt.Text
            };
            Db.Employees.Add(emp);
                Db.SaveChanges();
                MessageBox.Show("Emp Added Succussfully");
            Container.ItemsSource = Db.Employees.ToList();
            nametxt.Text = "";
            statetxt.Text = "";
            idtxt.Text = "";
            }
            catch(Exception Excp)
            {
                Console.WriteLine($"Error : {Excp}");
            }
        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {
            try
            {
            if(idtxt.Text == "")
            {
                MessageBox.Show("Delete button is reqiured");
                    return;
            }
            var emp = Db.Employees.FirstOrDefault(Emp => Emp.EmpId.ToString() == idtxt.Text);
            emp.EmpName = nametxt.Text != "" ? nametxt.Text : emp.EmpName;
            emp.EmpState = statetxt.Text != "" ? nametxt.Text : emp.EmpState;
            Db.SaveChanges();
            MessageBox.Show("Updated Successfully");
            Container.ItemsSource = Db.Employees.ToList();
            nametxt.Text = "";
            statetxt.Text = "";
             idtxt.Text = "";
            }
            catch(Exception Exp)
            {
                MessageBox.Show($"Error : {Exp}");
            }
        }

        private void DeleteBtn(object sender, RoutedEventArgs e)
        {
            try {
            if (idtxt.Text == "")
            {
                MessageBox.Show("Some Filed are not Filled");
                return;
            }
                var emp = Db.Employees.Where(Emp => Emp.EmpId.ToString() == idtxt.Text).First(); 
                Db.Employees.Remove(emp);
                Db.SaveChanges();
                MessageBox.Show("Deleted Successfully");
                Container.ItemsSource = Db.Employees.ToList();
                nametxt.Text = "";
                statetxt.Text = "";
                idtxt.Text = "";
            }
            catch(Exception Exp)
            {
                Console.WriteLine($"Error : {Exp}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(searchtxt.Text == "")
            {
                Container.ItemsSource = Db.Employees.ToList();
                return;
            }
            var Employees = Db.Employees.Where(emp => emp.EmpName == searchtxt.Text).ToList();
            Container.ItemsSource = Employees;
        }
    }
}
            //var Employees = Db.Employees.Where(emp => emp.EmpName.Contains(searchtxt.Text)).ToList();
