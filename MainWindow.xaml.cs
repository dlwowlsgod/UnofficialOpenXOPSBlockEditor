using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnofficialXOPSBlockEditor
{
    public class BlockMaterial
    {
        public int textureID;
        public float[] u = new float[4];
        public float[] v = new float[4];
        public float center_x;
        public float center_y;
        public float center_z;
        public float vx;
        public float vy;
        public float vz;
        public float shadow;
    }
    public class BlockData
    {
        public int ID;
        public float[] x = new float[8];
        public float[] y = new float[8];
        public float[] z = new float[8];
        public BlockMaterial[] material = new BlockMaterial[6];
    }
    public partial class MainWindow : Window
    {
        BlockData[] Block;
        string[] BlockTexture;
        bool isChanged;
        public MainWindow()
        {
            Block = new BlockData[1600];
            BlockTexture = new string[100];
            isChanged = false;
            InitializeComponent();
        }

        //Event Handlers
        private void NewBlock(object sender, RoutedEventArgs e)
        {
            Model3DGroup cube = new();
            Point3D p0 = new(0, 0, 0);
            Point3D p1 = new(5, 0, 0);
            Point3D p2 = new(5, 0, 5);
            Point3D p3 = new(0, 0, 5);
            Point3D p4 = new(0, 5, 0);
            Point3D p5 = new(5, 5, 0);
            Point3D p6 = new(5, 5, 5);
            Point3D p7 = new(0, 5, 5);

            //front side triangles
            cube.Children.Add(CreateTriangleModel(p3, p2, p6));
            cube.Children.Add(CreateTriangleModel(p3, p6, p7));
            //right side triangles
            cube.Children.Add(CreateTriangleModel(p2, p1, p5));
            cube.Children.Add(CreateTriangleModel(p2, p5, p6));
            //back side triangles
            cube.Children.Add(CreateTriangleModel(p1, p0, p4));
            cube.Children.Add(CreateTriangleModel(p1, p4, p5));
            //left side triangles
            cube.Children.Add(CreateTriangleModel(p0, p3, p7));
            cube.Children.Add(CreateTriangleModel(p0, p7, p4));
            //top side triangles
            cube.Children.Add(CreateTriangleModel(p7, p6, p5));
            cube.Children.Add(CreateTriangleModel(p7, p5, p4));
            //bottom side triangles
            cube.Children.Add(CreateTriangleModel(p2, p3, p0));
            cube.Children.Add(CreateTriangleModel(p2, p0, p1));

            ModelVisual3D model = new();
            model.Content = cube;
            this.RenderView.Children.Add(model);
        }

        //Private Functions
        private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            Vector3D normal = CalculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.White));
            GeometryModel3D model = new(mesh, material);
            Model3DGroup group = new();
            group.Children.Add(model);
            return group;
        }
        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }


    }
}