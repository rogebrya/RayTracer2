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
    /// Interaction logic for AddRotateY.xaml
    /// </summary>
    public partial class AddRotateY : Window {
        public ITransformation rotateY;

        public AddRotateY() {
            InitializeComponent();
            rotateY = new RotateY();
            txtTheta.Text = ((RotateY)rotateY).Theta.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool tSucceed = double.TryParse(txtTheta.Text, out double theta);

            if (tSucceed) {
                rotateY = new RotateY(theta);
            } else {
                rotateY = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            rotateY = null;
            Close();
        }
    }
}
