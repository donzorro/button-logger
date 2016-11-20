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
        public MainWindow()
        {
            Students = new ObservableCollection<Student>();
            Students.Add(new Student { FirstName = "Carlos", LastName = "Burin", StudentId = "fedeburin" });
            Students.Add(new Student { FirstName = "Nora", LastName = "Golic", StudentId = "noragolic" });
            Students.Add(new Student { FirstName = "Robert", LastName = "Pepin", StudentId = "robertpepin" });

            InitializeComponent();

            // this.Loaded += MainWindow_Loaded;
            //StartEmulation();

            //InputManager.Current.PreProcessInput += (sender, e) =>
            //{
            //    if (e.StagingItem.Input is MouseButtonEventArgs)
            //        GlobalClickEventHandler(sender,
            //          (MouseButtonEventArgs)e.StagingItem.Input);
            //};

            MonitorService ms = new MonitorService();
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

//        private void Archive(string id, string path)
//        { 
//            DirectoryInfo target = new DirectoryInfo(@"C:\Users\Carlos\Documents\Visual Studio 2015\Projects\Samples\SavingAndZippingArchived\");
//            DirectoryInfo root = new DirectoryInfo(@"C:\Users\Carlos\Documents\Visual Studio 2015\Projects\Samples\SavingAndZipping\"+ id);

//            var now = DateTime.Now;
//            var gap = TimeSpan.FromMinutes(1);
//            var toproccess = root.GetFiles()
//                .Where(x => now.Subtract(x.LastWriteTime) > gap)
//                .Select(x => x.FullName);

//            foreach(var t in toproccess)
//            {
//                AddFileToZip(target + id + ".zip", t, CompressionOption.Maximum);
//                File.Delete(t);
//            }

//            lock(l)
//            {
//                if(Directory.GetFiles(path).Length == 0)
//                {
//                    Directory.Delete(path);
//                }
//            }
//        }

//        object l = new object();
        
//        private static void AddFileToZip(string zipFilename, string fileToAdd, CompressionOption compression = CompressionOption.Normal)
//        {
//            //using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
//            //{
//            //    string destFilename = ".\\" + System.IO.Path.GetFileName(fileToAdd);
//            //    Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
//            //    if (zip.PartExists(uri))
//            //    {
//            //        zip.DeletePart(uri);
//            //    }
//            //    PackagePart part = zip.CreatePart(uri, "", compression);
//            //    using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
//            //    {
//            //        using (Stream dest = part.GetStream())
//            //        {
//            //            fileStream.CopyTo(dest);
//            //        }
//            //    }
//            //}
//        }

//        private void StartEmulation()
//        {
////            var zipper = new Task(() =>
////            {
////                while (true)
////                {
////                    DirectoryInfo root = new DirectoryInfo(@"C:\Users\Carlos\Documents\Visual Studio 2015\Projects\Samples\SavingAndZipping\");
////                    var now = DateTime.Now;
////                    var gap = TimeSpan.FromMinutes(5);
////                    var toproccess = root.GetDirectories()
////                        .Where(x => now.Subtract(x.LastWriteTime) > gap)
////                        .Select(x => x.Name);

////                    foreach(var t in toproccess)
////                    {
////                        Archive(t, root + t);
////                    }

////                    Thread.Sleep(gap);
////                }
////            });

////            zipper.Start();



////            var producer = new Task(() =>
////            {
////                Random rnd = new Random();

////                while (true)
////                {
////                    int inquiryid = rnd.Next(150);
////                    int received = rnd.Next(1, 4000);
////                    DirectoryInfo root = new DirectoryInfo(@"C:\Users\Carlos\Documents\Visual Studio 2015\Projects\Samples\SavingAndZipping\");

////                    lock (l)
////                    {
////                        Directory.CreateDirectory(root + "\\" + inquiryid);
////                        File.WriteAllText($"{root}\\{inquiryid}\\{inquiryid}.{DateTime.Now.Ticks}.xml",
////                        @"Finalmente, Gimena Accardi (31) y Nicolás Vázquez (39) anunciaron su casamiento tras 9 años de noviazgo.
////Los actores no solo conviven en su casa sino también en el trabajo: comparten elenco en El otro lado de la cama y conducen juntos Como anillo al dedo (El Trece). Ahora, para sumar una alegría a su presente laboral, darán un paso más y contraerán matrimonio el 10 de diciembre en Mar del Plata.
////Esta será la primera boda de la actriz y la segunda de Vázquez. El actor se casó por primera vez con la actriz Mercedes Funes en 2006, luego de 6 años de noviazgo, pero se terminaron separando después de poco más de un año y medio.
////El enlace entre Gimena y Nicolás será el puntapíe del arranque de la temporada en La Feliz ya que días después trasladarán la obra de teatro a esas playas.
////Para Mercedes Funes, la ex de Vázquez, el presente amoroso no es tan auspicioso. La actriz, que se encuentra rodando un filme sobre la vida de Tita Merello, reveló que se separó hace algunos meses de su pareja, Fernando, luego de 8 años.");
////                    }


////                    Thread.Sleep(TimeSpan.FromMilliseconds(received));
////                }
////            });

////            producer.Start();

//        }

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
            System.Windows.MessageBox.Show("stop clicked!");
        }
    }
}
