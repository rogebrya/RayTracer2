using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RayTracer2 {
    /// <summary>
    /// Interaction logic for MakeLight.xaml
    /// </summary>
    public partial class MakeLight : Window {
        public Light light;

        public MakeLight() {
            InitializeComponent();
            light = new Light();
            txtPositionX.Text = light.Position.X.ToString();
            txtPositionY.Text = light.Position.Y.ToString();
            txtPositionZ.Text = light.Position.Z.ToString();
            txtColorR.Text = light.Intensity.Red.ToString();
            txtColorB.Text = light.Intensity.Green.ToString();
            txtColorG.Text = light.Intensity.Blue.ToString();
            txtPositionXT.Text = light.Target.X.ToString();
            txtPositionYT.Text = light.Target.Y.ToString();
            txtPositionZT.Text = light.Target.Z.ToString();
            txtFieldOfView.Text = light.LightType;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            double.TryParse(txtPositionX.Text, out double x);
            double.TryParse(txtPositionY.Text, out double y);
            double.TryParse(txtPositionZ.Text, out double z);
            Tuple position = Tuple.Point(x, y, z);

            double.TryParse(txtColorR.Text, out double r);
            double.TryParse(txtColorG.Text, out double g);
            double.TryParse(txtColorB.Text, out double b);
            Color color = new Color(r, g, b);

            if (cmbLight.SelectedIndex == 0) {
                light = Light.PointLight(position, color);

            } else if (cmbLight.SelectedIndex == 1) {
                double.TryParse(txtPositionXT.Text, out double xt);
                double.TryParse(txtPositionYT.Text, out double yt);
                double.TryParse(txtPositionZT.Text, out double zt);
                Tuple target = Tuple.Point(xt, yt, zt);

                double.TryParse(txtFieldOfView.Text, out double result);
                double fieldOfView = result;

                light = Light.SpotLight(position, color, target, fieldOfView);
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void cmbLight_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(((ComboBox)e.Source).SelectedIndex == 0) {
                //light = Light.PointLight();
            } else if (((ComboBox)e.Source).SelectedIndex == 1) {
                //light = Light.SpotLight();
            }
            txtPositionXT.Text = light.Target.X.ToString();
            txtPositionYT.Text = light.Target.Y.ToString();
            txtPositionZT.Text = light.Target.Z.ToString();
            txtFieldOfView.Text = light.LightType;
        }
    }
}
