using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zametochki
{
    /// <summary>
    /// Логика взаимодействия для OpenIsfWindow.xaml
    /// </summary>
    public partial class OpenIsfWindow : Page
    {
        public OpenIsfWindow()
        {
            InitializeComponent();
        }
        public OpenIsfWindow(Note note)
        {
            InitializeComponent();
            FileStream fs = new FileStream(note.Path, FileMode.Open);
            ink.Strokes = new StrokeCollection(fs);
            tb_title.Text = note.Name;
            fs.Close();
        }

        public bool DialogResult { get; private set; }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string title = tb_title.Text;
            if (title == "" || title == " ")
            {
               System.Windows.MessageBox.Show("error", "заголовок не должен быть пустым");
                return;
            }

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string relPath = @"..\data"; // Относительный путь к файлу
            string resPath = System.IO.Path.Combine(exeDir, relPath); // Объединяет две строки в путь.
            string currentDirectory = System.IO.Path.GetFullPath(resPath); // Возвращает для указанной строки пути абсолютный путь.

            string path = $"{currentDirectory}\\{title}.isf";
            if (!File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                ink.Strokes.Save(fs);
                fs.Close();
                
            }
            else
            {
                if (System.Windows.MessageBox.Show("предупреждение", "заметка уже существует, перезпаписать?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    FileStream fs = new FileStream(path, FileMode.Create);
                    ink.Strokes.Save(fs);
                    fs.Close();
                }
            }
        }

        
    }
}
