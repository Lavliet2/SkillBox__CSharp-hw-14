using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ClassLibrary;

namespace Homework_12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repository;
        public MainWindow()
        {
            InitializeComponent();
            repository = new Repository();
            DataContext = repository;
            repository.PropertyChanged += Repository_PropertyChanged;
        }

        private void Repository_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Repository.Clients))
            {
                lv_Clients.ItemsSource = null;
                lv_Clients.ItemsSource = repository.Clients;
            }
        }
        private void OpenAccount_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            ListViewItem listViewItem = FindVisualParent<ListViewItem>(button);
            int index = lv_Clients.ItemContainerGenerator.IndexFromContainer(listViewItem);
            lv_Clients.SelectedIndex = index;

            Client selectedClient = (Client)lv_Clients.SelectedItem;
            if (lv_Clients.SelectedItem == null) return;

            EditAccount editAccount = new EditAccount(selectedClient, repository);
            editAccount.Show();

        }
        private void DelAccount_Click(object sender, RoutedEventArgs e)
        {
            repository.RemoveClientAt(lv_Clients.SelectedIndex);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lb_ErrorLog.Content = string.Empty;
            if(!repository.AddClient(tb_Client.Text))
            {
                lb_ErrorLog.Content = "Формат ввода: Фамилия Имя Отчество";
            }
            else
            {
                tb_Client.Text = string.Empty;
            }
        }

        private T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                if (parent is T correctlyTyped)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
}
