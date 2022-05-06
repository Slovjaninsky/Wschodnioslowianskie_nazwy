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
using System.IO;

namespace Wschodnioslowianskie_nazwy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Translator translate;
        public MainWindow()
        {
            InitializeComponent();
            translate = new Translator(); //creating the instance of Translator class
            languageSelection.Width = (textToChange.Width - 20) / 3; //adjusting sizes of comboboxes and buttons
            polszczenieSelection.Width = (textToChange.Width - 20) / 3;
            transcrypt.Width = (textToChange.Width - 20) / 3;
        }

        //adjusting sizes of elements after resizing the window
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newTextFieldHeight = (e.NewSize.Height - languageSelection.ActualHeight - information.ActualHeight - 50) / 2;
            if (newTextFieldHeight < 0) newTextFieldHeight = 0;
            textToChange.Height = newTextFieldHeight;
            textToChange.Width = e.NewSize.Width - 20;
            languageSelection.Width = (textToChange.Width - 20) / 3;
            ukrLang.Width = (textToChange.Width - 20) / 3;
            belLang.Width = (textToChange.Width - 20) / 3;
            rusLang.Width = (textToChange.Width - 20) / 3;
            polszczenieSelection.Width = (textToChange.Width - 20) / 3;
            polFalse.Width = (textToChange.Width - 20) / 3;
            polTrue.Width = (textToChange.Width - 20) / 3;
            transcrypt.Width = (textToChange.Width - 20) / 3;
            languageSelection.Margin = new Thickness(10, textToChange.Height + 20, 0, 0);
            polszczenieSelection.Margin = new Thickness(languageSelection.Width + 20, textToChange.Height + 20, 0, 0);
            transcrypt.Margin = new Thickness(languageSelection.Width + 30 + polszczenieSelection.Width, textToChange.Height + 20, 0, 0);
            outputField.Margin = new Thickness(10, textToChange.Height + 30 + transcrypt.ActualHeight, 0, 0);
            outputField.Height = newTextFieldHeight;
            outputField.Width = e.NewSize.Width - 20;
            information.Margin = new Thickness(10, e.NewSize.Height - information.ActualHeight - 10, 0, 0);
        }
        //"Transkrybuj" button click
        private void transcrypt_Click(object sender, RoutedEventArgs e)
        {
            string text = textToChange.Text;
            if (text == null) return;
            else
            {
                switch (languageSelection.SelectedIndex)
                {
                    case 0:
                        outputField.Text = translate.translateUkrainian(text, polszczenieSelection.SelectedIndex == 1); break; //selected Ukrainian
                    case 1:
                        outputField.Text = translate.translateBelarusian(text, polszczenieSelection.SelectedIndex == 1); break; //selected Belarusian
                    case 2:
                        outputField.Text = translate.translateRussian(text, polszczenieSelection.SelectedIndex == 1); break; //selected Russian
                }
            }
        }

        private void information_Click(object sender, RoutedEventArgs e)
        {
            Window popup = new Window();
            TextBox popupText = new TextBox();
            popupText.IsReadOnly = true;
            using(StreamReader read = new StreamReader("READ_ME.txt"))
            {
                popupText.Text = read.ReadToEnd();
            }
            popup.Content = popupText;
            popup.Show();
        }
    }
}
