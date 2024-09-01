// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Homework_12
{
    public class Repository : INotifyPropertyChanged
    {
        private List<Client> clients;
        public List<Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        public event EventHandler<ActionLogEntry> ActionLogged;
        public event PropertyChangedEventHandler PropertyChanged;
        public Repository()
        {
            //Clients = new List<Client>();
            clients = GetAllClients<List<Client>>();
        }

        /// <summary>
        /// Парсинг файла (база данных)
        /// </summary>
        /// <returns>Массив сотрудников</returns>
        public T GetAllClients<T>()
        {
            return JsonManager.LoadFromJson<T>();
        }

        /// <summary>
        /// Обновление списка клиентов из файла
        /// </summary>
        public void Update()
        {
            clients = JsonManager.LoadFromJson<List<Client>>();
        }

        public void AddClient(Client data) 
        {
            data.ActionsLogEntry.Add(new ActionLogEntry("Добавление клиента в базу"));
            Clients.Add(data);            
            SaveJson();
            OnPropertyChanged(nameof(Clients));
        }

        public bool AddClient(string str)
        {
            Client client = new Client();
            string[] splitedStr = str.Split(' ');
            if (splitedStr.Length != 3 ) { return false; }

            client.LastName = splitedStr[0];
            client.FirstName = splitedStr[1];
            client.Patronymic = splitedStr[2];
            AddClient(client);
            return true;
        }

        /// <summary>
        /// Удаление клиента из списка по индексу
        /// </summary>
        /// <param name="index">Индекс клиента для удаления</param>
        public void RemoveClientAt(int index)
        {
            if (index >= 0 && index < clients.Count)
            {
                clients.RemoveAt(index);
                SaveJson();
            }
            else
            {
                Console.WriteLine("Index is out of range.");
            }
        }
        public void RemoveClientItems(List<Client> items)
        {
            foreach (Client item in items)
            {
                if (item == null) continue;
                clients.Remove(item);
            }
            SaveJson();
        }

        public void SaveJson()
        {
            JsonManager.SaveToJson(clients);
            Update();
            OnPropertyChanged(nameof(Clients));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
