using System;
using System.Threading.Tasks;

namespace store_car_web_project.Classes
{
    public class Logger
    {
        private const string path = @"Files/Log.txt";
        //private readonly MasterService<Empty> _service;

        public Logger()
        {
            //_service = new MasterService<Empty>();
        }

        public void Write(Exception ex, string msg)
        {
            try
            {
                string nl = Environment.NewLine;

                string header = msg + "\t" + DateTime.UtcNow + nl + nl;

                string text = ex != null ? ex.Message + nl : "";

                text += ex.InnerException != null ? ex.InnerException.Message + nl : "";

                string footer = nl + "------------------------------------" + nl + nl;

                System.IO.File.AppendAllText(path, header + text + footer);

                //_service.RunScript("INSERT INTO Logger.Error " +
                //    "VALUES (GETDATE(), '" + msg + "', '" + text + "')");
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task WriteAsync(Exception ex, string msg)
        {
            try
            {
                string nl = Environment.NewLine;

                string header = msg + "\t" + DateTime.UtcNow + nl + nl;

                string text = ex != null ? ex.Message + nl : "";

                text += ex.InnerException != null ? ex.InnerException.Message + nl : "";

                string footer = nl + "------------------------------------" + nl + nl;

                await System.IO.File.AppendAllTextAsync(path, header + text + footer);

                //_service.RunScript("INSERT INTO Logger.Error " +
                //    "VALUES (GETDATE(), '" + msg + "', '" + text + "')");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
