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
        public Camera camera;

        public MakeCamera() {
            InitializeComponent();
            camera = new Camera();
            txtHsize.Text = camera.Hsize.ToString();
            txtVsize.Text = camera.Vsize.ToString();
            txtFieldOfView.Text = camera.FieldOfView.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            int.TryParse(txtHsize.Text, out int result1);
            camera.Hsize = result1;

            int.TryParse(txtVsize.Text, out result1);
            camera.Vsize = result1;

            double.TryParse(txtFieldOfView.Text, out double result2);
            camera.FieldOfView = result2;

            //((MainWindow)Application.Current.MainWindow).World.Camera = camera;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            AddViewTransform addViewTransform = new AddViewTransform();
            addViewTransform.ShowDialog();
            if (addViewTransform.viewTransform is not null) {
                camera.TransformList.Add(addViewTransform.viewTransform);
                lbTransformList.Items.Add(addViewTransform.viewTransform.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (lbTransformList.SelectedIndex >= 0 && lbTransformList.SelectedIndex <= lbTransformList.Items.Count - 1) {
                camera.TransformList.RemoveAt(lbTransformList.SelectedIndex);
                lbTransformList.Items.RemoveAt(lbTransformList.SelectedIndex);
            }
        }
    }
}
