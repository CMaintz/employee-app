using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            Task<string> task = client.GetStringAsync("https://localhost:44367/api/employeeapi/getemployee/1");
            
            String msg = task.Result;

            DTO.Model.Employee emp = JsonSerializer.Deserialize<DTO.Model.Employee>(msg);
            Resultlabel.Content = emp.Name + " " + emp.YearsEmployed;



        }

        private void TestPostButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            DTO.Model.Employee emp = new DTO.Model.Employee();
            emp.Name = "Patrick";
            emp.YearsEmployed = 37;
            emp.CompanyId = 1;
            StringContent content = new StringContent(JsonSerializer.Serialize(emp),Encoding.UTF8, "application/json");
            //Eller brug den nye JsonContent
            HttpResponseMessage result = client.PostAsync("https://localhost:44367/api/employeeapi/addemployee", content).Result;
            Resultlabel.Content = result.StatusCode.ToString();
        }
    }
}
