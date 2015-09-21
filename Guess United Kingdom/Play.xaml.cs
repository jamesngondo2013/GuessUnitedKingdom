using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Guess_United_Kingdom
{
   
    public sealed partial class Play : Page
    {
        public int scorePoints;

        public static bool _timerOn = true;
        public int userNumber = 1;
        public int bookCounter;
        int guessCount=5, newCount=2;
        public int scoreVal;

        DispatcherTimer waitTimer = new DispatcherTimer();
        DispatcherTimer guessTimer = new DispatcherTimer();
        DispatcherTimer overTimer = new DispatcherTimer();

        public Random rand = new Random();

        List<string> countyList = new List<string>{
                                "Scotland",
                                "Northen_Ireland",
                                "Wales",
                                "Cornwall",
                                "Devon",
                                "Somerset",
                                "Dorset",
                                "Wiltshire",
                                "Hampshire",
                                "Beckshire",
                                "Nofolk",
                                "Suffolk",
                                "Essex",
                                "Kent",
                                "London",
                                "Surrey",
                                "Cambridgeshire",
                                "Hertfordshire",
                                "West_Sussex",
								"East_Sussex",
                                "Lincolnshire",
                                "Northumberland",
                                "Cumbria",
                                "Cheshire",
                                "Shropshire",
                                "Herefordshire",
                                "Nottinghamshire",
                                "Derbyshire",
                                "Staffordshire",
                                "Northamptonshire",
                                "Oxfordshire",
                                "Buckinghamshire",
                                "Bedfordshire",
                                "Leicestershire",
                                "Warwickshire",
                                "Worcestershire"
                                };

        //string[] canvasArray = new string[36];

        List<string> countyRandList = new List<string>();

        Random random = new Random();

        DispatcherTimer newTimer = new DispatcherTimer();

        public Play()
        {
            this.InitializeComponent();

            myRandomBooks();
            guessTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); // 1 second
            guessTimer.Tick += Guess_Tick;
            guessTimer.Start();

            waitTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); // 1 second
            waitTimer.Tick += Wait_Tick;
            waitTimer.Start();
        }

        private void Wait_Tick(object sender, object e)
        {
            newCount--;
            if (newCount == 2)
            {
                //TimerBlock.Text = "";
                //QuestionBlock2.Text = "";
            }
            if (newCount == 0)
            {
                //bookCounter++;
                //myRandomBooks();
                //guessTimer.Start();
               // QuestionBlock2.Text = "";
                //waitTimer.Stop();
            }
        }
        private void Guess_Tick(object sender, object e)
        {
            guessCount--;
            TimerBlock.Text = guessCount.ToString();
            if (guessCount == 4)
            {

                QuestionBlock2.Text = "";
            }
            if (guessCount == 0)
            {
                bookCounter++;
               // QuestionBlock2.Text = "Slow Guess";
                ScoreBlock.Text = scoreVal.ToString();
                myRandomBooks();

            }

        }
       

        private void myRandomBooks()
        {
            if (countyList.Count == 0)
            {
                QuestionBlock2.Text = "Game Over";
                QuestionBlock.Text = "";
                TimerBlock.Text = "";
                guessTimer.Stop();
                this.Frame.Navigate(typeof(PlayQuit));
            }
            else
            {
                guessCount = 5;
                int books;
                books = rand.Next(0, countyList.Count());
                countyRandList.Add(countyList[books]);
                countyList.RemoveAt(books);

                QuestionBlock.Text = countyRandList[bookCounter];
            }
        }
        private void Path_Tapped(object sender, TappedRoutedEventArgs e)
        {

            //  Canvas tmp = BibleBooks;
            // int index = tmp.Children.Count;
            Canvas current = sender as Canvas;

            if (QuestionBlock.Text == current.Tag.ToString())
            {
                //have matched
                scoreVal++;
                bookCounter++;

                if (countyList.Count == 0)
                {
                    QuestionBlock2.Text = "Game Over";
                    QuestionBlock.Text = "";
                    TimerBlock.Text = "";
                    guessTimer.Stop();
                    this.Frame.Navigate(typeof(PlayQuit));
                }
                else
                {
                    QuestionBlock2.Text = "Correct";
                    ScoreBlock.Text = scoreVal.ToString();
                    current.Opacity = 1;
                    myRandomBooks();
                }


            }
            else
            {

                QuestionBlock2.Text = "Wrong Guess";

                guessTimer.Start();
                guessCount = 5;
                 
                if(guessCount==4)
                {
                    QuestionBlock2.Text = "";
                    guessTimer.Stop();

                }
               

                ScoreBlock.Text = scoreVal.ToString();
                ((App)(App.Current)).TotalScore = scoreVal;
            }

        }

        private void gameOver()
        {
            if (bookCounter == countyList.Count)
            {
                TimerBlock.Opacity = 0;
                QuestionBlock2.Text = "Game over...Your Score is " + scoreVal;
                QuestionBlock.Text = "";
                TimerBlock.Text = "";
                guessTimer.Stop();
                this.Frame.Navigate(typeof(PlayQuit));

            }
        }

    }
}
