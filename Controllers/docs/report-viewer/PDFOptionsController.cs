﻿using BoldReports.Web.ReportViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReportServices.Controllers.docs
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PDFOptionsController : ApiController, IReportController
    {
        //Post action for processing the rdl/rdlc report 
        public object PostReportAction(Dictionary<string, object> jsonResult)
        {
            return ReportHelper.ProcessReport(jsonResult, this);
        }

        //Get action for getting resources from the report
        [System.Web.Http.ActionName("GetResource")]
        [AcceptVerbs("GET")]
        public object GetResource(string key, string resourcetype, bool isPrint)
        {
            return ReportHelper.GetResource(key, resourcetype, isPrint);
        }

        //Method will be called when initialize the report options before start processing the report        
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string resourcesPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Scripts");

            reportOption.ReportModel.ExportResources.Scripts = new List<string>
            {
                resourcesPath + @"\bold-reports\common\bold.reports.common.min.js",
                resourcesPath + @"\bold-reports\common\bold.reports.widgets.min.js",
                //Chart component script
                resourcesPath + @"\bold-reports\data-visualization\ej.chart.min.js",
                //Gauge component scripts
                resourcesPath + @"\bold-reports\data-visualization\ej.lineargauge.min.js",
                resourcesPath + @"\bold-reports\data-visualization\ej.circulargauge.min.js",
                //Map component script
                resourcesPath + @"\bold-reports\data-visualization\ej.map.min.js",
                //Report Viewer Script
                resourcesPath + @"\bold-reports\bold.report-viewer.min.js"
            };

            reportOption.ReportModel.ExportResources.DependentScripts = new List<string>
            {
                resourcesPath + @"\dependent\jquery.min.js"
            };

            reportOption.ReportModel.PDFOptions = new BoldReports.Writer.PDFOptions()
            {
                EnableComplexScript = true,
                PdfConformanceLevel = Syncfusion.Pdf.PdfConformanceLevel.Pdf_A1B,

                //Load Missing font stream
                Fonts = new Dictionary<string, System.IO.Stream>
                {
                    { "Segoe UI", System.IO.File.OpenRead(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Resources/docs/font_symbols.ttf")) },
                }
            };
        }

        //Method will be called when reported is loaded
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            //You can update report options here
        }
    }
}