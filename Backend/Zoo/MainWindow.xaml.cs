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

using Animals;

namespace Zoo
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
        
        public void UpdateFood()
        {
            HerbNeedsLabel.Content = AnimalFactory.GetInstance().CalcFoodRequirements(typeof(Herbivore));
            CarnivorNeedsLabel.Content = AnimalFactory.GetInstance().CalcFoodRequirements(typeof(Carnivore));
            
            TotalPriceLabel.Content = AnimalFactory.GetInstance().CalcTotalPrice();
        }

        private void UpdateAnimalList()
        {
            var animals = AnimalFactory.GetInstance().GetAnimalCount();
            var listItems = new string[animals.Length];
            
            for (var i = 0; i < animals.Length; i++)
            {
                listItems[i] = $"{animals[i].Item2} x {animals[i].Item1}";
            }
            
            AnimalListBox.ItemsSource = listItems;
            AnimalListBox.Items.Refresh();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            AnimalComboBox.ItemsSource = AnimalFactory.GetAnimalNames();
            UpdateFood();
            UpdateAnimalList();
        }

        private void BuyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var type = AnimalComboBox.SelectionBoxItem as string;
            if (type == null || !int.TryParse(AmountTextBox.Text, out int amount))
            {
                return;
            }

            AnimalFactory.GetInstance().Create(type, amount).Count(); // Stop compiler from pruning this pure function call :)


            UpdateAnimalList();
            UpdateFood();
        }
    }
}
