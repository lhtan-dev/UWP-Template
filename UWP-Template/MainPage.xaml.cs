using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Template.Classes;
using UWP_Template.Views.HamburgerMenu;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Template
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        /// <summary>
        /// Initiate the list of Nav menu items in hamburger.
        /// TODO : Make changes to symbol , label and destination page accordingly
        /// </summary>
        private List<NavMenuItem> navMenuItems = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.Home,
                    Label = "Home",
                    DestinationPage = typeof(MainPage),
                },
                new NavMenuItem()
                {
                    symbol = Symbol.Map,
                    Label = "Map",
                    DestinationPage = typeof(HamburgerPage1),
                }
            });

        public MainPage()
        {
            this.InitializeComponent();

            // To initialize the list of Nav menu items into the List View
            NavMenuListView.ItemsSource = navMenuItems;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //  Automatically redirects content frame to hamburger welcome screen
            ContentFrame.Navigate(typeof(MainPage), e.Parameter, new DrillInNavigationTransitionInfo());
            ContentFrame.BackStack.Clear();
            NavMenuListView.SelectedIndex = 0;

            //  Windows System Back button
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            ContentFrame.CanGoBack ?
            AppViewBackButtonVisibility.Visible :
            AppViewBackButtonVisibility.Collapsed;

            base.OnNavigatedTo(e);
        }


        /// <summary>
        /// Updates hamburger menu item after each time the frame navigates.
        /// </summary>
        private void UpdateHamburgerMenuItem()
        {
            for (int i = 0; i < navMenuItems.Count(); i++)
            {
                if (ContentFrame.CurrentSourcePageType == navMenuItems[i].DestinationPage)
                {
                    NavMenuListView.SelectedIndex = i;
                }
            }
        }


        /// <summary>
        /// Event handler when back button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //Frame rootFrame = Window.Current.Content as Frame;
            if (ContentFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (ContentFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                ContentFrame.GoBack();
                UpdateHamburgerMenuItem();
            }
        }

        /// <summary>
        /// Event handler when hamburger button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HamburgerMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            //  Open the pane if it's closed and vice versa when button is pressed
            HamburgerMenuSplitView.IsPaneOpen = !HamburgerMenuSplitView.IsPaneOpen;
        }


        /// <summary>
        /// Clicked handler when any item of navigation menu is presed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //  Close the split view pane
            HamburgerMenuSplitView.IsPaneOpen = false;

            //  Navigate to selected menu item pressed 
            //  Original source as parameter
            //  DrillInNavigation type of transition
            ContentFrame.Navigate(((NavMenuItem)e.ClickedItem).DestinationPage, e.OriginalSource, new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Update the back button everytime the back is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }
    }
}

