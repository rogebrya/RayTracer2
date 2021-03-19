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
    /// Interaction logic for MakeMaterial.xaml
    /// </summary>
    public partial class MakeMaterial : Window {
        public Material material;
        private Pattern pattern;
        private bool patternControlsAreVisible = false;

        public MakeMaterial() {
            InitializeComponent();
            material = new Material();
            txtColorR.Text = material.Color.Red.ToString();
            txtColorB.Text = material.Color.Blue.ToString();
            txtColorG.Text = material.Color.Green.ToString();
            txtAmbient.Text = material.Ambient.ToString();
            txtDiffuse.Text = material.Diffuse.ToString();
            txtShininess.Text = material.Shininess.ToString();
            txtReflectivity.Text = material.Reflectivity.ToString();
            txtTransparency.Text = material.Transparency.ToString();
            txtRefractiveIndex.Text = material.RefractiveIndex.ToString();
            txtColorP1R.Text = "0";
            txtColorP1G.Text = "0";
            txtColorP1B.Text = "0";
            txtColorP2R.Text = "1";
            txtColorP2G.Text = "1";
            txtColorP2B.Text = "1";
            TogglePatternControls();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            bool rSucceed = double.TryParse(txtColorR.Text, out double r);
            bool gSucceed = double.TryParse(txtColorG.Text, out double g);
            bool bSucceed = double.TryParse(txtColorB.Text, out double b);
            if (rSucceed && gSucceed && bSucceed) {
                material.Color = new Color(r, g, b);
            }

            if (double.TryParse(txtAmbient.Text, out double result)) {
                material.Ambient = result;
            }
            
            if (double.TryParse(txtDiffuse.Text, out result)) {
                material.Diffuse = result;
            }
            
            if (double.TryParse(txtShininess.Text, out result)) {
                material.Shininess = result;
            }

            if (double.TryParse(txtReflectivity.Text, out result)) {
                material.Reflectivity = result;
            }
            
            if (double.TryParse(txtTransparency.Text, out result)) {
                material.Transparency = result;
            }

            if (double.TryParse(txtRefractiveIndex.Text, out result)) {
                material.RefractiveIndex = result;
            }
            
            material.Pattern = pattern;

            if (pattern is not null) {
                bool r1Succeed = double.TryParse(txtColorP1R.Text, out double r1);
                bool g1Succeed = double.TryParse(txtColorP1G.Text, out double g1);
                bool b1Succeed = double.TryParse(txtColorP1B.Text, out double b1);
                bool r2Succeed = double.TryParse(txtColorP2R.Text, out double r2);
                bool g2Succeed = double.TryParse(txtColorP2G.Text, out double g2);
                bool b2Succeed = double.TryParse(txtColorP2B.Text, out double b2);

                if (r1Succeed && g1Succeed && b1Succeed) {
                    pattern.A = new Color(r1, b1, g1);
                }
                if (r2Succeed && g2Succeed && b2Succeed) {
                    pattern.B = new Color(r2, g2, b2);
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            material = null;
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            switch (cmbAddTransform.SelectedIndex) {
                case 0:
                    AddTranslate translate = new AddTranslate();
                    translate.ShowDialog();
                    if (translate.translation is not null) {
                        pattern.TransformList.Add(translate.translation);
                        lbTransformList.Items.Add(translate.translation.ToString());
                    }
                    break;
                case 1:
                    AddScale scale = new AddScale();
                    scale.ShowDialog();
                    if (scale.scale is not null) {
                        pattern.TransformList.Add(scale.scale);
                        lbTransformList.Items.Add(scale.scale.ToString());
                    }
                    break;
                case 2:
                    AddRotateX rotateX = new AddRotateX();
                    rotateX.ShowDialog();
                    if (rotateX.rotateX is not null) {
                        pattern.TransformList.Add(rotateX.rotateX);
                        lbTransformList.Items.Add(rotateX.rotateX.ToString());
                    }
                    break;
                case 3:
                    AddRotateY rotateY = new AddRotateY();
                    rotateY.ShowDialog();
                    if (rotateY.rotateY is not null) {
                        pattern.TransformList.Add(rotateY.rotateY);
                        lbTransformList.Items.Add(rotateY.rotateY.ToString());
                    }
                    break;
                case 4:
                    AddRotateZ rotateZ = new AddRotateZ();
                    rotateZ.ShowDialog();
                    if (rotateZ.rotateZ is not null) {
                        pattern.TransformList.Add(rotateZ.rotateZ);
                        lbTransformList.Items.Add(rotateZ.rotateZ.ToString());
                    }
                    break;
                case 5:
                    AddShear shear = new AddShear();
                    shear.ShowDialog();
                    if (shear.shear is not null) {
                        pattern.TransformList.Add(shear.shear);
                        lbTransformList.Items.Add(shear.shear.ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (lbTransformList.SelectedIndex >= 0 && lbTransformList.SelectedIndex <= lbTransformList.Items.Count - 1) {
                pattern.TransformList.RemoveAt(lbTransformList.SelectedIndex);
                lbTransformList.Items.RemoveAt(lbTransformList.SelectedIndex);
            }
        }

        private void cmbPattern_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (cmbPattern.SelectedIndex) {
                case 0:
                    pattern = null;
                    patternControlsAreVisible = false;
                    break;
                case 1:
                    pattern = new Checker3DPattern();
                    patternControlsAreVisible = true;
                    break;
                case 2:
                    pattern = new GradientPattern();
                    patternControlsAreVisible = true;
                    break;
                case 3:
                    pattern = new RingPattern();
                    patternControlsAreVisible = true;
                    break;
                case 4:
                    pattern = new StripedPattern();
                    patternControlsAreVisible = true;
                    break;
                default:
                    break;
            }
            if (lbTransformList is not null) {
                lbTransformList.Items.Clear();
            }
            TogglePatternControls();
        }

        private void TogglePatternControls() {
            if (txtColorP1R is not null && txtColorP1G is not null && txtColorP1B is not null && txtColorP2R is not null && txtColorP2G is not null && txtColorP2B is not null && lbTransformList is not null) {
                if (patternControlsAreVisible == true) {
                    lblColorP1.Visibility = Visibility.Visible;
                    lblColorP2.Visibility = Visibility.Visible;
                    txtColorP1R.Visibility = Visibility.Visible;
                    txtColorP1G.Visibility = Visibility.Visible;
                    txtColorP1B.Visibility = Visibility.Visible;
                    txtColorP2R.Visibility = Visibility.Visible;
                    txtColorP2G.Visibility = Visibility.Visible;
                    txtColorP2B.Visibility = Visibility.Visible;
                    lbTransformList.Visibility = Visibility.Visible;
                    btnAdd.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                } else {
                    lblColorP1.Visibility = Visibility.Hidden;
                    lblColorP2.Visibility = Visibility.Hidden;
                    txtColorP1R.Visibility = Visibility.Hidden;
                    txtColorP1G.Visibility = Visibility.Hidden;
                    txtColorP1B.Visibility = Visibility.Hidden;
                    txtColorP2R.Visibility = Visibility.Hidden;
                    txtColorP2G.Visibility = Visibility.Hidden;
                    txtColorP2B.Visibility = Visibility.Hidden;
                    lbTransformList.Visibility = Visibility.Hidden;
                    btnAdd.Visibility = Visibility.Hidden;
                    btnDelete.Visibility = Visibility.Hidden;
                }
            }
            
            
        }
    }
}
