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
using System.Diagnostics;

namespace RayTracer2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private World world;
        private Camera camera;

        public MainWindow() {
            InitializeComponent();
            tbkSceneLog.Text = "";
            world = new World();
            world.AddLight(Light.PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));
            foreach(Light lt in world.Lights) {
                UpdateLog(lt.ToString());
            }
            camera = new Camera(400, 200, Math.PI / 3);
            camera.Transform = Transformation.ViewTransform(
                    Tuple.Point(-2, 4, -7),
                    Tuple.Point(0, 1, 0),
                    Tuple.Vector(0, 1, 0)
                    );
            UpdateLog(camera.ToString());
        }

        public World World {
            get { return world; }
            set { world = value; }
        }

        public Camera Camera {
            get { return camera; }
            set { camera = value; }
        }

        private void btnAddShape_Click(object sender, RoutedEventArgs e) {
            MakeSphere mksphr = new MakeSphere();
            mksphr.ShowDialog();
        }

        private void btnRender_Click(object sender, RoutedEventArgs e) {
            Stopwatch time = new Stopwatch();
            time.Start();
            Canvas canvas = camera.Render(world, tbkSceneLog);
            time.Stop();
            UpdateLog("Render Complete" + Environment.NewLine + "Time Elapsed: " + time.Elapsed.Seconds);

            StreamWriter sw = new StreamWriter(@"C:\Users\prome\Desktop\GuiTest.ppm", false, Encoding.ASCII, 131072);
            sw.Write(canvas.CanvasToPPM());
            sw.Flush();
            sw.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public void UpdateLog(string s) {
            tbkSceneLog.Text += s;
            scvLog.ScrollToBottom();
        }

        private void btnAddCamera_Click(object sender, RoutedEventArgs e) {
            MakeCamera mkcam = new MakeCamera();
            mkcam.ShowDialog();
        }

        private void btnAddLight_Click(object sender, RoutedEventArgs e) {
            MakeLight mklight = new MakeLight();
            mklight.ShowDialog();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e) {

        }
    }
}
