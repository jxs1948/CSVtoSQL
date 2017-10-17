using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvPath = @"C:\Users\admin\Documents\Visual Studio 2013\Projects\ConvertCSVtoSQL_CSharpApplication\ConvertCSVtoSQL_CSharpApplication\Files\FinalTest.csv";
            StreamReader sr = new StreamReader(csvPath);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[71] { 
            new DataColumn("File Name", typeof(string)),
            new DataColumn("Full File Name", typeof(string)),
            new DataColumn("File Name w/o Extension", typeof(string)),
            new DataColumn("Friendly Folder Name", typeof(string)),
            new DataColumn("File URI", typeof(string)),
            new DataColumn("Folder Name", typeof(string)),
            new DataColumn("Folder URI", typeof(string)),
            new DataColumn("Width", typeof(string)),
            new DataColumn("Height", typeof(string)), 
            new DataColumn("Size", typeof(string)),
            new DataColumn("File Format", typeof(string)),
            new DataColumn("Checksum", typeof(string)),
            new DataColumn("Last Modified", typeof(string)),
            new DataColumn("Last Updated in Database", typeof(string)),
            new DataColumn("BPP", typeof(string)),
            new DataColumn("Width and Height", typeof(string)),
            new DataColumn("Image Object Identifier (OID)", typeof(string)),
            new DataColumn("Media ID", typeof(string)),
            new DataColumn("Rating", typeof(string)),
            new DataColumn("Label", typeof(string)),
            new DataColumn("EXIF Date", typeof(string)),
            new DataColumn("EXIF Time", typeof(string)),
            new DataColumn("Current_ID", typeof(string)),
            new DataColumn("Sorting_ID", typeof(string)),
            new DataColumn("Sighting_Date", typeof(string)),
            new DataColumn("Adate", typeof(string)),            
            new DataColumn("Area_Code", typeof(string)),
            new DataColumn("Location", typeof(string)),
            new DataColumn("Photographer", typeof(string)),
            new DataColumn("Searching_Comments", typeof(string)),
            new DataColumn("General_Comments", typeof(string)),
            new DataColumn("Animal_Reference", typeof(string)),
            new DataColumn("Size_Class", typeof(string)),
            new DataColumn("Reproductive_Code", typeof(string)),
            new DataColumn("Verifier_1", typeof(string)),
            new DataColumn("Date_Verifier1", typeof(string)),
            new DataColumn("Verifier_2", typeof(string)),
            new DataColumn("Date_Verifier2", typeof(string)),
            new DataColumn("Verifier_3", typeof(string)),
            new DataColumn("Date_Verifier3", typeof(string)),
            new DataColumn("Catalog_ID", typeof(string)),
            new DataColumn("Rescue_ID", typeof(string)),
            new DataColumn("Facility_ID", typeof(string)),
            new DataColumn("Capture_ID", typeof(string)),
            new DataColumn("Tag_ID", typeof(string)),
            new DataColumn("Mortality_ID", typeof(string)),
            new DataColumn("Length", typeof(string)),
            new DataColumn("Measurement_Method", typeof(string)),
            new DataColumn("DVD_Identifier", typeof(string)),
            new DataColumn("Head_Direction", typeof(string)),
            new DataColumn("Collection_Agency", typeof(string)),													
            new DataColumn("Sex", typeof(string)),
            new DataColumn("Calf_Size", typeof(string)),
            new DataColumn("Calf_ID", typeof(string)),
            new DataColumn("Calf_Sex", typeof(string)),
            new DataColumn("Genetic_Field_ID", typeof(string)),
            new DataColumn("Calf_AgeObs", typeof(string)),
            new DataColumn("Calf_AgeF", typeof(string)),
            new DataColumn("Calf_Length", typeof(string)),
            new DataColumn("Calf_Measurement_Method", typeof(string)),
            new DataColumn("Cookied_Adult", typeof(string)),
            new DataColumn("Cookied_Calf", typeof(string)),
            new DataColumn("CAC1_Birthdate", typeof(string)),
            new DataColumn("CAC2_Reprod_History", typeof(string)),
            new DataColumn("CAC3_Features", typeof(string)),
            new DataColumn("CAC4_Size", typeof(string)),
            new DataColumn("CAC5_Obs_Confidence", typeof(string)),
            new DataColumn("Sex_Determination_Code", typeof(string)),
            new DataColumn("Date_of_MIPS_transfer", typeof(string)),
            new DataColumn("Cold_Lesions", typeof(string)),
            new DataColumn("Calf_Cold_Lesions", typeof(string))
            
            });
                /*    These four categories are not read as they contain comma in their va;ue which is leading to exception
                new DataColumn("Categories", typeof(string))
                new DataColumn("Categories (fully qualified)", typeof(string)),
                new DataColumn("Categories (incl. indirect)", typeof(string)),
                new DataColumn("Categories (incl. indirect, fully qualified)",typeof(string)),
                */
            string csvData = File.ReadAllText(csvPath);
            Boolean headerRowHasBeenSkipped = false;
            foreach (string row in csvData.Split('\n'))
            {
                if (headerRowHasBeenSkipped)
                {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    var i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
              }
                headerRowHasBeenSkipped = true;
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=HPCLAB1\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True"))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.iMATCH";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }

        }
    }
}
