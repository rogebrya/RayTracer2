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
    /// Interaction logic for MakeCamera.xaml
    /// </summary>
    public partial class MakeCamera : Window {
        Camera camera;

        public MakeCamera() {
            InitializeComponent();
            camera = new Camera();
            txtHsize.Text = camera.Hsize.ToString();
            txtVsize.Text = camera.Vsize.ToString();
            txtFieldOfView.Text = camera.FieldOfView.ToString();
            txt00.Text = camera.Transform.GetMatrix[0, 0].ToString();
            txt01.Text = camera.Transform.GetMatrix[0, 1].ToString();
            txt02.Text = camera.Transform.GetMatrix[0, 2].ToString();
            txt03.Text = camera.Transform.GetMatrix[0, 3].ToString();
            txt10.Text = camera.Transform.GetMatrix[1, 0].ToString();
            txt11.Text = camera.Transform.GetMatrix[1, 1].ToString();
            txt12.Text = camera.Transform.GetMatrix[1, 2].ToString();
            txt13.Text = camera.Transform.GetMatrix[1, 3].ToString();
            txt20.Text = camera.Transform.GetMatrix[2, 0].ToString();
            txt21.Text = camera.Transform.GetMatrix[2, 1].ToString();
            txt22.Text = camera.Transform.GetMatrix[2, 2].ToString();
            txt23.Text = camera.Transform.GetMatrix[2, 3].ToString();
            txt30.Text = camera.Transform.GetMatrix[3, 0].ToString();
            txt31.Text = camera.Transform.GetMatrix[3, 1].ToString();
            txt32.Text = camera.Transform.GetMatrix[3, 2].ToString();
            txt33.Text = camera.Transform.GetMatrix[3, 3].ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            int.TryParse(txtHsize.Text, out int result1);
            camera.Hsize = result1;

            int.TryParse(txtVsize.Text, out result1);
            camera.Vsize = result1;

            double.TryParse(txtFieldOfView.Text, out double result2);
            camera.FieldOfView = result2;

            double.TryParse(txt00.Text, out double m00);
            double.TryParse(txt01.Text, out double m01);
            double.TryParse(txt02.Text, out double m02);
            double.TryParse(txt03.Text, out double m03);

            double.TryParse(txt10.Text, out double m10);
            double.TryParse(txt11.Text, out double m11);
            double.TryParse(txt12.Text, out double m12);
            double.TryParse(txt13.Text, out double m13);

            double.TryParse(txt20.Text, out double m20);
            double.TryParse(txt21.Text, out double m21);
            double.TryParse(txt22.Text, out double m22);
            double.TryParse(txt23.Text, out double m23);

            double.TryParse(txt30.Text, out double m30);
            double.TryParse(txt31.Text, out double m31);
            double.TryParse(txt32.Text, out double m32);
            double.TryParse(txt33.Text, out double m33);

            camera.Transform = new Matrix(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);

            ((MainWindow)Application.Current.MainWindow).World.Camera = camera;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
