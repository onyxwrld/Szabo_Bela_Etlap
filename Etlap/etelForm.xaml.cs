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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for etelForm.xaml
    /// </summary>
    public partial class etelForm : Window
    {
		private etlapService etlapService = new etlapService();
		private etel etelUpdate;
		public etelForm(etlapService etS)
		{
			InitializeComponent();

		}

		private void ujHozzaadButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				etel etel2 = CreateData();
				if (etlapService.Create(etel2))
				{
					MessageBox.Show("Sikeres hozzáadás");
					newEtelNev.Text = "";
					newEtelAr.Text = "";
					newEtelKategoria.Text = "";
					newEtelLeiras.Text = "";
				}
				else
				{
					MessageBox.Show("Hiba történt a hozzáadás során");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private etel CreateData()
		{
			string nev = newEtelNev.Text.Trim();
			string leiras = newEtelLeiras.Text.Trim();
			string kategoria = newEtelKategoria.Text.Trim();
			string ar = newEtelAr.Text.Trim();
			if (string.IsNullOrEmpty(nev))
			{
				throw new Exception("Név megadása kötelező");
			}
			if (string.IsNullOrEmpty(leiras))
			{
				throw new Exception("Leirás megadása kötelező");
			}

			if (string.IsNullOrEmpty(kategoria))
			{
				throw new Exception("Kor megadása kötelező");
			}
			if (!int.TryParse(ar, out int arr))
			{
				throw new Exception("Ár csak szám lehet");
			}

			etel etel1 = new etel();
			etel1.nev = nev;
			etel1.leiras = leiras;
			etel1.kategoria = kategoria;
			etel1.ar = arr;
			return etel1;
		}
	}
}
