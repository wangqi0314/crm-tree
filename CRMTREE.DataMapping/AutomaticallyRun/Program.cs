using MigrationService.Helper;
using MigrationService.ImplementMethod;
using MigrationService.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticallyRun
{
    class Program
    {
        public static string customerdetailsPath = @"E:\temp\customerdetails";
        public static string jobcodePath = @"E:\temp\repairment";
        public static string appointPath = @"E:\temp\appointment";
        public static string salesPath = @"E:\temp\saleservice";

        public static string batchPath = @"E:\temp\batch";

        static void Main(string[] args)
        {
            ITxtService itx = new ImportFromSourceFile();

            string[] customerList = Directory.GetDirectories(customerdetailsPath);
            string[] jobList = Directory.GetDirectories(jobcodePath);
            string[] appointmentList = Directory.GetDirectories(appointPath);
            string[] salesList = Directory.GetDirectories(salesPath);
            string[] batchList = Directory.GetDirectories(batchPath);

            string filename = string.Empty;

            Console.WriteLine("Loading for customer detail");

            foreach (string cusPath in customerList)
            {
                string dirName = cusPath.Split('\\')[cusPath.Split('\\').Length - 1];

                CommonHelper.instance.dealerParameter = dirName.Split('_')[1].ToString();
                CommonHelper.instance.dealerActuallyName = dirName;

                Console.WriteLine("Loading for customer detail" + dirName);

                bool customerDetailflag = itx.scanFolderUpload(cusPath, out filename, cusPath + "\\UnDone", cusPath + "\\Done");

                if (customerDetailflag)
                {

                }
                else
                {
                    CommonHelper.instance.generateEmailToItStuffs(null, "The below files haven't been uploaded succussfully:" + filename, "AutomaticallyToolMessage");
                }
            }

            Console.WriteLine("customer detail has been done");

            Console.WriteLine("Loading for repairment service code");

            foreach (string jobPath in jobList)
            {
                string dirName = jobPath.Split('\\')[jobPath.Split('\\').Length - 1];

                CommonHelper.instance.dealerParameter = dirName.Split('_')[1].ToString();
                CommonHelper.instance.dealerActuallyName = dirName;

                Console.WriteLine("Loading for repairment service" + dirName);

                

                bool jobcodeflag = itx.scanFolderUpload(jobPath, out filename, jobPath + "\\UnDone", jobPath + "\\Done");

                if (jobcodeflag)
                {

                }
                else
                {
                    CommonHelper.instance.generateEmailToItStuffs(null, "The below files haven't been uploaded succussfully:" + filename, "AutomaticallyToolMessage");
                }

            }
            Console.WriteLine("Repairment service code has been done");

            Console.WriteLine("Loading for appointment code");
            
            foreach (string apptPath in appointmentList)
            {
                string dirName = apptPath.Split('\\')[apptPath.Split('\\').Length - 1];

                CommonHelper.instance.dealerParameter = dirName.Split('_')[1].ToString();
                CommonHelper.instance.dealerActuallyName = dirName;

                Console.WriteLine("Loading for appointment code" + dirName);

                bool appointflag = itx.scanFolderUpload(apptPath, out filename, apptPath + "\\UnDone", apptPath + "\\Done");

                if (appointflag)
                {


                }
                else
                {
                    CommonHelper.instance.generateEmailToItStuffs(null, "The below files haven't been uploaded succussfully:" + filename, "AutomaticallyToolMessage");
                }
            }
            Console.WriteLine("Appointment service code has been done");

            Console.WriteLine("Loading for sales service code");

            foreach (string salePath in salesList)
            {
                string dirName = salePath.Split('\\')[salePath.Split('\\').Length - 1];

                CommonHelper.instance.dealerParameter = dirName.Split('_')[1].ToString();
                CommonHelper.instance.dealerActuallyName = dirName;

                Console.WriteLine("Loading for sales code" + dirName);

                bool appointflag = itx.scanFolderUpload(salePath, out filename, salePath + "\\UnDone", salePath + "\\Done");

                if (appointflag)
                {


                }
                else
                {
                    CommonHelper.instance.generateEmailToItStuffs(null, "The below files haven't been uploaded succussfully:" + filename, "AutomaticallyToolMessage");
                }
            }
            Console.WriteLine("Sales service code has been done");

            Console.WriteLine("Loading for batchFile code");

            itx.dataMappingExtend();

            foreach (string batchpath in batchList)
            {
                string dirName = batchpath.Split('\\')[batchpath.Split('\\').Length - 1];

                CommonHelper.instance.dealerParameter = dirName.Split('_')[1].ToString();
                CommonHelper.instance.dealerActuallyName = dirName;

                Console.WriteLine("Loading for batchFile code" + dirName);

                bool batchflag = itx.dataMappingExtend(batchpath);

                if (batchflag)
                {


                }
                else
                {
                    CommonHelper.instance.generateEmailToItStuffs(null, "The below files haven't been uploaded succussfully:" + batchpath, "AutomaticallyToolMessage");
                }
            }
            Console.WriteLine("batchFile code has been done");
        }
    }
 }

