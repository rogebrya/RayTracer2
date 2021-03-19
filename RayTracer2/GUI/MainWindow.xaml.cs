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
        public List<Material> materials;

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

            try {
                StreamReader jsonReader = new StreamReader("C:\\Users\\prome\\Desktop\\materials.rtmt");
                string jsonString = jsonReader.ReadToEnd();
                world = JsonSerializer.Deserialize<World>(jsonString);
                tbkSceneLog.Text += "Materials successfully loaded from file." + Environment.NewLine;
            } catch {
                tbkSceneLog.Text += "No materials file found." + Environment.NewLine;
            }
        }

        private void btnAddShape_Click(object sender, RoutedEventArgs e) {
            MakeSphere mksphr = new MakeSphere();
            mksphr.ShowDialog();
            if (mksphr.sphere is not null) {
                world.Shapes.Add(mksphr.sphere);
            }
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
            tbkSceneLog.Text += "Render Complete" + Environment.NewLine + "Time Elapsed: " + time.Elapsed.Seconds + " seconds";
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
            if (mkcam.camera is not null) {
                world.Camera = mkcam.camera;
            }
            UpdateLog();
        }

        private void btnAddLight_Click(object sender, RoutedEventArgs e) {
            MakeLight mklight = new MakeLight();
            mklight.ShowDialog();
            if (mklight.light is not null) {
                world.Lights.Add(mklight.light);
            }
            UpdateLog();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e) {
            world = new World();
            UpdateLog();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
            };
            StreamWriter sw = new StreamWriter("C:\\Users\\prome\\Desktop\\savefile.rtsv", false, Encoding.ASCII);
            sw.Write(JsonSerializer.Serialize(world, options));
            sw.Flush();
            sw.Close();
            //string jsonString = JsonSerializer.Serialize(world, options);
            //File.WriteAllText("C:\\Users\\prome\\Desktop\\savefile", jsonString);
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) {
            StreamReader jsonReader = new StreamReader("C:\\Users\\prome\\Desktop\\savefile.rtsv");
            string jsonString = jsonReader.ReadToEnd();
            world = JsonSerializer.Deserialize<World>(jsonString);
        }

        private void btnManageMaterials_Click(object sender, RoutedEventArgs e) {
            ManageMaterials mngMaterials = new ManageMaterials();
            mngMaterials.ShowDialog();
            if (mngMaterials.materials is not null) {
                materials = mngMaterials.materials;
            }
            UpdateLog();
        }
    }
}
