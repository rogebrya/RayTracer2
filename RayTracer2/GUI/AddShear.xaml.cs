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
    /// Interaction logic for AddShear.xaml
    /// </summary>
    public partial class AddShear : Window {
        public ITransformation shear;

        public AddShear() {
            InitializeComponent();
            shear = new Shear();
            txtXY.Text = ((Shear)shear).XY.ToString();
            txtXZ.Text = ((Shear)shear).XZ.ToString();
            txtYX.Text = ((Shear)shear).YX.ToString();
            txtYZ.Text = ((Shear)shear).YZ.ToString();
            txtZX.Text = ((Shear)shear).ZX.ToString();
            txtZY.Text = ((Shear)shear).ZY.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool xySucceed = double.TryParse(txtXY.Text, out double xy);

            bool xzSucceed = double.TryParse(txtXZ.Text, out double xz);

            bool yxSucceed = double.TryParse(txtYX.Text, out double yx);

            bool yzSucceed = double.TryParse(txtYZ.Text, out double yz);

            bool zxSucceed = double.TryParse(txtZX.Text, out double zx);

            bool zySucceed = double.TryParse(txtZY.Text, out double zy);

            if (xySucceed && xzSucceed && yxSucceed && yzSucceed && zxSucceed && zySucceed) {
                shear = new Shear(xy, xz, yx, yz, zx, zy);
            } else {
                shear = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            shear = null;
            Close();
        }
    }
}
