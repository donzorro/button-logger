using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{

    public class MonitorService
    {
        public  MonitorService()
        {
            InputManager.Current.PreProcessInput += (sender, e) =>
            {
                if (e.StagingItem.Input is MouseButtonEventArgs)
                    GlobalClickEventHandler(sender,
                      (MouseButtonEventArgs)e.StagingItem.Input);

                if (e.StagingItem.Input is KeyboardEventArgs)
                {
                    var d = e.StagingItem.Input as KeyboardEventArgs;
                    //Console.WriteLine(d.RoutedEvent.Name);
                    GlobalKeyEnteredEventHandler(sender, d);
                //    GlobalClickEventHandler(sender,
                //                        (MouseButtonEventArgs)e.StagingItem.Input);
                }
                  
            };

        }

        private void GlobalClickEventHandler(object sender, MouseButtonEventArgs input)
        {
            if (input.RoutedEvent.Name != "MouseDown")
            {
                return;
            }

            var element = input.Device.Target;
            var uielement = element as UIElement;
            if (uielement != null)
            {
                var time = DateTime.Now;
                Point pt = input.GetPosition(uielement);
                var id = GetStack(uielement);

                if (!string.IsNullOrWhiteSpace(id))
                    Console.WriteLine($"{time} User clicked element {id} on position {pt}");
            }

        }

        private void GlobalKeyEnteredEventHandler(object sender, KeyboardEventArgs input)
        {
            if (input.RoutedEvent.Name != "KeyDown")
            {
                return;
            }

            var element = input.Device.Target;
            var uielement = element as UIElement;
            if (uielement != null)
            {
                var time = DateTime.Now;
                //Point pt = input.GetPosition(this);
                var id = GetStack(uielement);

                if (!string.IsNullOrWhiteSpace(id))
                    Console.WriteLine($"{time} User typed {((KeyEventArgs)input).Key} on element {id} on position");
            }
        }
        public static DependencyObject GetParent(DependencyObject obj)
        {
            if (obj == null)
                return null;

            ContentElement ce = obj as ContentElement;
            if (ce != null)
            {
                DependencyObject parent = ContentOperations.GetParent(ce);
                if (parent != null)
                    return parent;

                FrameworkContentElement fce = ce as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            if (obj.GetType().Name.Contains("PopupRoot"))
                return LogicalTreeHelper.GetParent(obj);

            return VisualTreeHelper.GetParent(obj);
        }

        public static string GetStack(DependencyObject source)
        {
            var stack = new Stack<string>();
            var obj = source;
            while (obj != null)
            {
                var id = AutomationProperties.GetAutomationId(obj);
                if(!string.IsNullOrWhiteSpace(id))
                {
                    stack.Push(id);
                }

                obj = GetParent(obj);
            }

            if(stack.Count == 1)
            {

            }

            return string.Join(".", stack);
        }
    }
}
