using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using TSG = Tekla.Structures.Geometry3d;

namespace BeamApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model myModel = new Model();

            //Beam myBeam = new Beam(new TSG.Point(1000, 1000, 1000),
            //                        new TSG.Point(6000, 6000, 1000));
            Beam myBeam = new Beam();
            myBeam.Name = "Хомут";
            myBeam.Material.MaterialString = "Ст3сп5_сортовой";
            myBeam.Profile.ProfileString = "CL16_5781_82";
            myBeam.PartNumber.Prefix = "Деталь";
            myBeam.PartNumber.StartNumber = 1;
            myBeam.AssemblyNumber.Prefix = "Сборка";
            myBeam.AssemblyNumber.StartNumber = 1;
            myBeam.Class = "20";
            myBeam.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            myBeam.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
            myBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            myBeam.Finish = "";
            myBeam.StartPoint = new TSG.Point(0, 0, 0);
            myBeam.EndPoint = new TSG.Point(1000, 0, 0);


            myBeam.Insert();
            myModel.CommitChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model myModel = new Model();

            Chamfer myChamfer = new TSM.Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT);


            ContourPoint point = new ContourPoint(new TSG.Point(0, 0, 0), null);
            ContourPoint point2 = new ContourPoint(new TSG.Point(2000, 2000, 0), myChamfer);
            ContourPoint point3 = new ContourPoint(new TSG.Point(0, 4000, 0), null);

            PolyBeam myPolyBeam = new PolyBeam();

            myPolyBeam.AddContourPoint(point);
            myPolyBeam.AddContourPoint(point2);
            myPolyBeam.AddContourPoint(point3);

            myPolyBeam.Name = "Хомут";
            myPolyBeam.Material.MaterialString = "Ст3сп5_сортовой";
            myPolyBeam.Profile.ProfileString = "CL16_5781_82";
            myPolyBeam.PartNumber.Prefix = "Деталь";
            myPolyBeam.PartNumber.StartNumber = 1;
            myPolyBeam.AssemblyNumber.Prefix = "Сборка";
            myPolyBeam.AssemblyNumber.StartNumber = 1;
            myPolyBeam.Class = "20";
            myPolyBeam.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            myPolyBeam.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
            myPolyBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            myPolyBeam.Finish = "";
                       
            bool Result = false;
            Result = myPolyBeam.Insert();
            myModel.CommitChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Model myModel = new Model();
            Picker _picker = new Picker();
            ArrayList pickedPoints = _picker.PickPoints(Picker.PickPointEnum.PICK_POLYGON, "Укажи полигон");

            ArrayList contourPoints = new ArrayList();
            foreach (Point point in pickedPoints)
            {
                ContourPoint contourPoint = new ContourPoint(point, new Chamfer());
                contourPoints.Add(contourPoint);
            }

            Tekla.Structures.Model.ContourPlate myContourPlate = new Tekla.Structures.Model.ContourPlate();
            myContourPlate.Contour.ContourPoints = contourPoints;
            myContourPlate.Profile.ProfileString = "PL25";
            myContourPlate.Material.MaterialString = "S235";
            myContourPlate.Name = "Хомут";
            myContourPlate.Material.MaterialString = "Ст3сп5_сортовой";
            //myBeam.Profile.ProfileString = "CL16_5781_82";
            myContourPlate.PartNumber.Prefix = "Деталь";
            myContourPlate.PartNumber.StartNumber = 1;
            myContourPlate.AssemblyNumber.Prefix = "Сборка";
            myContourPlate.AssemblyNumber.StartNumber = 1;
            myContourPlate.Position.Depth = TSM.Position.DepthEnum.FRONT;
            myContourPlate.Class = "20";
            myContourPlate.Finish = "";
            //myBeam.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            //myBeam.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
            //myBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            //myBeam.StartPoint = new TSG.Point(0, 0, 0);
            //myBeam.EndPoint = new TSG.Point(1000, 0, 0);

            myContourPlate.Insert();
            myModel.CommitChanges();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Model myModel = new Model();

            //The transformation plane to be set as the current transformation plane
            myModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());

            //Define origin and two vectors to set the UCS to the XZ plane
            TSG.Point Origin = new TSG.Point(0, 0, 0);  //Represents the origin
            TSG.Vector X = new TSG.Vector(1, 0, 1);     //Represents x-axis
            TSG.Vector Y = new TSG.Vector(0, 1, 1);     //Represents y-axis

            //Create a new transformation plane defined by the given origin and two vectors
            TransformationPlane XZ_Plane = new TransformationPlane(Origin, X, Y);

            //Setting the current transformation plane to be (XZ plane)
            myModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(XZ_Plane);
            myModel.CommitChanges();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Model myModel = new Model();

            ContourPoint point = new ContourPoint(new TSG.Point(0, 0, 0), null);
            ContourPoint point2 = new ContourPoint(new TSG.Point(2000, 2000, 0), null);
            ContourPoint point3 = new ContourPoint(new TSG.Point(0, 4000, 0), null);

            PolyBeam myPolyBeam = new PolyBeam();

            myPolyBeam.AddContourPoint(point);
            myPolyBeam.AddContourPoint(point2);
            myPolyBeam.AddContourPoint(point3);

            myPolyBeam.Name = "Хомут";
            myPolyBeam.Material.MaterialString = "Ст3сп5_сортовой";
            myPolyBeam.Profile.ProfileString = "CL16_5781_82";
            myPolyBeam.PartNumber.Prefix = "Деталь";
            myPolyBeam.PartNumber.StartNumber = 1;
            myPolyBeam.AssemblyNumber.Prefix = "Сборка";
            myPolyBeam.AssemblyNumber.StartNumber = 1;
            string a = "20";
            myPolyBeam.Class = a;
            myPolyBeam.Position.Plane = TSM.Position.PlaneEnum.MIDDLE;
            myPolyBeam.Position.Depth = TSM.Position.DepthEnum.MIDDLE;
            myPolyBeam.Position.Rotation = TSM.Position.RotationEnum.TOP;
            myPolyBeam.Finish = "";

            bool Result = false;
            Result = myPolyBeam.Insert();
            myModel.CommitChanges();
        }

        private void button6_Click(object sender, EventArgs e)
        {
        Model myModel = new Model();

            //ArrayList myContourPoints2 = new ArrayList();

            // myContourPoints2.Add(myContour.CalculatePolygon);
            // contourPoints2.Add(new myCounter = );

            //string B = myModel.SelectModelObject.GetAccessibilityObjectById;
            //Assembly myAssembly = myModel.SelectModelObject as 
            //Assembly myAssembly;

            //string b = 
           // ModelObject.Get

           // Tekla.Structures.Model.Part part = myModel.SelectModelObject(Enum.);

        }

        
    }
}