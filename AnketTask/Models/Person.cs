using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnketTask.Models
{
    public class Person : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string email;
        private string phone;
        private DateTime birthday;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Surname { get => surname; set { surname = value; OnPropertyChanged(); } }
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }
        public DateTime Birthday { get => birthday; set { birthday = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Person() { }

        public Person(string name, string surname, string email, string phone, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}