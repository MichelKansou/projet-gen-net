using System;
using System.Windows.Controls;

namespace Client.Navigation
{
    public static class Navigation
    {
        private static Frame _frame;

        public static Frame Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }

        // Check if current navigation page is different 
        public static bool Navigate(Page sourcePageUri)
        {
             return _frame.NavigationService.Navigate(sourcePageUri);
        }

        // Check if current page content is different
        public static bool Navigate(object content)
        {
            if (_frame.NavigationService.Content != content)
            {
                return _frame.Navigate(content);
            }
            return true;
        }

        public static void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
    }
}
