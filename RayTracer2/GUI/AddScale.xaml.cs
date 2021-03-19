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
using System.Windows.Shapes;

namespace RayTracer2 {
    /// <summary>
    /// Interaction logic for AddScale.xaml
    /// </summary>
    public partial class AddScale : Window {
        public ITransformation scale;

        public AddScale() {
            InitializeComponent();
            scale = new Scale();
            txtX.Text = ((Scale)scale).X.ToString();
            txtY.Text = ((Scale)scale).Y.ToString();
            txtZ.Text = ((Scale)scale).Z.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool xSucceed = double.TryParse(txtX.Text, out double x);

            bool ySucceed = double.TryParse(txtY.Text, out double y);

            bool zSucceed = double.TryParse(txtZ.Text, out double z);

            if (xSucceed && ySucceed && zSucceed) {
                scale = new Scale(x, y, z);
            } else {
                scale = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            scale = null;
            Close();
        }
    }
}
