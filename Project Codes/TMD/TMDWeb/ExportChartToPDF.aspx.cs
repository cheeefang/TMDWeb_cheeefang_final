using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.DataVisualization.Charting;

public partial class ExportChartToPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Document Doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        PdfWriter.GetInstance(Doc, Response.OutputStream);
        Doc.Open();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            Chart1.SaveImage(memoryStream, ChartImageFormat.Png);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(memoryStream.GetBuffer());
            img.ScalePercent(75f);
            Doc.Add(img);
            Doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Chart.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(Doc);
            Response.End();
        }
    }
}