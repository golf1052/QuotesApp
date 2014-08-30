using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuotesApp
{
    public class WaitingForParse
    {
        public ProgressBar progressBar;
        public List<UIElement> elements = new List<UIElement>();
        List<string> oldTexts = new List<string>();

        public WaitingForParse(UIElement elementToHide, Grid mainGrid)
        {
            SetUpProgressBar(mainGrid);
            elements.Add(elementToHide);
        }

        public WaitingForParse(List<UIElement> elementsToHide, Grid mainGrid)
        {
            SetUpProgressBar(mainGrid);
            elements = elementsToHide;
        }

        void SetUpProgressBar(Grid mainGrid)
        {
            progressBar = new ProgressBar();
            progressBar.IsIndeterminate = true;
            progressBar.Visibility = Visibility.Collapsed;
            progressBar.HorizontalAlignment = HorizontalAlignment.Left;
            progressBar.VerticalAlignment = VerticalAlignment.Top;
            progressBar.Width = mainGrid.ActualWidth;
            progressBar.Height = 10;
            mainGrid.Children.Add(progressBar);
        }

        public void Activate()
        {
            progressBar.Visibility = Visibility.Visible;
            oldTexts.Clear();
            foreach (UIElement element in elements)
            {
                oldTexts.Add("");
                if (element.GetType() == typeof(TextBlock))
                {
                    oldTexts[oldTexts.Count - 1] = ((TextBlock)element).Text;
                    ((TextBlock)element).Text = "";
                    element.IsHitTestVisible = false;
                }
                else
                {
                    element.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void Deactivate()
        {
            progressBar.Visibility = Visibility.Collapsed;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].GetType() == typeof(TextBlock))
                {
                    ((TextBlock)elements[i]).Text = oldTexts[i];
                    elements[i].IsHitTestVisible = true;
                }
                else
                {
                    elements[i].Visibility = Visibility.Visible;
                }
            }
            oldTexts.Clear();
        }
    }
}
