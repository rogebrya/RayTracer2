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
    /// Interaction logic for AddRotateZ.xaml
    /// </summary>
    public partial class AddRotateZ : Window {
        public ITransformation rotateZ;

        public AddRotateZ() {
            InitializeComponent();
            rotateZ = new RotateZ();
            txtTheta.Text = ((RotateZ)rotateZ).Theta.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool tSucceed = double.TryParse(txtTheta.Text, out double theta);

            if (tSucceed) {
                rotateZ = new RotateY(theta);
            } else {
                rotateZ = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            rotateZ = null;
            Close();
        }
    }
}
