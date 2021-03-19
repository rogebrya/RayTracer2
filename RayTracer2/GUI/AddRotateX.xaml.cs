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
    /// Interaction logic for AddRotateX.xaml
    /// </summary>
    public partial class AddRotateX : Window {
        public ITransformation rotateX;

        public AddRotateX() {
            InitializeComponent();
            rotateX = new RotateX();
            txtTheta.Text = ((RotateX)rotateX).Theta.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool tSucceed = double.TryParse(txtTheta.Text, out double theta);

            if (tSucceed) {
                rotateX = new RotateX(theta);
            } else {
                rotateX = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            rotateX = null;
            Close();
        }
    }
}
