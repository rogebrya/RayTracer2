using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for ManageMaterials.xaml
    /// </summary>
    public partial class ManageMaterials : Window {
        public List<Material> materials;

        public ManageMaterials() {
            InitializeComponent();
            materials = ((MainWindow)Application.Current.MainWindow).materials;
            if (materials is null) {
                materials = new List<Material>();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            MakeMaterial mkMat = new MakeMaterial();
            mkMat.ShowDialog();
            if (mkMat.material is not null) {
                materials.Add(mkMat.material);
                lbMaterialSelection.Items.Add(mkMat.material.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (lbMaterialSelection.SelectedIndex >= 0 && lbMaterialSelection.SelectedIndex <= lbMaterialSelection.Items.Count - 1) {
                materials.RemoveAt(lbMaterialSelection.SelectedIndex);
                lbMaterialSelection.Items.RemoveAt(lbMaterialSelection.SelectedIndex);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            ((MainWindow)Application.Current.MainWindow).materials = materials;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void lbMaterialSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tbkMaterialDescription.Text = materials[lbMaterialSelection.SelectedIndex].ToString();
        }

        private void btnSaveList_Click(object sender, RoutedEventArgs e) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
            };
            StreamWriter sw = new StreamWriter("C:\\Users\\prome\\Desktop\\materials.rtmt", false, Encoding.ASCII);
            sw.Write(JsonSerializer.Serialize(materials, options));
            sw.Flush();
            sw.Close();
        }
    }
}
