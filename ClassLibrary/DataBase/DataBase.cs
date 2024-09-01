using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Homework_12
{
    public class Client : INotifyPropertyChanged
    {

        private string lastName;
        public  string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        
        private string firstName;
        public  string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        
        private string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                if (patronymic != value)
                {
                    patronymic = value;
                    OnPropertyChanged(nameof(Patronymic));
                }
            }
        }
        
        private decimal commonBalanc;
        public decimal CommonBalanc
        {
            get { return Accounts.Sum(account => account.Balance); }
            //get { return commonBalanc; }
            //set
            //{
            //    if (commonBalanc != value)
            //    {
            //        commonBalanc = value;
            //        OnPropertyChanged(nameof(CommonBalanc));
            //    }
            //}
        }

        public ObservableCollection<Account> Accounts { get; set; }

        public List<ActionLogEntry> ActionsLogEntry { get; set; }

        public Client()
        {
            Accounts = new ObservableCollection<Account>();
            if (Accounts == null) commonBalanc = 0;
            else
            {
                foreach (var account in Accounts)
                {
                    commonBalanc += account.Balance;
                }
            }

            ActionsLogEntry = new List<ActionLogEntry>();
        }



        public Account GetAccount(string ID)
        {
            foreach(var account in Accounts)
            {
                if (account.ID == ID) return account;
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Account
    {
        public List<string> CardTypes { get; set; } = new List<string> { "Депозитный", "Недепозитный", "Кредитный" };
        public enum AccountType
        {
            Deposit,
            NonDeposit,
            Credit
        }
        public string ID { get; set; } = UniqueIdGenerator.GenerateDigitUniqueId();
        public decimal Balance { get; set; } = 0;
        public AccountType Type { get; set; } = AccountType.Credit;

        public int IndexType
        {
            get { return (int)Type; }
            set { }
        }

        public string TypeName
        {
            get { return CardTypes.ElementAt(IndexType); }
            set { }
        }
    }

    public class UniqueIdGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerateDigitUniqueId()
        {
            int randomNumber = random.Next(100000000, 999999999);
            string uniqueId = randomNumber.ToString() + random.Next(1000000, 9999999).ToString();
            return uniqueId;
        }
    }
}
