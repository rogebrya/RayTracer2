using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace RayTracer2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        World world;
        Camera camera;
        public MainWindow() {
            InitializeComponent();
            tbkSceneLog.Text = "";
            world = new World();
            world.Light = Light.PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1));
            tbkSceneLog.Text += world.Light.ToString();
            camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Transformation.ViewTransform(
                    Tuple.Point(-2, 4, -7),
                    Tuple.Point(0, 1, 0),
                    Tuple.Vector(0, 1, 0)
                    );
            tbkSceneLog.Text += camera.ToString();
        }

        private void btnAddShape_Click(object sender, RoutedEventArgs e) {
            Sphere s = new Sphere();
            bool keep = true;
            MakeSphere mksphr = new MakeSphere(s, keep);
            mksphr.ShowDialog();
            if (keep) {
                world.AddShape(s);
                tbkSceneLog.Text += s.ToString();
            }
        }

        private void btnRender_Click(object sender, RoutedEventArgs e) {
            Canvas canvas = camera.Render(world, tbkSceneLog);
            tbkSceneLog.Text += "Render Complete";

            StreamWriter sw = new StreamWriter(@"C:\Users\prome\Desktop\GuiTest.ppm", false, Encoding.ASCII, 131072);
            sw.Write(canvas.CanvasToPPM());
            sw.Flush();
            sw.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();
        }
    }
}
