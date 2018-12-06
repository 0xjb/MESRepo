using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for BatchSetup.xaml
    /// </summary>
    public partial class BatchSetup : Window
    {
        private IPresentation presentationFacade;
        MainWindow window;
        public BatchSetup(IPresentation pf, MainWindow w)
        {
            window = w;
            PresentationFacade = pf;
            InitializeComponent();
            batchQueueGrid.ItemsSource = presentationFacade.ILogic.Batches.Batches;
            DataContext = this;
            ProductTypeTB.DataContext = GetRecipes();
        }

        public IPresentation PresentationFacade
        {
            get { return presentationFacade; }
            set { presentationFacade = value; }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float batchId = presentationFacade.ILogic.GetHighestBatchId() + 1;
                float productType = float.Parse(ProductTypeTB.Text);
                float amount = float.Parse(AmountTB.Text);
                presentationFacade.ILogic.CreateBatch(batchId, amount, productType);
                testlabel.Content = "Batch added to the list";
            }
            catch (System.FormatException)
            {
                testlabel.Content = "you must insert correct values into the boxes";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ISimpleBatch b = presentationFacade.ILogic.GetCurrentBatch();
            b.TimestampStart = DateTime.Now.ToString();
            presentationFacade.ILogic.StartProduction();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (batchQueueGrid.SelectedItem != null)
            {
                presentationFacade.ILogic.Batches.MoveUp(batchQueueGrid.SelectedItem as ISimpleBatch);
            }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (batchQueueGrid.SelectedItem != null)
            {
                presentationFacade.ILogic.Batches.MoveDown(batchQueueGrid.SelectedItem as ISimpleBatch);
            }

        }

        private IList<IRecipe> GetRecipes()
        {
            IDictionary<float, IRecipe> recipes = presentationFacade.ILogic.GetAllRecipes();
            IList<IRecipe> list = new List<IRecipe>();
            foreach (KeyValuePair<float, IRecipe> recipe in recipes)
            {
                list.Add(recipe.Value);
            }
            return list;
        }
    }
}
