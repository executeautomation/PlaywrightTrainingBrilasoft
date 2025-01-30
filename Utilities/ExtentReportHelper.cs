using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace PlaywrightTestDemo.Utilities
{
    public class ExtentReportHelper : IDisposable
    {
        public ExtentReports ExtentReports { get; set; }

        public ExtentReportHelper()
        {
            ExtentReports = GetExtentReport();
        }

        private ExtentReports GetExtentReport()
        {
            var extentReport = new ExtentReports();
            extentReport.AttachReporter(new ExtentSparkReporter("TestReport.html"));
            return extentReport;
        }

        public void Dispose()
        {
            ExtentReports.Flush();
        }
    }
}
