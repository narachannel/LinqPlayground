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

namespace LinqPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listBox.DataContext = new List<string>() {"aaa", "bbb", "ccc"};
            listBox.SelectionMode = SelectionMode.Multiple;
            
            //var selected = listBox.SelectedItems.OfType<ListBoxItem>().Any(s => s.IsSelected);

            //GUIのパーツの集合はGenericでない場合が多い
            //下記は一例だがGenericでないコレクションに対してもOfTypeやCastなどを用いれば
            //LINQで処理可能
            button.Click += (s, e) => { MessageBox.Show(listBox.SelectedItems.OfType<ListBoxItem>().Any(a => a.IsSelected).ToString()); };
        }       
    }
}
