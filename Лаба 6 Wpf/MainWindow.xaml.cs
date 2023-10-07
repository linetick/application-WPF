using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Threading;

namespace Лаба_6_Wpf
{
    
    
    public class Item 
    {
     
        public string Discipline { get; set; }
        public Brush Color { get; set; }
        public bool Flag { get; set; }

      
    }
    public class Combo
    {
        public string Name { get; set; } = "";
        public bool Flag { get; set; }
        public override string ToString() => $"{Name}";
        
            
        
    }
   
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Combik.ItemsSource = new Combo[]
            {
                new Combo{Name = "false", Flag = true},
                new Combo{Name = "true", Flag = false}
            };
            Combik.SelectedIndex = 0;
            listbox1.ItemsSource = items;
            string file = @"D:\\Prog\\DOC.txt";
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] elementsT = line.Split(',');
                    string key = elementsT[0];
                    bool resTrue = bool.Parse(elementsT[1]);
                    string[] elementsF = line.Split(',');
                    bool resFalse = bool.Parse(elementsF[1]);
                    string key1 = elementsF[0];
                    if (resTrue == true && elementsT != null)
                    for(int i = 0; i < elementsT.Length; i++)
                    {
                            string element = elementsT[i]; 

                            items.Insert(0, new Item { Discipline = key, Color = Brushes.Green, Flag = resTrue });
                            elementsT = null;
                            break;
                    }
                    if (resFalse == false && elementsF != null)
                    {
                        for(int i =0; i< elementsF.Length; i++)
                        {
                            string elementt = elementsF[i];
                            items.Insert(0, new Item { Discipline = key1, Color = Brushes.Red, Flag = resFalse });
                            elementsF = null;
                            break;
                        }
                    }   
                };
            }


        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
       public ObservableCollection<Item> items = new ObservableCollection<Item>();
       public HashSet<string> strings = new HashSet<string>();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string discipline = Discipline.Text;


            if (!strings.Contains(discipline))
            {
                

                if (Combik.SelectedValue != null && bool.TryParse(Combik.SelectedValue.ToString(), out bool flag))
                {
                    strings.Add(discipline);
                    if (flag == true)
                    {
                        items.Insert(0, new Item { Discipline = discipline, Color = Brushes.Green, Flag = flag });
                    }
                    else
                    {
                        items.Insert(0, new Item { Discipline = discipline, Color = Brushes.Red, Flag = flag });

                    }
                }
                else
                {

                }   
            }
            else
            {
                MessageBox.Show("Данная дисциплина уже существует");
            }
            
           
        }
        private void Combik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            object g = listbox1.SelectedValue;

            if (listbox1.SelectedItem != null)
            {

                if (listbox1.SelectedItem != null && listbox1.SelectedValue is Item)
                {
                    Item item1 = (Item)g;
                    items.Remove(item1);
                    listbox1.Items.Refresh();
                    if (RadioButton2.IsChecked == true)
                    {

                        var sortedFalse = items.OrderBy(x => x.Flag);
                        bool filterValue = false;
                        var filteredItems = sortedFalse.Where(item => item.Flag == filterValue);
                        listbox1.ItemsSource = filteredItems;
                    }
                    if (RadioButton3.IsChecked == true)
                    {
                        var sortedTrue = items.OrderBy(x => x.Flag);
                        bool filterValue = true;
                        var filteredItems = sortedTrue.Where(item => item.Flag == filterValue);
                        listbox1.ItemsSource = filteredItems;

                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Изменить статус                           
               object selectedItem = listbox1.SelectedValue;

            
                if (listbox1.SelectedItem != null && listbox1.SelectedValue is Item)
                {
                    Item item1 = (Item)selectedItem;

                    if (item1.Flag == true)
                    {

                        item1.Flag = false;
                        item1.Color = Brushes.Red;
                        listbox1.Items.Refresh();
                    }
                    else
                    {
                        item1.Flag = true;
                        item1.Color = Brushes.Green;
                        listbox1.Items.Refresh();
                    }


                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строчку которую вы хотите изменить");
                }
            
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Отобразить
            if (RadioButton1.IsChecked == true || RadioButton2.IsChecked == true || RadioButton3.IsChecked == true )
            {
                if (RadioButton1.IsChecked == true)
                {
                    listbox1.ItemsSource = items;
                }
                if (RadioButton2.IsChecked == true)
                {

                    var sortedFalse = items.OrderBy(x => x.Flag);
                    bool filterValue = false;
                    var filteredItems = sortedFalse.Where(item=> item.Flag == filterValue);
                    listbox1.ItemsSource = filteredItems;
                }
                if (RadioButton3.IsChecked == true)
                {
                    var sortedTrue = items.OrderBy(x => x.Flag);
                    bool filterValue = true;
                    var filteredItems = sortedTrue.Where(item=> item.Flag == filterValue);
                    listbox1.ItemsSource = filteredItems;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пункт.");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SaveObservableCollectionToFile();
        }
        private void SaveObservableCollectionToFile()
        {
            string file = @"D:\\Prog\\DOC.txt";
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach(var item in items)
                {
                    writer.WriteLine($"{item.Discipline}, {item.Flag}");  
                }
                
            }
        }
    }
}
