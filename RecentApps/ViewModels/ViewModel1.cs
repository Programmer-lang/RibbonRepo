using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Drawing.Printing;
using DevExpress.Xpf.Reports.UserDesigner;

namespace RecentApps.ViewModels
{
    [POCOViewModel]
    public class ViewModel1
    {


        DataModel DBContext = new DataModel();
        public virtual MenuIDHistory SelectedMenuId { get; set; }

        public ObservableCollection<MenuIDHistory> MyList { get; set; }

        const string TheUser = "Ali";
        int Id = 13;
        double AppNum = 4.0;

        public ViewModel1()
        {

            try
            {

        

                //  MyList = DBContext.MenuIDHistories.ToList();



                var MyList1 = DBContext.Database.SqlQuery<MenuIDHistory>("usp_GetData " + TheUser);
                MyList = new ObservableCollection<MenuIDHistory>(MyList1);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }
    
        public void Edit()
        {
            string data = "";
            if (SelectedMenuId != null)
            {
                data = SelectedMenuId.Note;
            }
            MessageBox.Show("Hi " + SelectedMenuId.Note);
        }

        public void New()
        {

            try
            {
                Id = 30;
                Id++;
                AppNum++;

                MenuIDHistory MyInstance = new MenuIDHistory() { Id = Id, AppNum = (Decimal)AppNum, User = TheUser, Note = AppNum.ToString() + "Notes" };
                DataModel MyContext = new DataModel();

                MyContext.Entry<MenuIDHistory>(MyInstance).State = EntityState.Added;

                MyContext.SaveChanges();

                MyList.Add(MyInstance);

                MessageBox.Show("تم إضافة الإستمارة " + AppNum.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void Print()

        {


            try
            {
                //   Cursor cur = Cursors.Wait;

                XtraReport report = new XtraReport()
                {
                    Name = "PrintMyData",
                    DisplayName = "Recent Apps",
                    PaperKind = PaperKind.Letter,
                    Margins = new Margins(100, 100, 100, 100)
                };

                ReportHeaderBand reportHeaderBand = new ReportHeaderBand()
                {
                    HeightF = 50
                };
                XRLabel titleLabel = new XRLabel()
                {
                    Text = "Current apps",
                    BoundsF = new RectangleF(0, 0, 300, 30),
                    StyleName = "Title"
                };
                reportHeaderBand.Controls.Add(titleLabel);




                DetailBand detailBand = new DetailBand()
                {
                    HeightF = 25
                };
                XRLabel detailLabel1 = new XRLabel()
                {
                    ExpressionBindings = { new ExpressionBinding("Text", "[AppNum]") },
                    BoundsF = new RectangleF(0, 0, 200, 20),
                    StyleName = "Normal"
                };

                XRLabel detailLabel2 = new XRLabel()
                {
                    ExpressionBindings = { new ExpressionBinding("Text", "[Note]") },
                    BoundsF = new RectangleF(200, 0, 300, 20),
                    StyleName = "Normal"
                };
                detailBand.Controls.Add(detailLabel1);
                detailBand.Controls.Add(detailLabel2);

                report.Bands.AddRange(new Band[] { reportHeaderBand, detailBand });

                report.DataSource = MyList;

                //   report.CreateDocument();
                //  report.ShowPreviewDialog();
                MyReport MyReport1 = new MyReport();
        
                MyReport1.ReportDesigner.OpenDocument(report);


                MyReport1.Show();

                //  report.ShowPreviewDialog();

                //   cur = Cursors.Arrow;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);

            }
        }
    }
}