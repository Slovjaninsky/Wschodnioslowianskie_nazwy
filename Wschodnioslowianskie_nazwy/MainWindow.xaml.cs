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
        #region informationText
        private string informationText = "~~~~POLSKI~~~~\n" +
            "\nTa aplikcja została stworzona dla sprawdzenia transkrypcji ukraińskich, białoruskich oraz rosyjskich nazw własnych na polską pisownię.\n" +
            "\nNIE JESTEM autorem zasad transkrypcji. Wszystkie zasady dotyczące transkrypcji oraz polszczenia nazw własnych znajsziecie na stronie PWN - Słownik Języka Polskiego:\n" +
            "\nUkraiński - https://sjp.pwn.pl/zasady/Transliteracja-i-transkrypcja-wspolczesnego-alfabetu-ukrainskiego;629710.html \n" +
            "\nBiałoruski - https://sjp.pwn.pl/zasady/Transliteracja-i-transkrypcja-wspolczesnego-alfabetu-bialoruskiego;629719.html \n" +
            "\nRosyjski - https://sjp.pwn.pl/zasady/Transliteracja-i-transkrypcja-wspolczesnego-alfabetu-rosyjskiego;629695.html \n" +
            "\nJak to działa?\n" +
            "\nStarałem się zrobić najbardziej intuicyjny GUI, ale na wszelki przypadek zostawię to tutaj.\n" +
            "\n\"Tu ma być twój tekst\" - wpisujecie tekst do transkrypcji (na przykład, \"Тарас Шевченко\").\n" +
            "\nDalej jest do wyboru język, z którego robi się transkrypcja (to jest istotne dla uzyskania prawidłowej transkrypcji) oraz istnieje możliwość polszczenia końcówek.\n" +
            "\nPrzycisk \"Transkrybuj\" robi transkrypcję wpisanego do głownego pola tekstu.\n" +
            "\nWynik pojawi się w dolnej sekcji okna (w wyżej wymienionym przykładzie - \"Taras Szewczenko\").\n" +
            "\nJak się znajdzie jakiś błąd lub nieprawidłowa transkrypcja - proszę poinfromować mnie wraz z szczegółami.\n" +
            "\n~~~РУССКИЙ~~~\n" +
            "\nЭто приложение было создано для проверки транскрипции украинских, белорусских и русских имен собственных на польское написание.\n" +
            "\nЯ НЕ ЯВЛЯЮСЬ автором правил транскрипции. Все правила транскрипции и полонизации имен собственных можно найти на сайте PWN - Словарь польского языка:\n" +
            "\nУкраинский - https://sjp.pwn.pl/zasady/Transliteracja-i-transkrypcja-wspolczesnego-alfabetu-ukrainskiego;629710.html \n" +
            "\nБелорусский - https://sjp.pwn.pl/zasady/Transliteracja-i-transkrypcja-wspolczesnego-alfabetu-bialoruskiego;629719.html \n" +
            "\nРусский - https://sjp.pwn.pl/zasady/Transliteracja-i-transkrypcja-wspolczesnego-alfabetu-rosyjskiego;629695.html \n" +
            "\nКак это работает?\n" +
            "\nЯ старался сделать максимально интуитивно понятный GUI, но в любом случае оставлю это здесь.\n" +
            "\n\"Tu ma być twój tekst\" - введите текст для транскрипции (например, \"Тарас Шевченко\").\n" +
            "\nДалее вы можете выбрать язык, с которого вы транскрибируете (это важно для правильной транскрипции), а также возможность полонизации окончаний.\n" +
            "\nКнопка «Transkrybuj» транскрибирует текст, введенный в основное поле.\n" +
            "\nРезультат появится в нижней части окна (в приведенном выше примере - \"Taras Szewczenko\").\n" +
            "\nЕсли вы обнаружите какую-либо ошибку или неправильную транскрипцию - пожалуйста, сообщите мне подробности.\n" +
            "\n~~~DANE KONTAKTOWE/КОНТАКТНЫЕ ДАННЫЕ~~~\n" +
            "\nt.me/slovjaninsky\nDiscord: Heavylight#7616";
        #endregion
        Translator translate;
        private bool textIsChanged = false;
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
            popupText.Text = informationText;
            popup.Content = popupText;
            popup.Show();
        }

        private void textToChange_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!textIsChanged)
            {
                textToChange.Foreground = Brushes.Black;
                textToChange.Text = "";
                textIsChanged = true;
            }
        }
    }
}
