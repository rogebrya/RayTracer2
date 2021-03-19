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
    /// Interaction logic for MakeSphere.xaml
    /// </summary>
    public partial class MakeSphere : Window {
        public Sphere sphere;
        List<Material> materials;

        public MakeSphere() {
            InitializeComponent();
            sphere = new Sphere();
            materials = ((MainWindow)Application.Current.MainWindow).materials;
            if (materials is not null) {
                foreach (Material mat in materials) {
                    lbMaterialSelection.Items.Add(mat.ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            sphere.Material = materials[lbMaterialSelection.SelectedIndex];

            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            switch (cmbAddTransform.SelectedIndex) {
                case 0:
                    AddTranslate translate = new AddTranslate();
                    translate.ShowDialog();
                    if (translate.translation is not null) {
                        sphere.TransformList.Add(translate.translation);
                        lbTransformList.Items.Add(translate.translation.ToString());
                    }
                    break;
                case 1:
                    AddScale scale = new AddScale();
                    scale.ShowDialog();
                    if (scale.scale is not null) {
                        sphere.TransformList.Add(scale.scale);
                        lbTransformList.Items.Add(scale.scale.ToString());
                    }
                    break;
                case 2:
                    AddRotateX rotateX = new AddRotateX();
                    rotateX.ShowDialog();
                    if (rotateX.rotateX is not null) {
                        sphere.TransformList.Add(rotateX.rotateX);
                        lbTransformList.Items.Add(rotateX.rotateX.ToString());
                    }
                    break;
                case 3:
                    AddRotateY rotateY = new AddRotateY();
                    rotateY.ShowDialog();
                    if (rotateY.rotateY is not null) {
                        sphere.TransformList.Add(rotateY.rotateY);
                        lbTransformList.Items.Add(rotateY.rotateY.ToString());
                    }
                    break;
                case 4:
                    AddRotateZ rotateZ = new AddRotateZ();
                    rotateZ.ShowDialog();
                    if (rotateZ.rotateZ is not null) {
                        sphere.TransformList.Add(rotateZ.rotateZ);
                        lbTransformList.Items.Add(rotateZ.rotateZ.ToString());
                    }
                    break;
                case 5:
                    AddShear shear = new AddShear();
                    shear.ShowDialog();
                    if (shear.shear is not null) {
                        sphere.TransformList.Add(shear.shear);
                        lbTransformList.Items.Add(shear.shear.ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (lbTransformList.SelectedIndex >= 0 && lbTransformList.SelectedIndex <= lbTransformList.Items.Count - 1) {
                sphere.TransformList.RemoveAt(lbTransformList.SelectedIndex);
                lbTransformList.Items.RemoveAt(lbTransformList.SelectedIndex);
            }
        }

        private void lbMaterialSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tbkMaterialDescription.Text = materials[lbMaterialSelection.SelectedIndex].ToString();
        }
    }
}
