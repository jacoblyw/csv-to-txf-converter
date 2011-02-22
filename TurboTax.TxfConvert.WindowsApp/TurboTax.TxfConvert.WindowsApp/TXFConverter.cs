using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LumenWorks.Framework.IO.Csv;

//Uses CSV Reader from http://www.codeproject.com/KB/database/CsvReader.aspx

namespace TurboTax.TxfConvert.WindowsApp
{
    public class TXFConverter
    {
        StringBuilder _Body;

        public TXFConverter(string csvfilePath, string savePath)
        {            
            if (File.Exists(csvfilePath))
            {
                InputFile = csvfilePath;
                SavePath = savePath;
            }
        }

        

        public void Execute()
        {
            if (Directory.Exists(SavePath) && File.Exists(InputFile))
            {
                _Body = new StringBuilder();
                InitHeader();
                ParseWithIO();

                string filename = Guid.NewGuid().ToString();

                OutputFile = string.Concat(SavePath, "\\", filename, ".txf");
                File.WriteAllText(OutputFile, _Body.ToString());
            }
            else
            {
                throw new Exception("Either Save Path for directory doesn't exist or input file doesn't exist");
            }
        }

        private void InitHeader()
        {
            _Body.Append("V041\nArodwhisnant\nD ").Append(DateTime.Now.ToString("MM/dd/yyyy")).Append("\n^\n");

        }

        private void ParseWithIO()
        {
            if (File.Exists(InputFile))
            {
                string[] filecontent = File.ReadAllLines(InputFile);


                // open the file "data.csv" which is a CSV file with headers
                using (CsvReader csv =
                       new CsvReader(new StreamReader(InputFile), true))
                {
                    int fieldCount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders();
                    int i = 0;

                    while (csv.ReadNextRecord())
                    {
                        if (csv[0].IndexOf("Total:") < 0) //skips last line of csv report.
                        {
                            AppendEntryHeader(Convert.ToDateTime(csv[3]), Convert.ToDateTime(csv[6]));

                            _Body.Append(string.Concat("P", csv[2].Replace(".", ""), " ", csv[0], "\n")); // Qty + Description
                            _Body.Append(string.Concat("D", Convert.ToDateTime(csv[3]).ToString("MM/dd/yyyy"), "\n")); //Open Date
                            _Body.Append(string.Concat("D", Convert.ToDateTime(csv[6]).ToString("MM/dd/yyyy"), "\n")); //Close Date

                            string cost = csv[5].Replace(",", "").Replace("(", "").Replace(")", "");
                            if (csv[5].Replace(",", "").Replace("(", "").Replace(")", "").Length > 6)
                            {
                                cost = cost.Split('.')[0];
                            }

                            _Body.Append(string.Concat("$", cost, "\n")); //Cost Basis

                            if (string.IsNullOrEmpty(csv[8]))
                            {
                                _Body.Append("$0"); //Proceed Basis
                            }
                            else
                            {
                                string proceed = csv[8].Replace(",", "").Replace("(","").Replace(")","");

                                if (proceed.Length > 6)
                                { 
                                    proceed = proceed.Split('.')[0];
                                }

                                _Body.Append(string.Concat("$", proceed)); //Proceed Basis
                            }

                            AppendEnding();

                            i++;
                        }
                    }
                }
            }

        }

        private void AppendEntryHeader(DateTime BuyDate, DateTime SellDate)
        {
            TimeSpan days = SellDate.Subtract(BuyDate);

            if (days.Days >= 365)
            {
                //Long term trade
                _Body.Append("TD\nN323\nC1\nL1\n");
            }
            else
            {
                //Short term trade
                _Body.Append("TD\nN321\nC1\nL1\n");
            }
        }

        private void AppendEnding()
        {
            _Body.Append("\n^\n");
        }

        public string InputFile { get; set; }

        public string OutputFile { get; set; }

        public string SavePath { get; set; }
    }
}
