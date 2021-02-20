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
        Sphere sphere;
        bool keep = true;
        public MakeSphere(Sphere s, bool b) {
            InitializeComponent();
            sphere = s;
            keep = b;
            txt00.Text = sphere.Transform.GetMatrix[0, 0].ToString();
            txt01.Text = sphere.Transform.GetMatrix[0, 1].ToString();
            txt02.Text = sphere.Transform.GetMatrix[0, 2].ToString();
            txt03.Text = sphere.Transform.GetMatrix[0, 3].ToString();
            txt10.Text = sphere.Transform.GetMatrix[1, 0].ToString();
            txt11.Text = sphere.Transform.GetMatrix[1, 1].ToString();
            txt12.Text = sphere.Transform.GetMatrix[1, 2].ToString();
            txt13.Text = sphere.Transform.GetMatrix[1, 3].ToString();
            txt20.Text = sphere.Transform.GetMatrix[2, 0].ToString();
            txt21.Text = sphere.Transform.GetMatrix[2, 1].ToString();
            txt22.Text = sphere.Transform.GetMatrix[2, 2].ToString();
            txt23.Text = sphere.Transform.GetMatrix[2, 3].ToString();
            txt30.Text = sphere.Transform.GetMatrix[3, 0].ToString();
            txt31.Text = sphere.Transform.GetMatrix[3, 1].ToString();
            txt32.Text = sphere.Transform.GetMatrix[3, 2].ToString();
            txt33.Text = sphere.Transform.GetMatrix[3, 3].ToString();
            txtColorR.Text = sphere.Material.Color.Red.ToString();
            txtColorB.Text = sphere.Material.Color.Blue.ToString();
            txtColorG.Text = sphere.Material.Color.Green.ToString();
            txtAmbient.Text = sphere.Material.Ambient.ToString();
            txtDiffuse.Text = sphere.Material.Diffuse.ToString();
            txtShininess.Text = sphere.Material.Shininess.ToString();
            txtReflectivity.Text = sphere.Material.Reflectivity.ToString();
            txtTransparency.Text = sphere.Material.Transparency.ToString();
            txtRefractiveIndex.Text = sphere.Material.RefractiveIndex.ToString();
            txtColorP1R.Text = "0";
            txtColorP1G.Text = "0";
            txtColorP1B.Text = "0";
            txtColorP2R.Text = "1";
            txtColorP2G.Text = "1";
            txtColorP2B.Text = "1";
            Matrix identityMatrix = Matrix.GetIdentityMatrix();
            txt00P.Text = identityMatrix.GetMatrix[0, 0].ToString();
            txt01P.Text = identityMatrix.GetMatrix[0, 1].ToString();
            txt02P.Text = identityMatrix.GetMatrix[0, 2].ToString();
            txt03P.Text = identityMatrix.GetMatrix[0, 3].ToString();
            txt10P.Text = identityMatrix.GetMatrix[1, 0].ToString();
            txt11P.Text = identityMatrix.GetMatrix[1, 1].ToString();
            txt12P.Text = identityMatrix.GetMatrix[1, 2].ToString();
            txt13P.Text = identityMatrix.GetMatrix[1, 3].ToString();
            txt20P.Text = identityMatrix.GetMatrix[2, 0].ToString();
            txt21P.Text = identityMatrix.GetMatrix[2, 1].ToString();
            txt22P.Text = identityMatrix.GetMatrix[2, 2].ToString();
            txt23P.Text = identityMatrix.GetMatrix[2, 3].ToString();
            txt30P.Text = identityMatrix.GetMatrix[3, 0].ToString();
            txt31P.Text = identityMatrix.GetMatrix[3, 1].ToString();
            txt32P.Text = identityMatrix.GetMatrix[3, 2].ToString();
            txt33P.Text = identityMatrix.GetMatrix[3, 3].ToString();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            keep = false;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) {
            double.TryParse(txt00.Text, out double m00);
            double.TryParse(txt01.Text, out double m01);
            double.TryParse(txt02.Text, out double m02);
            double.TryParse(txt03.Text, out double m03);

            double.TryParse(txt10.Text, out double m10);
            double.TryParse(txt11.Text, out double m11);
            double.TryParse(txt12.Text, out double m12);
            double.TryParse(txt13.Text, out double m13);

            double.TryParse(txt20.Text, out double m20);
            double.TryParse(txt21.Text, out double m21);
            double.TryParse(txt22.Text, out double m22);
            double.TryParse(txt23.Text, out double m23);

            double.TryParse(txt30.Text, out double m30);
            double.TryParse(txt31.Text, out double m31);
            double.TryParse(txt32.Text, out double m32);
            double.TryParse(txt33.Text, out double m33);

            sphere.Transform = new Matrix(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);

            double.TryParse(txtColorR.Text, out double r);
            double.TryParse(txtColorG.Text, out double g);
            double.TryParse(txtColorB.Text, out double b);
            sphere.Material.Color = new Color(r, g, b);

            double.TryParse(txtAmbient.Text, out double result);
            sphere.Material.Ambient = result;

            double.TryParse(txtDiffuse.Text, out result);
            sphere.Material.Diffuse = result;

            double.TryParse(txtShininess.Text, out result);
            sphere.Material.Shininess = result;

            double.TryParse(txtReflectivity.Text, out result);
            sphere.Material.Reflectivity = result;

            double.TryParse(txtTransparency.Text, out result);
            sphere.Material.Transparency = result;

            double.TryParse(txtRefractiveIndex.Text, out result);
            sphere.Material.RefractiveIndex = result;


            if (cmbPattern.SelectedIndex != 0) {
                double.TryParse(txtColorP1R.Text, out double r1);
                double.TryParse(txtColorP1G.Text, out double g1);
                double.TryParse(txtColorP1B.Text, out double b1);
                double.TryParse(txtColorP2R.Text, out double r2);
                double.TryParse(txtColorP2G.Text, out double g2);
                double.TryParse(txtColorP2B.Text, out double b2);

                switch (cmbPattern.SelectedIndex) {
                    case 1:
                        sphere.Material.Pattern = new GradientPattern(new Color(r1, b1, g1), new Color(r2, g2, b2));
                        break;
                    case 2:
                        sphere.Material.Pattern = new StripedPattern(new Color(r1, b1, g1), new Color(r2, g2, b2));
                        break;
                    case 3:
                        sphere.Material.Pattern = new Checker3DPattern(new Color(r1, b1, g1), new Color(r2, g2, b2));
                        break;
                    case 4:
                        sphere.Material.Pattern = new RingPattern(new Color(r1, b1, g1), new Color(r2, g2, b2));
                        break;
                    default:
                        break;
                }

                double.TryParse(txt00P.Text, out double m00P);
                double.TryParse(txt01P.Text, out double m01P);
                double.TryParse(txt02P.Text, out double m02P);
                double.TryParse(txt03P.Text, out double m03P);

                double.TryParse(txt10P.Text, out double m10P);
                double.TryParse(txt11P.Text, out double m11P);
                double.TryParse(txt12P.Text, out double m12P);
                double.TryParse(txt13P.Text, out double m13P);

                double.TryParse(txt20P.Text, out double m20P);
                double.TryParse(txt21P.Text, out double m21P);
                double.TryParse(txt22P.Text, out double m22P);
                double.TryParse(txt23P.Text, out double m23P);

                double.TryParse(txt30P.Text, out double m30P);
                double.TryParse(txt31P.Text, out double m31P);
                double.TryParse(txt32P.Text, out double m32P);
                double.TryParse(txt33P.Text, out double m33P);

                sphere.Material.Pattern.Transform = new Matrix(m00P, m01P, m02P, m03P, m10P, m11P, m12P, m13P, m20P, m21P, m22P, m23P, m30P, m31P, m32P, m33P);
            }

            Close();
        }
    }
}
