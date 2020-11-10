using System;
using System.Media;
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
using System.Windows.Threading;

namespace RunningHorses
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int imgIndexFirstHorse;
        int imgIndexSecondHorse;
        int indexImage;
        int firstHorseX = 30;
        int secondHorseX = 30;
        int firstHorseCurrentSpeed = 0;
        int secondHorseCurrentSpeed = 0;
        int countOfTimerTick = 0;
        Image[] firstHorseImg = new Image[4] { new Image(), new Image(), new Image(), new Image() };
        Image[] secondHorseImg = new Image[4] { new Image(), new Image(), new Image(), new Image() };
        Image[] flagImg = new Image[3] { new Image(), new Image(), new Image() };
        DispatcherTimer dt;
        MediaPlayer player1;
        MediaPlayer player2;
        bool clickStart = false;
        bool buttonPauseOn = false;

        public MainWindow()
        {
            InitializeComponent();
            HorseImages();
        }

        private void HorseImages()
        {
            if (firstHorseImg[0].Source != null)
                return;
            for (int i = 0; i < 4; i++)
            {
                firstHorseImg[i].Source = new BitmapImage(new Uri($@"..\..\Assets\horse_run{i}.png", UriKind.Relative));
                secondHorseImg[i].Source = new BitmapImage(new Uri($@"..\..\Assets\horse_run{i}.png", UriKind.Relative));
            }
            for (int i = 0; i < 3; i++)
            {
                flagImg[i].Source = new BitmapImage(new Uri($@"..\..\Assets\flag{i}.png", UriKind.Relative));
            }
            imgIndexFirstHorse = 0;
            imgIndexSecondHorse = 0;
            indexImage = 0;
        }

        private void horseOne_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && clickStart == true)
            {
                firstHorseCurrentSpeed = (firstHorseX - 30) / countOfTimerTick;
                InfoTextBlock.Text = $"{firstHorseCurrentSpeed} пикселов в 150 милисекунд";
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                InfoTextBlock.Text = $"{720 - firstHorseX} пикселов до шиниша!";
            }
        }

        private void horseTwo_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && clickStart == true)
            {
                secondHorseCurrentSpeed = (secondHorseX - 30) / countOfTimerTick;
                InfoTextBlock.Text = $"{secondHorseCurrentSpeed} пикселов в 150 милисекунд";
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                InfoTextBlock.Text = $"{720 - secondHorseX} пикселов до шиниша!";
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (clickStart == false)
            {
                dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromMilliseconds(150);
                dt.Tick += timer_Tick;
                dt.Start();
                clickStart = true;
                buttonPauseOn = false;
                player1 = new MediaPlayer();
                player2 = new MediaPlayer();
                player1.Open(new Uri(@"..\..\Sounds\loshadinyj-galop.mp3", UriKind.Relative));
                player2.Open(new Uri(@"..\..\Sounds\zvuk-skachki-galopom.mp3", UriKind.Relative));
                player1.Play();
                player2.Play();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            countOfTimerTick++;
            if (firstHorseX >= 720 || secondHorseX >= 720)
            {
                dt.Tick -= timer_Tick;
                player1.Stop();
                player2.Stop();
                if (firstHorseX > secondHorseX)
                {
                    MessageBox.Show("Первая лошадь победила!");
                }
                else if (firstHorseX < secondHorseX)
                {
                    MessageBox.Show("Вторая лошадь победила!");
                }
                else MessageBox.Show("Победила дружба!");
            }

            imageFirstHorse.ImageSource = firstHorseImg[imgIndexFirstHorse++].Source;
            Canvas.SetLeft(horseOne, firstHorseX += new Random(DateTime.Now.Second).Next(1, 10));
            imageSecondHorse.ImageSource = secondHorseImg[imgIndexSecondHorse++].Source;
            Canvas.SetLeft(horseTwo, secondHorseX += new Random(DateTime.Now.Millisecond).Next(1, 10));
            flagImage.Source = flagImg[indexImage++].Source;
            if (imgIndexFirstHorse == 4 | imgIndexSecondHorse == 4)
            {
                imgIndexFirstHorse = 0;
                imgIndexSecondHorse = 0;
            }
            if (indexImage == 3)
                indexImage = 0;
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            if (clickStart == true || buttonPauseOn == true)
            {
                dt.Tick -= timer_Tick;
                dt = null;
                firstHorseX = 30;
                secondHorseX = 30;
                countOfTimerTick = 0;
                Canvas.SetLeft(horseOne, firstHorseX);
                Canvas.SetLeft(horseTwo, secondHorseX);
                clickStart = false;
                player1.Stop();
                player2.Stop();
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (clickStart == true)
            {
                dt.Tick -= timer_Tick;
                buttonPauseOn = true;
                clickStart = false;
                player1.Stop();
                player2.Stop();
            }
        }
    }
}