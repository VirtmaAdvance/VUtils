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
using VUtils.Services.Controls.DataCollection;
using VUtils.Services.FileSystem;

namespace VUtils
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window
	{

		public readonly PathCollection PathItems;


		public MainWindow()
		{
			InitializeComponent();
			PathItems=new(listView);
			PathItems.Search("D:\\_DEV_");

		}





	}
}
