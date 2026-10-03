using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
using BusinessLogic.BLL;
using DTO.Model;

namespace EmployeeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private EmployeeBLL empBll = new EmployeeBLL();
        private CompanyBLL compBll = new CompanyBLL();

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _companyEmployees = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> CompanyEmployees
        {
            get => _companyEmployees;
            set
            {
                if (_companyEmployees != value)
                {
                    _companyEmployees = value;
                    OnPropertyChanged(nameof(CompanyEmployees));
                }
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    lblClickedEmployeeDeets.Visibility = Visibility.Visible;
                    OnPropertyChanged(nameof(SelectedEmployee));
                } else
                {
                    lblClickedEmployeeDeets.Visibility = Visibility.Hidden;
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void GetEmployeeByID(object sender, RoutedEventArgs e)
        {
 
            var id = searchFieldEmployee.Text;
            DTO.Model.Employee tempEmployee;
            try
            {
                tempEmployee = empBll.getEmployee(int.Parse(id));
                if (tempEmployee == null)
                {
                    txtBlkEmployeeDetails.Text = $"No employee with ID {id}";
                    return;
                }
                txtBlkEmployeeDetails.Text = $"Name: {tempEmployee.Name} (ID: {tempEmployee.EmployeeId})" +
                    $"\nCompany: {compBll.getCompany(tempEmployee.CompanyId).Name} (ID: {tempEmployee.CompanyId})" +
                    $"\nYears employed: {tempEmployee.YearsEmployed}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           //TODO: if not a number, remove it? Or make an alert warning thang. (Unless fields can be set to only numericals)

        }

        private void GetCompanyByID(object sender, RoutedEventArgs e)
        {
            var searchInput = searchFieldCompany.Text.Trim();
            
            try
            {
                var parsedInput = int.Parse(searchInput);
                if (parsedInput > 0)
                {
                    DTO.Model.Company tempCompany = compBll.getCompany(parsedInput);
                    if (tempCompany == null)
                    {
                        txtBlkCompanyDetails.Text = $"No company with ID {parsedInput}";
                        CompanyEmployees = new ObservableCollection<Employee>();
                        return;
                    }

                    txtBlkCompanyDetails.Text = "Company Details\n" + tempCompany.DisplayDetails();

                    CompanyEmployees = new ObservableCollection<Employee>(tempCompany.Employees);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                txtBlkCompanyDetails.Text = ex.Message;
                CompanyEmployees = new ObservableCollection<Employee>();
            }

         

        }
    }

}