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

            if (!myBeam.Insert())
            {
                Console.WriteLine("не добавил");
            }
            if (myBeam.Insert())
            {
                Console.WriteLine("Добавлено!");
            }
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

            string selPartProfile = "PROFILE";
            string selPartmaterial = "123";
            double selPartSTARTX = 0.0;
            double selPartSTARTY = 0.0;
            double selPartSTARTZ = 0.0;
            double selPartSTARTxINWORKPLANE = 0.0;
            double selPartSTARTyINWORKPLANE = 0.0;
            double selPartSTARTzINWORKPLANE = 0.0;
            string selPARTGUID = "";
            int selPARTID = 0;
            string selGUID = "";
            int selID = 0;


            //Tekla.Structures.Model.UI.ModelObjectSelector selected = new Tekla.Structures.Model.UI.ModelObjectSelector();
            //Tekla.Structures.Model.ModelObjectEnumerator mySelect = (selected.GetSelectedObjects() as TSM.ModelObjectEnumerator);
            //    if ((mySelect.Current as TSM.PolyBeam) != null)
            //    {
            //    TSM.PolyBeam myPolyBeam2 = mySelect.Current as TSM.PolyBeam;
            //    TSM.ModelObjectEnumerator myChildren = myPolyBeam2.GetChildren();
            //    //myChildren.Current.Identifier.GUID()
            //    }

            if (myModel.GetConnectionStatus())
            {
                ModelObjectEnumerator myObjectsInModel = myModel.GetModelObjectSelector().GetAllObjects();
                
                while (myObjectsInModel.MoveNext())
                {
                    //Beam MyBeam = myObjectsInModel.Current as Beam;
                    PolyBeam myPolyBeam = myObjectsInModel.Current as PolyBeam;

                    if (myPolyBeam != null)
                    {
                        myPolyBeam.GetReportProperty("PROFILE", ref selPartProfile); //Получить зщначение параметра PROFILE и записать в переменную selPartProfile объявленную чуть выше. 
                        myPolyBeam.GetReportProperty("PROFILE", ref selPartmaterial);
                        myPolyBeam.GetReportProperty("START_X", ref selPartSTARTX);
                        myPolyBeam.GetReportProperty("START_Y", ref selPartSTARTY);
                        myPolyBeam.GetReportProperty("START_Z", ref selPartSTARTZ);
                        myPolyBeam.GetReportProperty("PART.GUID", ref selPARTGUID);
                        myPolyBeam.GetReportProperty("PART.ID", ref selPARTID);
                        myPolyBeam.GetReportProperty("GUID", ref selGUID);
                        myPolyBeam.GetReportProperty("ID", ref selID);
                        myPolyBeam.GetReportProperty("START_X_IN_WORK_PLANE", ref selPartSTARTxINWORKPLANE);
                        myPolyBeam.GetReportProperty("START_Y_IN_WORK_PLANE", ref selPartSTARTyINWORKPLANE);
                        myPolyBeam.GetReportProperty("START_Z_IN_WORK_PLANE", ref selPartSTARTzINWORKPLANE);

                        ArrayList myPolyBeamPoints = myPolyBeam.GetPolybeamCoordinateSystems();
                        
                        //////foreach (Point point in myPolyBeamPoints)
                        //////{
                        //////    ContourPoint myPolyBeamPoint = new ContourPoint(point, new Chamfer());
                        //////    myPolyBeamPoints.Add(myPolyBeamPoint);
                        //////}
                        //_picker.PickPoints(Picker.PickPointEnum.PICK_POLYGON, "Укажи полигон");
                        //myPolyBeam.GetPolybeamCoordinateSystems()

                        label1.Text = myPolyBeamPoints.Count.ToString();
                        textBox1.Text = selPartProfile;

                        double RoundselPartSTARTX = Math.Round(selPartSTARTX, 2);
                        double RoundselPartSTARTY = Math.Round(selPartSTARTY, 2);
                        double RoundselPartSTARTZ = Math.Round(selPartSTARTZ, 2);

                        double RoundselPartSTARTxINWORKPLANE = Math.Round(selPartSTARTxINWORKPLANE, 2);
                        double RoundselPartSTARTyINWORKPLANE = Math.Round(selPartSTARTyINWORKPLANE, 2);
                        double RoundselPartSTARTzINWORKPLANE = Math.Round(selPartSTARTzINWORKPLANE, 2);



                        textBox3.AppendText("Старт ( " + RoundselPartSTARTX.ToString() + "; " + RoundselPartSTARTY.ToString() + "; " + RoundselPartSTARTZ.ToString() + " )" + Environment.NewLine);
                        textBox3.AppendText("СтартWP ( " + RoundselPartSTARTxINWORKPLANE.ToString() + "; " + RoundselPartSTARTyINWORKPLANE.ToString() + "; " + RoundselPartSTARTzINWORKPLANE.ToString() + " )" + Environment.NewLine);
                        textBox3.AppendText(selPARTGUID + Environment.NewLine + Environment.NewLine);
                        textBox3.AppendText(selPARTID + Environment.NewLine + Environment.NewLine);
                        textBox3.AppendText(selGUID + Environment.NewLine + Environment.NewLine);
                        textBox3.AppendText(selID + Environment.NewLine + Environment.NewLine);
                        textBox3.AppendText(selPartProfile + Environment.NewLine); //Параметр плюс переход на новую строку
                        textBox3.AppendText(selPartProfile + Environment.NewLine);
                        textBox3.AppendText("------------------------------" + Environment.NewLine);
                        //textBox3.AppendText(myPolyBeamPoints[0] + Environment.NewLine + myPolyBeamPoints[1]);
                    }
                    
                  //  myPolyBeam.GetReportProperty("ASSEMBLY_POS", ref AssPos);
                   // if (!myPolyBeam.GetReportProperty("ASSEMBLY_POS", ref AssPos))
                  //      Console.WriteLine("GetReportProperty failed!!!");
                   // label1.Text = AssPos;

                  //  Beam myBeam = myEnum.Current as Beam;
                  //  while (myEnum.MoveNext())
                  //  {
                  //      if(myBeam != null)
                 //        {
                 //           label1.Text = AssPos
                 //           // beam selected, add code for beam here
                         //}
                }

                //    TSM.ModelObject selectedObject = Model.SelectModelObject(partInTheDrawing.ModelIdentifier);

                    //if (myPolyBeam != null)//if (myPolyBeam != null && myPolyBeam.Name == "FOOTING") //use same name as given in exercise 1
                    //{
                    //    //Here is included also method for checking if pad footing already has rebar
                    //    //This still adds the new rebar to it, but could also skip the creation of new rebar
                    //    ModelObjectEnumerator BeamChildren = myPolyBeam.GetChildren();
                    //    //bool HasRebars = false;

                    //    while (BeamChildren.MoveNext())
                    //    {
                    //        if (BeamChildren.Current is Reinforcement)
                    //        {
                    //            //HasRebars = true;
                    //            textBox1.AppendText("This polybeam");
                    //        }
                    //    }

                    //    //if (HasRebars)
                    //    //{
                    //    //    CreateRebar(MyBeam, MyBeam.StartPoint.X, MyBeam.StartPoint.Y);
                    //    //}
                    //    //else
                    //    //{
                    //    //    CreateRebar(MyBeam, MyBeam.StartPoint.X, MyBeam.StartPoint.Y);
                    //    //}
                    //}
                //}
                myModel.CommitChanges();
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Model myModel = new Model();

            //public ModelObjectEnumerator GetSelectedObjects();

            Beam B = new Beam(new Point(0, 0, 0), new Point(0, 0, 6000));
            Beam B1 = new Beam(new Point(0, 1000, 0), new Point(0, 1000, 6000));
            Beam B2 = new Beam(new Point(0, 2000, 0), new Point(0, 2000, 6000));

            B.Insert();
            B1.Insert();
            B2.Insert();

            ArrayList ObjectsToSelect = new ArrayList();
            ObjectsToSelect.Add(B);
            ObjectsToSelect.Add(B2);

            Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS.Select(ObjectsToSelect);

            myModel.CommitChanges();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
