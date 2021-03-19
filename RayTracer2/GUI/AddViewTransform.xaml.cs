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
    /// Interaction logic for AddViewTransform.xaml
    /// </summary>
    public partial class AddViewTransform : Window {
        public ITransformation viewTransform;

        public AddViewTransform() {
            InitializeComponent();
            viewTransform = new ViewTransform();
            txtFromX.Text = ((ViewTransform)viewTransform).From.X.ToString();
            txtFromY.Text = ((ViewTransform)viewTransform).From.Y.ToString();
            txtFromZ.Text = ((ViewTransform)viewTransform).From.Z.ToString();
            txtToX.Text = ((ViewTransform)viewTransform).To.X.ToString();
            txtToY.Text = ((ViewTransform)viewTransform).To.Y.ToString();
            txtToZ.Text = ((ViewTransform)viewTransform).To.Z.ToString();
            txtUpX.Text = ((ViewTransform)viewTransform).Up.X.ToString();
            txtUpY.Text = ((ViewTransform)viewTransform).Up.Y.ToString();
            txtUpZ.Text = ((ViewTransform)viewTransform).Up.Z.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool fxSucceed = double.TryParse(txtFromX.Text, out double fromX);

            bool fySucceed = double.TryParse(txtFromY.Text, out double fromY);

            bool fzSucceed = double.TryParse(txtFromZ.Text, out double fromZ);

            bool txSucceed = double.TryParse(txtToX.Text, out double toX);

            bool tySucceed = double.TryParse(txtToY.Text, out double toY);

            bool tzSucceed = double.TryParse(txtToZ.Text, out double toZ);

            bool uxSucceed = double.TryParse(txtUpX.Text, out double upX);

            bool uySucceed = double.TryParse(txtUpY.Text, out double upY);

            bool uzSucceed = double.TryParse(txtUpZ.Text, out double upZ);

            if (fxSucceed && fySucceed && fzSucceed && txSucceed && tySucceed && tzSucceed && uxSucceed && uySucceed && uzSucceed) {
                viewTransform = new ViewTransform(fromX, fromY, fromZ, toX, toY, toZ, upX, upY, upZ);
            } else {
                viewTransform = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            viewTransform = null;
            Close();
        }
    }
}
