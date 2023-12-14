using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		etlapService etS = new etlapService();
		public MainWindow()
		{
			InitializeComponent();
			Read();
		}
		private void Read()
		{
			etelData.ItemsSource = etS.GetAll();
		}

		private void ujButton_Click(object sender, RoutedEventArgs e)
		{
			etelForm form = new etelForm(etS);
			form.Closed += (_, _) =>
			{
				Read();
			};
			form.ShowDialog();
		}

		private void torlesButton_Click(object sender, RoutedEventArgs e)
		{
			etel selected = etelData.SelectedItem as etel;
			if (selected == null)
			{
				MessageBox.Show("Törléshez előbb válasszon ki dolgozót!");
				return;
			}
			MessageBoxResult selectedButton =
				MessageBox.Show($"Biztos, hogy törölni szeretné az alábbi ételt: {selected.nev}?",
					"Törlés", MessageBoxButton.YesNo);
			if (selectedButton == MessageBoxResult.Yes)
			{
				if (etS.Delete(selected.id))
				{
					MessageBox.Show("Sikeres törlés!");
				}
				else
				{
					MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található");
				}
				Read();
			}
		}

		private void ftButton_Click(object sender, RoutedEventArgs e)
		{
			int ertek = Convert.ToInt32(fixFtText.Text);
			etel selected = etelData.SelectedItem as etel;
			MessageBoxResult selectedButton =
				MessageBox.Show($"Biztosan szeretné növelni a árát {ertek}-al?",
					"Emelés", MessageBoxButton.YesNo);
			if (selectedButton == MessageBoxResult.Yes)
			{
				if (selected != null)
				{
					if (etS.UpdateEgyElemFt(selected.id, selected, ertek))
					{
						MessageBox.Show("Sikeres Emelés!");
						fixFtText.Text = "";
					}
					else
					{
						MessageBox.Show("Hiba történt az emelés során, a megadott elem nem található");
						fixFtText.Text = "";
					}
					Read();
				}
				else
				{
					if (!etS.UpdateTobbElemFt(ertek))
					{
						MessageBox.Show("Sikeres Emelés!");
						fixFtText.Text = "";
					}
					else
					{
						MessageBox.Show("Hiba történt az emelés során, a megadott elem nem található");
						fixFtText.Text = "";
					}
					Read();
				}
			}
		}
		private void szazalekButton_Click(object sender, RoutedEventArgs e)
		{
			int ertek = Convert.ToInt32(szazalekText.Text);
			etel selected = etelData.SelectedItem as etel;
			MessageBoxResult selectedButton =
				MessageBox.Show($"Biztosan szeretné növelni az árát {ertek}%?",
					"Emelés", MessageBoxButton.YesNo);
			if (selectedButton == MessageBoxResult.Yes)
			{
				if(selected!=null)
				{
					if (etS.UpdateEgyElemSzazalek(selected.id, selected, ertek))
					{
						MessageBox.Show("Sikeres Emelés!");
						fixFtText.Text = "";
					}
					else
					{
						MessageBox.Show("Hiba történt az emelés során, a megadott elem nem található");
						fixFtText.Text = "";
					}
					Read();
				}
				else
				{
					if (!etS.UpdateTobbElemSzazalek(ertek))
					{
						MessageBox.Show("Sikeres Emelés!");
						fixFtText.Text = "";
					}
					else
					{
						MessageBox.Show("Hiba történt az emelés során, a megadott elem nem található");
						fixFtText.Text = "";
					}
					Read();
				}	
			}
		}

		private void etelData_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			etel selected = etelData.SelectedItem as etel;
			if (selected!=null)
			{
				etelLeiras.Content = selected.leiras;
			}
			else
			{
				etelLeiras.Content = "";
			}
		}
	}
}
