using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pálma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Dessert> desserts { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            desserts = new List<Dessert>();

            string palmaSrc = @"..\..\..\src\palma.txt";

            foreach (var item in File.ReadAllLines(palmaSrc))
            {
                var x = item.Split(";");
                desserts.Add(new(x[0], x[1], bool.Parse(x[2]), int.Parse(x[3]), x[4]));
            }

            Random r = new Random();

            var randomDessert = desserts[r.Next(desserts.Count)];

            textboxRandomDessert.Text = $"Mai ajánlatunk: {randomDessert.DessertName}";

            var mostExpensiveDessertObj = desserts.MaxBy(e => e.DessertPrice);
            textboxMostExpensiveDessertName.Text = mostExpensiveDessertObj.DessertName;
            textboxMostExpensiveDessertPriceAndUnit.Text = $"{mostExpensiveDessertObj.DessertPrice} Ft/{mostExpensiveDessertObj.DessertUnit}";

            var leastExpensiveDessertObj = desserts.MinBy(e => e.DessertPrice);
            textboxLeastExpensiveDessertName.Text = leastExpensiveDessertObj.DessertName;
            textboxLeastExpensiveDessertPriceAndUnit.Text = $"{leastExpensiveDessertObj.DessertPrice} Ft/{leastExpensiveDessertObj.DessertUnit}";

            textboxHowManyAwardWinnerDesserts.Text = $"{desserts.Count(e => e.HasDessertBeenAwarded)} féle díjnyertes édességből választhat.";

            var distinctDessertsList = desserts.GroupBy(e => e.DessertName).Select(d => d.First()).ToList();

            using StreamWriter swLista = new(@"..\..\..\src\lista.txt", false);
            foreach (var dessert in distinctDessertsList)
            {
                swLista.WriteLine($"{dessert.DessertName} - {dessert.DessertType}");
            }

            var distinctDessertTypesList = desserts.GroupBy(e => e.DessertType);

            using StreamWriter swStat = new(@"..\..\..\src\stat.txt", false);
            foreach (var dessert in distinctDessertTypesList)
            {
                swStat.WriteLine($"{dessert.First().DessertType} {dessert.Count()}");
            }
        }

        private void btnBidSave_Click(object sender, RoutedEventArgs e)
        {

            if (textboxSearchedDessertUserInput.Text != null &&
                desserts.Select(e => e.DessertType).Contains(textboxSearchedDessertUserInput.Text)
                )
            {
                using StreamWriter swAjanlat = new(@"..\..\..\src\ajanlat.txt", false);

                var dessertsMatchingWithUserInput = desserts.Where(e => e.DessertType == textboxSearchedDessertUserInput.Text);

                foreach (var item in dessertsMatchingWithUserInput)
                {
                    swAjanlat.WriteLine($"{item.DessertName} {item.DessertPrice} {item.DessertUnit}");
                }

                MessageBox.Show($"Sikeres volt a fájlbeírás. {dessertsMatchingWithUserInput.Count()} db desszert íródott be a tartományba, melyeknek átlaga {dessertsMatchingWithUserInput.Average(e => e.DessertPrice)} Ft.");

            }
            else
            {
                MessageBox.Show("Nincs megfelelő sütink. Kérjük, válasszon mást!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var inputtedName = textboxNewDessertFromUserInputName.Text;
            var inputtedType = textboxNewDessertFromUserInputType.Text;
            var inputtedUnit = textboxNewDessertFromUserInputUnit.Text;
            var inputtedPrice = textboxNewDessertFromUserInputPrice.Text;

            if (inputtedName.Length == 0 || inputtedType.Length == 0 || inputtedUnit.Length == 0 || !int.TryParse(inputtedPrice, out var inputtedPriceInt))
            {
                MessageBox.Show("Valamelyik adat hiányzik vagy nem helyes formátumú!", "Hiba");
            }
            else
            {
                using StreamWriter swCuki = new(@"..\..\..\src\cuki.txt", true);

                var isCheckboxChecked = checkboxNewDessertFromUserInputHasBeenAwarded.IsChecked == true;

                swCuki.WriteLine($"{inputtedName};{inputtedType};{isCheckboxChecked};{inputtedPriceInt};{inputtedUnit}");
                desserts.Add(new(inputtedName, inputtedType, isCheckboxChecked, inputtedPriceInt, inputtedUnit));

                MessageBox.Show("A desszert hozzáadásra került.");
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}