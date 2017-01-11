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

namespace Renamer
{
    /// <summary>
    /// filedetail.xaml 的交互逻辑
    /// </summary>
    public partial class filedetail : UserControl
    {
        public filedetail()
        {
            InitializeComponent();
        }
        bool? st;
        public bool? Status
        {
            get
            {
                return st;
            }
            set
            {
                st = value;
                if (st == null)
                {
                    status.Text = "-";
                    status.Foreground = Brushes.Gray;
                }
                else if ((bool)st)
                {
                    status.Text = "成功";
                    status.Foreground = Brushes.Green;
                }
                else if (!(bool)st)
                {
                    status.Text = "失败";
                    status.Foreground = Brushes.Red;
                }
            }
        }
        public string Nowfilename
        {
            get
            {
                return nowname.Text;
            }
            set
            {
                nowname.Text = value;
            }
        }
        public string Toname
        {
            get
            {
                return toname.Text;
            }
            set
            {
                toname.Text = value;
            }
        }
        public string Filelocation;
    }
}
