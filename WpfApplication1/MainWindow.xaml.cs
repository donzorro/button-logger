using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Test.Input;
using Microsoft.Test;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Input;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Collections.ObjectModel;
using TestStack.White.UIItems.Finders;
using TestStack.White.Factory;
using System.Diagnostics;
using WpfApplication1.Recording;

namespace WpfApplication1
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> Students
        {
            get;set;
        }

        public ObservableCollection<UserAction> UserActions
        {
            get;set;
        }
        
        public MainWindow()
        {
            Students = new ObservableCollection<Student>();
            Students.Add(new Student { FirstName = "Carlos", LastName = "Burin", StudentId = "fedeburin" });
            Students.Add(new Student { FirstName = "Nora", LastName = "Golic", StudentId = "noragolic" });
            Students.Add(new Student { FirstName = "Robert", LastName = "Pepin", StudentId = "robertpepin" });

        

            // this.Loaded += MainWindow_Loaded;
            //StartEmulation();

            //InputManager.Current.PreProcessInput += (sender, e) =>
            //{
            //    if (e.StagingItem.Input is MouseButtonEventArgs)
            //        GlobalClickEventHandler(sender,
            //          (MouseButtonEventArgs)e.StagingItem.Input);
            //};

            MonitorService ms = new MonitorService();
            UserActions = ms.UserActions;

            InitializeComponent();
        }

        //private void GlobalClickEventHandler(object sender, MouseButtonEventArgs input)
        //{
        //    if(input.RoutedEvent.Name != "MouseDown")
        //    {
        //        return;
        //    }

        //    var element = input.Device.Target;
        //    var uielement = element as UIElement;
        //    if(uielement != null)
        //    {
        //        var id = MonitorService.GetId(uielement);
        //        Point pt = input.GetPosition(this);

        //        if (!string.IsNullOrWhiteSpace(id))
        //            Console.WriteLine($"{DateTime.Now} User clicked element {id} on position {pt}");
        //    }
          
        //}


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AutomationElement textBox = AutomationUtilities.FindElementsByName(
            AutomationElement.RootElement,
            txtbox.Name)[0];
            setText(textBox, "papini");
        }

        private static TestStack.White.Application _application;
        private static TestStack.White.UIItems.WindowItems.Window _mainWindow;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo(System.Windows.Forms.Application.ExecutablePath);
            // launch the process through white application
            _application = TestStack.White.Application.Attach(Process.GetCurrentProcess());
            _mainWindow = _application.GetWindow("MainWindow", InitializeOption.NoCache);

            Point textboxLocation = txtbox.PointToScreen(new Point(0d, 0d));

            Microsoft.Test.Input.Mouse.MoveTo(new System.Drawing.Point((int)textboxLocation.X, (int)textboxLocation.Y));
            Microsoft.Test.Input.Mouse.Click(Microsoft.Test.Input.MouseButton.Left);

            TextCompositionManager.StartComposition(
                new TextComposition(InputManager.Current, txtbox, "pwpito"));

            //var button = _mainWindow.Get<Button>("stopButton");
            //button.Click();


            //TextCompositionManager.StartComposition(
            //    new TextComposition(InputManager.Current, txtbox, " papito"));

            //AutomationElement automationElement = _mainWindow.GetElement(SearchCriteria.ByAutomationId("stopButton"));
            //var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            //invokePattern.Invoke();

            //this.stopButton.

            //Keyboard.Type("Hello world.");
            //Keyboard.Press(Key.Shift);
            //Keyboard.Type("hello, capitalized world.");
            //Thread.Sleep(100);
            //SendKeys.SendWait("pepe");
            // Keyboard.Release(Key.Shift);
        }

        private void setText(AutomationElement aEditableTextField, string aText)
        {
            ValuePattern pattern = aEditableTextField.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;

            if (pattern != null)
            {
                pattern.SetValue(aText);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("stop clicked!   ");
        }

        private void OnReplayClicked(object sender, RoutedEventArgs e)
        {
            var actions = UserActions.ToList();
            PlaybackService service = new PlaybackService();
            service.Replay(actions);
        }

        private void OnClearClicked(object sender, RoutedEventArgs e)
        {
            UserActions.Clear();
        }
    }
}
