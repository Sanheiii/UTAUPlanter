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
using System.Xml;
using utauPlugin;

namespace Planter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UtauPlugin utauPlugin = new UtauPlugin();
        List<Button> lyricButtons=new List<Button>();
        int index=0;
        int Index
        {
            get
            {
                return index;
            }
            set
            {
                if(value<lyricButtons.Count)
                {
                    lyricButtons[Index].Background = new SolidColorBrush(Colors.Orange);
                    lyricButtons[value].Background = new SolidColorBrush(Colors.White);
                    index = value;
                }
            }
        }
        public MainWindow()
        {
            string[] args = App.args;
            if (args.Count() != 0)
            {
                utauPlugin = new UtauPlugin(args[0]);
            }
            this.Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitGallery();
            InitWorkspace();
        }

        void InitGallery()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Lyric.xml");
            XmlNodeList sentenceNodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode sentenceNode in sentenceNodes)
            {
                string background = (sentenceNode as XmlElement).GetAttribute("background");
                Color color = (Color)ColorConverter.ConvertFromString(background);
                SolidColorBrush brush = new SolidColorBrush(color);
                XmlNodeList wordNodes = sentenceNode.ChildNodes;
                WrapPanel wrapPanel = new WrapPanel() { Margin=new Thickness(0,3,0,3)};
                foreach(XmlNode wordNode in wordNodes)
                {
                    string title = (wordNode as XmlElement).GetAttribute("title");
                    string lyric = (wordNode as XmlElement).InnerText;
                    Button button=GalleryButtonFactory(title,lyric,brush);
                    wrapPanel.Children.Add(button);
                }
                gallery.Children.Add(wrapPanel);
            }
        }
        void InitWorkspace()
        {
            utauPlugin.Input();
            List<Note> notes=utauPlugin.note;
            WrapPanel wrapPanel=new WrapPanel() { Margin = new Thickness(0, 3, 0, 3) };
            workspace.Children.Add(wrapPanel);
            int i = utauPlugin.hasPrev ? 1 : 0;
            int j = utauPlugin.hasNext ? 1 : 0;
            for (;i<notes.Count-j;i++)
            {
                Note note = notes[i];
                if(note.GetLyric()=="R")
                {
                    if(wrapPanel.Children.Count != 0)
                    {
                        wrapPanel = new WrapPanel() { Margin = new Thickness(0, 3, 0, 3) };
                        workspace.Children.Add(wrapPanel);
                    }
                    continue;
                }
                Button button=LyricButtonFactory(note);
                wrapPanel.Children.Add(button);
                lyricButtons.Add(button);
            }
            Index = 0;
        }
        Button LyricButtonFactory(Note note)
        {
            Button button = new Button()
            {
                Content = note.GetLyric(),
                Tag = note,
                Background = new SolidColorBrush(Colors.Orange),
                BorderThickness = new Thickness(1, 1, 0, 0),
                MinWidth = 50
            };
            button.Click += LyricButton_Click;
            return button;
        }

        private void LyricButton_Click(object sender, RoutedEventArgs e)
        {
            Index = lyricButtons.IndexOf(sender as Button);
        }

        Button GalleryButtonFactory(string title, string lyric,SolidColorBrush brush)
        {
            Button button = new Button()
            {
                Content = title,
                Tag = lyric,
                Background = brush,
                BorderThickness = new Thickness(1,1,0,0),
                MinWidth = 40
            };
            if(lyric=="")
            {
                button.IsEnabled = false;
                button.Background = new SolidColorBrush(Colors.Black);
                button.ToolTip = "NULL";
            }
            else
            {
                button.Click += GalleryButton_Click;
                button.ToolTip = lyric;
            }
            return button;
        }

        private void GalleryButton_Click(object sender, RoutedEventArgs e)
        {
            Note note = lyricButtons[Index].Tag as Note;
            Button button = sender as Button;
            note.SetLyric(button.Tag as string);
            lyricButtons[Index].Content = note.GetLyric();
            Index++;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            utauPlugin.Output();
            Application.Current.Shutdown();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.Owner = this;
            dialog.Show();
        }
    }
}
