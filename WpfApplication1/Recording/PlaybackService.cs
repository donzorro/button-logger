using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApplication1.Recording;

namespace WpfApplication1
{
    public class PlaybackService
    {
        public void Replay(IEnumerable<UserAction> actions)
        {
            Task t = new Task(() =>
           {
               foreach (var a in actions)
               {
                   Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() => Replay(a)));

                   Thread.Sleep(TimeSpan.FromSeconds(2));
               }
           });

            t.Start();
        }

        private void Replay(UserAction action)
        {
            //Application.Current.Windows.
            var target = NavigatePath(action.Path, Application.Current.MainWindow);

            if (target == null)
            {
                MessageBox.Show($"Unable to navigate path {action.Path}");
            }
            else
            {
                Point pointLocation = target.PointToScreen(action.Point);
                Microsoft.Test.Input.Mouse.MoveTo(new System.Drawing.Point((int)pointLocation.X, (int)pointLocation.Y));
                Microsoft.Test.Input.Mouse.Click(Microsoft.Test.Input.MouseButton.Left);
            }
         }
        

        private FrameworkElement NavigatePath(string path, FrameworkElement root)
        {
            var navigationPath = path.Split('.');
            FrameworkElement uielement = root;
            foreach (var element in navigationPath)
            {
                if (uielement == null)
                {
                    return null;
                }

                if(element == "^Popup")
                {
                    var popup = PresentationSource.CurrentSources.OfType<HwndSource>()
                          .Select(h => h.RootVisual)
                          .OfType<FrameworkElement>()
                          .Select(f => f.Parent)
                          .OfType<Popup>()
                          .Where(p => p.IsOpen).FirstOrDefault();

                    var popupRoot = LogicalTreeHelper.GetChildren(popup).Cast<FrameworkElement>().FirstOrDefault();
                    uielement = popupRoot;

                    continue;
                }
                else
                {
                    uielement = FindChild<FrameworkElement>(uielement, element);
                }
              }

            return uielement;

          }


        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            if (string.IsNullOrEmpty(childName)) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                
                var frameworkElement = child as FrameworkElement;
                // If the child's name is set for search
                if (frameworkElement != null && childType != null &&  AutomationProperties.GetAutomationId( frameworkElement) == childName)
                {
                    // if the child's name is of the request name
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null)
                        break;
                }
            }

            return foundChild;
        }
    }
}
