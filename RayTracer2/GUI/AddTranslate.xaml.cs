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
    /// Interaction logic for AddTranslate.xaml
    /// </summary>
    public partial class AddTranslate : Window {
        public ITransformation translation;

        public AddTranslate() {
            InitializeComponent();
            translation = new Translate();
            txtX.Text = ((Translate)translation).X.ToString();
            txtY.Text = ((Translate)translation).Y.ToString();
            txtZ.Text = ((Translate)translation).Z.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool xSucceed = double.TryParse(txtX.Text, out double x);

            bool ySucceed = double.TryParse(txtY.Text, out double y);

            bool zSucceed = double.TryParse(txtZ.Text, out double z);

            if (xSucceed && ySucceed && zSucceed) {
                translation = new Translate(x, y, z);
            } else {
                translation = null;
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            translation = null;
            Close();
        }
    }
}
