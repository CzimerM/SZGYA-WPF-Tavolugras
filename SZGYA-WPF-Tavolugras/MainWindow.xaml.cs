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

namespace SZGYA_WPF_Tavolugras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Versenyzo> versenyzok;
        private List<Versenyzo> filteredVersenyzok;

        public MainWindow()
        {
            InitializeComponent();
            versenyzok = new List<Versenyzo>();
            var sr = new StreamReader("../../../src/tavolugras.txt", Encoding.UTF8);
            while(!sr.EndOfStream) versenyzok.Add(new Versenyzo(sr.ReadLine()));
            sr.Close();

            lstbxVersenyzok.ItemsSource = versenyzok;
            lstbxVersenyzok.SelectedIndex = 0;

            filteredVersenyzok = versenyzok.Where(v => !v.Ugrasok.Contains(-1)).ToList();
            filteredVersenyzok.Sort((a, b) => a.Ugrasok.Max().CompareTo(b.Ugrasok.Max()));
            lstbxFullosVersenyzok.ItemsSource = filteredVersenyzok;
            lblLegjobbUgrasAtlag.Content = Math.Round(filteredVersenyzok.Average(v => v.Ugrasok.Max()), 2);
        }

        private void lstbxVersenyzokSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Versenyzo)lstbxVersenyzok.SelectedItem) == null) return;
                
            int legjobb = ((Versenyzo)lstbxVersenyzok.SelectedItem).Ugrasok.Max();
            if (legjobb != -1)
                lblLegjobbEredmeny.Content = legjobb;
            else
                lblLegjobbEredmeny.Content = "Nincs";

            int belepettSzam = 0;
            foreach (int ugras in ((Versenyzo)lstbxVersenyzok.SelectedItem).Ugrasok)
            {
                if (ugras == -1) belepettSzam++;
            }
            if (belepettSzam == 2) MessageBox.Show("Nincs értelme maximumot számolni egy érvényes ugrásból!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            lblTelepules.Content = ((Versenyzo)lstbxVersenyzok.SelectedItem).Telepules;
        }

        private void btnSzuresClick(object sender, RoutedEventArgs e)
        {
            if (txbEletkor.Text == "" && txbTelepules.Text == "") return;
            List<Versenyzo> szurt = new(versenyzok);

            if (txbEletkor.Text != "")
            {
                int kor = int.Parse(txbEletkor.Text);
                szurt = szurt.Where(v => v.Kor == kor).ToList();
            }
            if (txbTelepules.Text != "")
            {
                szurt = szurt.Where(v => v.Telepules == txbTelepules.Text).ToList();
            }
            lstbxVersenyzok.ItemsSource = szurt; 
            
        }
    }
}