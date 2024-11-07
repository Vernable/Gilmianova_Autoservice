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

namespace Gilmianova_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentService=new Service();
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
            if(SelectedService != null)
                _currentService = SelectedService;
            DataContext = _currentService;
        }
        public AddEditPage()
        {
            InitializeComponent();
            DataContext = _currentService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentService.Title))
                errors.AppendLine("укажите название услуги.");

            if (_currentService.Cost == 0)
                errors.AppendLine("Укажите стоимость услуги");
            if (_currentService.DiscountInt < 0 )
                errors.AppendLine("Укажите скидку");
            if (_currentService.DurationInSeconds>0 || _currentService.DurationInSeconds>240)
                errors.AppendLine("Длительность не может быть больше 240 минут.");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var allServices=Gilmianova_AutoserviceEntities.GetContext().Service.ToList();
            allServices=allServices.Where(p=>p.Title==_currentService.Title).ToList();
            if (allServices.Count == 0)
            {
                if (_currentService.ID == 0)
                {
                    Gilmianova_AutoserviceEntities.GetContext().Service.Add(_currentService);

                }
                try
                {
                    Gilmianova_AutoserviceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }

        }
    }
}
