using LiveCharts;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for OEE.xaml
    /// </summary>
    public partial class OEE : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;

        private int speed;

        public SeriesCollection SeriesCollectionPilsner { get; set; }
        public SeriesCollection SeriesCollectionWheat { get; set; }
        public SeriesCollection SeriesCollectionIPA { get; set; }
        public SeriesCollection SeriesCollectionStout { get; set; }
        public SeriesCollection SeriesCollectionAle { get; set; }
        public SeriesCollection SeriesCollectionAlcoholFree { get; set; }

        private List<string> arrayListLabels;

        private bool closeApp;

        public OEE(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();

            arrayListLabels = new List<string>();

            SeriesCollectionPilsner = new SeriesCollection

            {
                new LineSeries
                {
                    Title = " Speed Pilsner",
                    Fill = Brushes.Chartreuse, Stroke = Brushes.Coral, PointGeometrySize = 5,
                    Values = new ChartValues<int> { }
                }
                ,
                new LineSeries
                {
                    Title = "Speed Wheat",
                    Fill = Brushes.DarkOrchid, Stroke = Brushes.LawnGreen, PointGeometrySize = 5,
                    Values = new ChartValues<int> {  },
                }
                ,
                new LineSeries
                {
                    Title = "Speed IPA",
                    Fill = Brushes.Aqua, Stroke = Brushes.CornflowerBlue, PointGeometrySize = 5,
                    Values = new ChartValues<int> {  },
                },
                new LineSeries
                {
                    Title = "Speed Stout",
                    Fill = Brushes.Chocolate, Stroke = Brushes.AntiqueWhite, PointGeometrySize = 5,
                    Values = new ChartValues<int> {  },
                },
                new LineSeries
                {
                    Title = "Speed Ale",
                    Fill = Brushes.CadetBlue, Stroke = Brushes.BlueViolet, PointGeometrySize = 5,
                    Values = new ChartValues<int> {  },
                },
                new LineSeries
                {
                    Title = "Speed Alcohol Free",
                    Fill = Brushes.Gold, Stroke = Brushes.DarkGoldenrod, PointGeometrySize = 5,
                    Values = new ChartValues<int> {  },
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
            SeriesCollectionPilsner[0].Values.Clear();
            SeriesCollectionPilsner[1].Values.Clear();
            SeriesCollectionPilsner[2].Values.Clear();
            SeriesCollectionPilsner[3].Values.Clear();
            SeriesCollectionPilsner[4].Values.Clear();
            SeriesCollectionPilsner[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 60;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(0.125.ToString());

            Speed = 120;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(0.125.ToString());

            Speed = 180;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(0.75.ToString());

            Speed = 180;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(1.75.ToString());

            Speed = 300;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(4.75.ToString());

            Speed = 360;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(5.5.ToString());

            Speed = 420;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(13.25.ToString());

            Speed = 480;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(21.5.ToString());

            Speed = 540;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(38.875.ToString());

            Speed = 600;
            SeriesCollectionPilsner[0].Values.Add(Speed);
            arrayListLabels.Add(64.5.ToString());
        }

        private void Wheat_Checked(object sender, RoutedEventArgs e)
        {
            PilserCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;
            SeriesCollectionPilsner[0].Values.Clear();
            SeriesCollectionPilsner[1].Values.Clear();
            SeriesCollectionPilsner[2].Values.Clear();
            SeriesCollectionPilsner[3].Values.Clear();
            SeriesCollectionPilsner[4].Values.Clear();
            SeriesCollectionPilsner[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(6.5.ToString());

            Speed = 60;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(10.25.ToString());

            Speed = 90;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(18.75.ToString());

            Speed = 120;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(23.25.ToString());

            Speed = 150;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(33.5.ToString());


            Speed = 180;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(49.5.ToString());

            Speed = 210;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(65.75.ToString());

            Speed = 240;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(72.75.ToString());

            Speed = 270;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(90.25.ToString());

            Speed = 300;
            SeriesCollectionPilsner[1].Values.Add(Speed);
            arrayListLabels.Add(100.ToString());
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

            SeriesCollectionPilsner[0].Values.Clear();
            SeriesCollectionPilsner[1].Values.Clear();
            SeriesCollectionPilsner[2].Values.Clear();
            SeriesCollectionPilsner[3].Values.Clear();
            SeriesCollectionPilsner[4].Values.Clear();
            SeriesCollectionPilsner[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(6.5.ToString());

            Speed = 60;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(10.25.ToString());

            Speed = 90;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(18.75.ToString());

            Speed = 120;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(23.25.ToString());

            Speed = 150;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(33.5.ToString());


            Speed = 180;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(49.5.ToString());

            Speed = 210;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(65.75.ToString());

            Speed = 240;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(72.75.ToString());

            Speed = 270;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(90.25.ToString());

            Speed = 300;
            SeriesCollectionPilsner[2].Values.Add(Speed);
            arrayListLabels.Add(100.ToString());
        }

        private void Stout_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;

            SeriesCollectionPilsner[0].Values.Clear();
            SeriesCollectionPilsner[1].Values.Clear();
            SeriesCollectionPilsner[2].Values.Clear();
            SeriesCollectionPilsner[3].Values.Clear();
            SeriesCollectionPilsner[4].Values.Clear();
            SeriesCollectionPilsner[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(6.5.ToString());

            Speed = 60;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(10.25.ToString());

            Speed = 90;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(18.75.ToString());

            Speed = 120;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(23.25.ToString());

            Speed = 150;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(33.5.ToString());


            Speed = 180;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(49.5.ToString());

            Speed = 210;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(65.75.ToString());

            Speed = 240;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(72.75.ToString());

            Speed = 270;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(90.25.ToString());

            Speed = 300;
            SeriesCollectionPilsner[3].Values.Add(Speed);
            arrayListLabels.Add(100.ToString());
        }

        private void Ale_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;
            AlcoholFreeCheckBox.IsChecked = false;

            SeriesCollectionPilsner[0].Values.Clear();
            SeriesCollectionPilsner[1].Values.Clear();
            SeriesCollectionPilsner[2].Values.Clear();
            SeriesCollectionPilsner[3].Values.Clear();
            SeriesCollectionPilsner[4].Values.Clear();
            SeriesCollectionPilsner[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(6.5.ToString());

            Speed = 60;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(10.25.ToString());

            Speed = 90;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(18.75.ToString());

            Speed = 120;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(23.25.ToString());

            Speed = 150;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(33.5.ToString());


            Speed = 180;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(49.5.ToString());

            Speed = 210;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(65.75.ToString());

            Speed = 240;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(72.75.ToString());

            Speed = 270;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(90.25.ToString());

            Speed = 300;
            SeriesCollectionPilsner[4].Values.Add(Speed);
            arrayListLabels.Add(100.ToString());
        }

        private void AlcoholFree_Checked(object sender, RoutedEventArgs e)
        {
            WheatCheckBox.IsChecked = false;
            IPACheckBox.IsChecked = false;
            StoutCheckBox.IsChecked = false;
            AleCheckBox.IsChecked = false;
            PilserCheckBox.IsChecked = false;

            SeriesCollectionPilsner[0].Values.Clear();
            SeriesCollectionPilsner[1].Values.Clear();
            SeriesCollectionPilsner[2].Values.Clear();
            SeriesCollectionPilsner[3].Values.Clear();
            SeriesCollectionPilsner[4].Values.Clear();
            SeriesCollectionPilsner[5].Values.Clear();
            arrayListLabels.Clear();

            Speed = 30;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(6.5.ToString());

            Speed = 60;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(10.25.ToString());

            Speed = 90;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(18.75.ToString());

            Speed = 120;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(23.25.ToString());

            Speed = 150;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(33.5.ToString());


            Speed = 180;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(49.5.ToString());

            Speed = 210;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(65.75.ToString());

            Speed = 240;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(72.75.ToString());

            Speed = 270;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(90.25.ToString());

            Speed = 300;
            SeriesCollectionPilsner[5].Values.Add(Speed);
            arrayListLabels.Add(100.ToString());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
