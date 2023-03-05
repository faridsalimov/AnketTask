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
using System.Text;
using System.Text.Json;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using AnketTask.Models;

namespace AnketTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<Person> Person { get; set; } = new ObservableCollection<Person>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Name section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                if (surnameTextBox.Text == "")
                {
                    MessageBox.Show("Surname section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    if (emailTextBox.Text == "")
                    {
                        MessageBox.Show("Email section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else
                    {
                        if (phoneTextBox.Text == "")
                        {
                            MessageBox.Show("Phone section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        else
                        {
                            Person person = new Person
                            {
                                Name = nameTextBox.Text,
                                Surname = surnameTextBox.Text,
                                Email = emailTextBox.Text,
                                Phone = phoneTextBox.Text,
                                Birthday = birthDayDatePicker.DisplayDate,
                            };

                            //var json = JsonSerializer.Serialize(person);
                            //File.WriteAllText($"{person.Name}{person.Surname}.json", json);

                            bool edited = false;

                            if (listBox.SelectedIndex >= 0)
                            {
                                foreach (var item in Person)
                                {
                                    if (listBox.SelectedItem.ToString() == item.Name)
                                    {
                                        edited = true;
                                        Person.Remove(item);
                                        Person.Add(person);
                                        MessageBox.Show("Person has been successfully edited to the database.", "Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                                        addButton.Content = "Add";
                                        break;
                                    }
                                }
                            }

                            if (!edited)
                            {
                                Person.Add(person);
                                MessageBox.Show("Person has been successfully added to the database.", "Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }

                            nameTextBox.Text = null;
                            surnameTextBox.Text = null;
                            emailTextBox.Text = null;
                            phoneTextBox.Text = null;
                            birthDayDatePicker.SelectedDate = null;
                        }
                    }
                }
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addButton.Content = "Edit";
            foreach (var item in Person)
            {
                if (listBox.SelectedItem.ToString() == item.Name)
                {
                    nameTextBox.Text = item.Name;
                    surnameTextBox.Text = item.Surname;
                    emailTextBox.Text = item.Email;
                    phoneTextBox.Text = item.Phone;
                    birthDayDatePicker.SelectedDate = item.Birthday.Date;
                    break;
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (loadSaveTextBox.Text == "")
            {
                MessageBox.Show("File Name section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                if (listBox.Items.Count == 0)
                {
                    MessageBox.Show("Listbox looks blank, please add a person.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    var person = Person.ToArray();
                    var json = JsonSerializer.Serialize(person);
                    File.WriteAllText($"{loadSaveTextBox.Text}.json", json);
                    loadSaveTextBox.Text = null;
                    MessageBox.Show("Your list have been successfully saved.", "Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (loadSaveTextBox.Text == "")
            {
                MessageBox.Show("File Name section looks blank.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                if (File.Exists($"{loadSaveTextBox.Text}.json"))
                {
                    var json = File.ReadAllText($"{loadSaveTextBox.Text}.json");
                    ObservableCollection<Person> person = JsonSerializer.Deserialize<ObservableCollection<Person>>(json);
                    Person = person;
                    listBox.ItemsSource = person;
                    loadSaveTextBox.Text = null;
                }

                else
                {
                    MessageBox.Show("No such file was found.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}