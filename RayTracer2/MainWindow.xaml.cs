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
using System.Text.Json;

namespace RayTracer2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private World world;

        public MainWindow() {
            InitializeComponent();
            tbkSceneLog.Text = "";
            world = new World();
            world.AddLight(Light.PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)));
            foreach (Light lt in world.Lights) {
                UpdateLog();
            }
            world.Camera = new Camera(400, 200, Math.PI / 3);
            world.Camera.Transform = new ViewTransform(
                    Tuple.Point(-2, 4, -7),
                    Tuple.Point(0, 1, 0),
                    Tuple.Vector(0, 1, 0)
                    ).GetTransform();
            UpdateLog();
        }

        public World World {
            get { return world; }
            set { world = value; }
        }

        private void btnAddShape_Click(object sender, RoutedEventArgs e) {
            MakeSphere mksphr = new MakeSphere();
            mksphr.ShowDialog();
            UpdateLog();
        }

        private void btnRender_Click(object sender, RoutedEventArgs e) {
            Stopwatch time = new Stopwatch();
            time.Start();
            Canvas canvas = world.Camera.RenderWrapper(world);

            StreamWriter sw = new StreamWriter(@"C:\Users\prome\Desktop\GuiTest.ppm", false, Encoding.ASCII, 131072);
            sw.Write(canvas.CanvasToPPM());
            sw.Flush();
            sw.Close();

            time.Stop();
            tbkSceneLog.Text += "Render Complete" + Environment.NewLine + "Time Elapsed: " + time.Elapsed.Seconds;
            scvLog.ScrollToBottom();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public void UpdateLog() {
            tbkSceneLog.Text = world.ToString();
            scvLog.ScrollToBottom();
        }

        private void btnAddCamera_Click(object sender, RoutedEventArgs e) {
            MakeCamera mkcam = new MakeCamera();
            mkcam.ShowDialog();
            UpdateLog();
        }

        private void btnAddLight_Click(object sender, RoutedEventArgs e) {
            MakeLight mklight = new MakeLight();
            mklight.ShowDialog();
            UpdateLog();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e) {

        }

        public void SaveWorld() {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(world, options);
            File.WriteAllText("C:\\Users\\prome\\Desktop\\savefile", jsonString);
        }

        public void LoadWorld() {
            string jsonString = File.ReadAllText("C:\\Users\\prome\\Desktop\\savefile");
            world = JsonSerializer.Deserialize<World>(jsonString);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            SaveWorld();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) {
            LoadWorld();
        }
    }
}
