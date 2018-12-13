using LiveCharts;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MES.Presentation
{
    //TODO Ændre OEE til Optimization , og bytte Optimization til OEE
    /// <summary>
    /// Interaction logic for OEE.xaml
    /// </summary>
    public partial class Opt : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;

        private int speed;

        public SeriesCollection SeriesCollectionBeer { get; set; }


        private List<string> arrayListLabels;

        private bool closeApp;

        public Opt(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();

            arrayListLabels = new List<string>();

            SeriesCollectionBeer = new SeriesCollection

            {
                new LineSeries
                {
                    //Title = " Speed Pilsner",
                    Title = "OEE Pilsner",
                    Fill = Brushes.Chartreuse, Stroke = Brushes.Coral, PointGeometrySize = 5,
                    //Values = new ChartValues<int> { }
                    Values = new ChartValues<double> { }
                }
                ,
                new LineSeries
                {
                    //Title = "Speed Wheat",
                    Title = "OEE Wheat",
                    LabelPoint =point => "TESTING",
                    Fill = Brushes.DarkOrchid, Stroke = Brushes.LawnGreen, PointGeometrySize = 5,
                    //Values = new ChartValues<int> {  },
                    Values = new ChartValues<double> {  }


                }
                ,
                new LineSeries
                {
                    //Title = "Speed IPA",
                    Title = "OEE IPA",
                    Fill = Brushes.Aqua, Stroke = Brushes.CornflowerBlue, PointGeometrySize = 5,
                    //Values = new ChartValues<int> {  },
                    Values = new ChartValues<double> {  },
                },
                new LineSeries
                {
                    //Title = "Speed Stout",
                    Title = "OEE Stout",
                    Fill = Brushes.Chocolate, Stroke = Brushes.AntiqueWhite, PointGeometrySize = 5,
                    //Values = new ChartValues<int> {  },
                    Values = new ChartValues<double> {  },
                },
                new LineSeries
                {
                    //Title = "Speed Ale",
                    Title = "OEE Ale",
                    Fill = Brushes.CadetBlue, Stroke = Brushes.BlueViolet, PointGeometrySize = 5,
                    //Values = new ChartValues<int> {  },
                    Values = new ChartValues<double> {  },
                },
                new LineSeries
                {
                    //Title = "Speed Alcohol Free",
                    Title = "OEE Alcohol Free",
                    Fill = Brushes.Gold, Stroke = Brushes.DarkGoldenrod, PointGeometrySize = 5,
                    //Values = new ChartValues<int> {  },
                    Values = new ChartValues<double> {  },
                }
            };

            DataContext = this;

            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            mw.Show();
        }

        private void Pilsner_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;
            SeriesCollectionBeer[0].Values.Clear();
            SeriesCollectionBeer[1].Values.Clear();
            SeriesCollectionBeer[2].Values.Clear();
            SeriesCollectionBeer[3].Values.Clear();
            SeriesCollectionBeer[4].Values.Clear();
            SeriesCollectionBeer[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 60;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.09);
            //arrayListLabels.Add(0.125.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 120;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.191);
            //arrayListLabels.Add(0.125.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 180;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.276);
            //arrayListLabels.Add(0.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 180;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.364);
            //arrayListLabels.Add(1.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 300;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.45);
            //arrayListLabels.Add(4.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 360;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.5);
            //arrayListLabels.Add(5.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 420;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.541);
            //arrayListLabels.Add(13.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 480;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.56);
            //arrayListLabels.Add(21.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 540;
            //SeriesCollectionBeer[0].Values.Add(Speed);
            SeriesCollectionBeer[0].Values.Add(0.478);
            //arrayListLabels.Add(38.875.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 600;
            SeriesCollectionBeer[0].Values.Add(0.28);
            //SeriesCollectionBeer[0].Values.Add(Speed);
            //arrayListLabels.Add(64.5.ToString());
            arrayListLabels.Add(Speed.ToString());
        }

        private void Wheat_Checked(object sender, RoutedEventArgs e)
        {
            PilserCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;
            SeriesCollectionBeer[0].Values.Clear();
            SeriesCollectionBeer[1].Values.Clear();
            SeriesCollectionBeer[2].Values.Clear();
            SeriesCollectionBeer[3].Values.Clear();
            SeriesCollectionBeer[4].Values.Clear();
            SeriesCollectionBeer[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 20;
            SeriesCollectionBeer[1].Values.Add(0.06);
            //SeriesCollectionBeer[1].Values.Add(Speed);
            //arrayListLabels.Add(6.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 40;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.11);
            //arrayListLabels.Add(10.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 60;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.152);
            //arrayListLabels.Add(18.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 80;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.206);
            //arrayListLabels.Add(23.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 100;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.206);
            //arrayListLabels.Add(33.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 120;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.215);
            //arrayListLabels.Add(49.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 140;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.235);
            //arrayListLabels.Add(65.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 160;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.26);
            //arrayListLabels.Add(72.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 180;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.222);
            //arrayListLabels.Add(90.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 200;
            //SeriesCollectionBeer[1].Values.Add(Speed);
            SeriesCollectionBeer[1].Values.Add(0.203);
            //arrayListLabels.Add(100.ToString());
            arrayListLabels.Add(Speed.ToString());
        }

        public int Speed
        {
            get => speed;
            set => speed = value;
        }

        public List<string> ArrayListLabels
        {
            get => arrayListLabels;
            set => arrayListLabels = value;
        }

        private void IPA_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;

            SeriesCollectionBeer[0].Values.Clear();
            SeriesCollectionBeer[1].Values.Clear();
            SeriesCollectionBeer[2].Values.Clear();
            SeriesCollectionBeer[3].Values.Clear();
            SeriesCollectionBeer[4].Values.Clear();
            SeriesCollectionBeer[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 15;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.1);
            //arrayListLabels.Add(6.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 30;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.197);
            //arrayListLabels.Add(10.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 45;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.286);
            //arrayListLabels.Add(18.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 60;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.39);
            //arrayListLabels.Add(23.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 75;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.48);
            //arrayListLabels.Add(33.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 90;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.504);
            //arrayListLabels.Add(49.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 105;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.513);
            //arrayListLabels.Add(65.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 120;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.446);
            //arrayListLabels.Add(72.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 135;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.105);
            //arrayListLabels.Add(90.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 150;
            //SeriesCollectionBeer[2].Values.Add(Speed);
            SeriesCollectionBeer[2].Values.Add(0.05);
            //arrayListLabels.Add(100.ToString());
            arrayListLabels.Add(Speed.ToString());
        }

        private void Stout_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;

            SeriesCollectionBeer[0].Values.Clear();
            SeriesCollectionBeer[1].Values.Clear();
            SeriesCollectionBeer[2].Values.Clear();
            SeriesCollectionBeer[3].Values.Clear();
            SeriesCollectionBeer[4].Values.Clear();
            SeriesCollectionBeer[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(6.5);
            //arrayListLabels.Add(6.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 60;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(10.25);
            //arrayListLabels.Add(10.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 90;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(18.75);
            //arrayListLabels.Add(18.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 120;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(23.25);
            //arrayListLabels.Add(23.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 150;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(33.5);
            //arrayListLabels.Add(33.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 180;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(49.5);
            //arrayListLabels.Add(49.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 210;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(65.75);
            //arrayListLabels.Add(65.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 240;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(72.75);
            //arrayListLabels.Add(72.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 270;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(90.25);
            //arrayListLabels.Add(90.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 300;
            //SeriesCollectionBeer[3].Values.Add(Speed);
            SeriesCollectionBeer[3].Values.Add(100.0);
            //arrayListLabels.Add(100.ToString());
            arrayListLabels.Add(Speed.ToString());
        }

        private void Ale_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;

            SeriesCollectionBeer[0].Values.Clear();
            SeriesCollectionBeer[1].Values.Clear();
            SeriesCollectionBeer[2].Values.Clear();
            SeriesCollectionBeer[3].Values.Clear();
            SeriesCollectionBeer[4].Values.Clear();
            SeriesCollectionBeer[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(6.5);
            //arrayListLabels.Add(6.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 60;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(10.25);
            //arrayListLabels.Add(10.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 90;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(18.75);
            //arrayListLabels.Add(18.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 120;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(23.25);
            //arrayListLabels.Add(23.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 150;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(33.5);
            //arrayListLabels.Add(33.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 180;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(49.5);
            //arrayListLabels.Add(49.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 210;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(65.75);
            //arrayListLabels.Add(65.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 240;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(72.75);
            //arrayListLabels.Add(72.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 270;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(90.25);
            //arrayListLabels.Add(90.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 300;
            //SeriesCollectionBeer[4].Values.Add(Speed);
            SeriesCollectionBeer[4].Values.Add(100.0);
            //arrayListLabels.Add(100.ToString());
            arrayListLabels.Add(Speed.ToString());
        }

        private void AlcoholFree_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;

            SeriesCollectionBeer[0].Values.Clear();
            SeriesCollectionBeer[1].Values.Clear();
            SeriesCollectionBeer[2].Values.Clear();
            SeriesCollectionBeer[3].Values.Clear();
            SeriesCollectionBeer[4].Values.Clear();
            SeriesCollectionBeer[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(6.5);
            //arrayListLabels.Add(6.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 60;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(10.25);
            //arrayListLabels.Add(10.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 90;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(18.75);
            //arrayListLabels.Add(18.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 120;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(23.25);
            //arrayListLabels.Add(23.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 150;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(33.5);
            //arrayListLabels.Add(33.5.ToString());
            arrayListLabels.Add(Speed.ToString());


            Speed = 180;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(49.5);
            //arrayListLabels.Add(49.5.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 210;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(65.75);
            //arrayListLabels.Add(65.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 240;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(72.75);
            //arrayListLabels.Add(72.75.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 270;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(90.25);
            //arrayListLabels.Add(90.25.ToString());
            arrayListLabels.Add(Speed.ToString());

            Speed = 300;
            //SeriesCollectionBeer[5].Values.Add(Speed);
            SeriesCollectionBeer[5].Values.Add(100.0);
            //arrayListLabels.Add(100.ToString());
            arrayListLabels.Add(Speed.ToString());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}