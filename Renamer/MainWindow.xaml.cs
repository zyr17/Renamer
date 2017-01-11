using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Renamer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Choose_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = spanel.Children.Count - 1; i >= 0; i--)
                {
                    if (spanel.Children[i] is filedetail) spanel.Children.RemoveAt(i);
                }
                foreach (string s in dlg.FileNames)
                {
                    filedetail fd = new filedetail();
                    var regexans = Regex.Match(s, @"^.*\\(?=[^\\]*$)");
                    if (!regexans.Success)
                    {
                        System.Windows.Forms.MessageBox.Show("正则匹配出错！路径字符串： \"" + s + "\"");
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("\"" + regexans.Value + "\"");
                        fd.Filelocation = regexans.Value;
                    }
                    var regexans2 = Regex.Match(s, @"(?<=^.*\\)[^\\]*$");
                    if (!regexans2.Success)
                    {
                        System.Windows.Forms.MessageBox.Show("正则匹配出错！文件字符串： \"" + s + "\"");
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("\"" + regexans2.Value + "\"");
                        fd.Nowfilename = regexans2.Value;
                    }
                    fd.Status = null;
                    fd.Toname = "";
                    spanel.Children.Add(fd);
                }
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(inputex.Text);
                for (int i = 1; i < spanel.Children.Count; i++)
                {
                    if (spanel.Children[i] is filedetail)
                    {
                        ((filedetail)spanel.Children[i]).Toname = regex.Replace(((filedetail)spanel.Children[i]).Nowfilename, change.Text, 1);
                    }
                }
            }
            catch
            {

            }
        }

        private void Change_Name(object sender, RoutedEventArgs e)
        {
            try{
                for (int i = 1; i < spanel.Children.Count; i++)
                {
                    if (spanel.Children[i] is filedetail)
                    {
                        string location = ((filedetail)spanel.Children[i]).Filelocation;
                        string source = ((filedetail)spanel.Children[i]).Nowfilename;
                        string dest = ((filedetail)spanel.Children[i]).Toname;
                        string suffix = Regex.Match(dest, @"\..*$").Value;
                        string dest_pre = Regex.Match(dest, @"^.*(?=\..*$)").Value;
                        string truedest = "";
                        for (int j = 0; ; j++)
                        {
                            if (source == dest)
                            {
                                truedest = dest;
                                break;
                            }
                            if (j == 99999)
                            {
                                truedest = source;
                                break;
                            }
                            if (j == 0)
                            {
                                if (!File.Exists(location + dest))
                                {
                                    File.Move(location + source, location + dest);
                                    truedest = dest;
                                    break;
                                }
                            }
                            else
                            {
                                string nowdest = dest_pre + " (" + j.ToString() + ")" + suffix;
                                if (!File.Exists(location + nowdest))
                                {
                                    File.Move(location + source, location + nowdest);
                                    truedest = nowdest;
                                    break;
                                }
                            }
                        }
                        ((filedetail)spanel.Children[i]).Status = truedest != source;
                        ((filedetail)spanel.Children[i]).Toname = "";
                        ((filedetail)spanel.Children[i]).Nowfilename = truedest;
                    }
                }
            }
            catch{

            }
        }
    }
}
