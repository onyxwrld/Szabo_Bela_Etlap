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

		}
	}
}
