using ClassLibrary;
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

namespace Homework_12
{
    /// <summary>
    /// Логика взаимодействия для EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        private Client client;
        private Repository repository;
        private CreateCard createCard;
        private Client selectedClient;
        private Repository repository1;

        public EditAccount(Client client, Repository repository)
        {
            InitializeComponent();
            this.client = client;

            tb_LastName.Text = client.LastName;
            tb_FirstName.Text = client.FirstName;
            tb_Patronymic.Text = client.Patronymic;
            this.client = client;
            this.repository = repository;
            DataContext = client;
        }


        private void UpdateClientData(Client updatedClient)
        {
            client = updatedClient;
            DataContext = client;
            lv_ClientInfo.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (client.FirstName != tb_FirstName.Text || client.LastName != tb_LastName.Text || client.Patronymic != tb_Patronymic.Text)
            {
                string tempFIO = $"{client.LastName} {client.FirstName} {client.Patronymic}";
                client.FirstName = tb_FirstName.Text;
                client.LastName = tb_LastName.Text;
                client.Patronymic = tb_Patronymic.Text;
                client.ActionsLogEntry.Add(new ActionLogEntry($"Данные клиента: {tempFIO} были изменены на: {client.LastName} {client.FirstName} {client.Patronymic}."));
            }

            repository.SaveJson();
            this.Close();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            createCard = new CreateCard(client);
            createCard.CardCreated += UpdateClientData;
            createCard.Show();
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            TransferMoney transferMoney = new TransferMoney(client, repository);
            transferMoney.Show();
        }
        private void DelAccount_Click(object sender, RoutedEventArgs e)
        {
            Account acc = client.Accounts.ElementAt(lv_ClientInfo.SelectedIndex);
            if (acc == null) return;

            var money = acc.Balance;
            client.ActionsLogEntry.Add(new ActionLogEntry($"Счёт: {acc.ID} были закрыт:"));
            client.Accounts.Remove(acc);
            if(client.Accounts.Count > 0)
            {
                client.Accounts.ElementAt(0).Balance += money;
            }
        }

        private void tb_Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
