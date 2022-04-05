using System.Collections.Generic;
using System.Windows;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for FarmGame.xaml
    /// </summary>
    public partial class FarmGame : Window
    {
        List<Name> allStrains = new List<Name>();
        public FarmGame()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbSelectStrain.Items.Add("--Select strain--");
            allStrains = DataManager.GetStrainNames();

            foreach (var item in allStrains)
            {
                cmbSelectStrain.Items.Add(item);
            }

        }

        private void btnAddWeedPlant_Click(object sender, RoutedEventArgs e)
        {
            Cultivator newPlant = new Cultivator()
            {

            };
        }
    }
}
