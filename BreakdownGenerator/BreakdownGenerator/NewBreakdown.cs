using System;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace BreakdownGenerator
{
    [Cmdlet(VerbsCommon.New, "Breakdown")]
    public class NewBreakdown : PSCmdlet
    {
        [Parameter(Mandatory = true,
            Position = 0,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public string Path { get; set; }

        [Parameter(Mandatory = false)]
        public bool Read { get; set; } = false;

        protected override void ProcessRecord()
        {
            if (Read)
            {
                PrintMidToConsole(Path);
            }
            else
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("");
                SaveMid(Path, info);
            }
        }

        private void PrintMidToConsole(string path)
        {
            try
            {
                using (FileStream fs = File.Open(path, FileMode.Open))
                {
                    int b;
                    while ((b = fs.ReadByte()) != -1)
                    {
                        Console.WriteLine(b);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SaveMid(string path, Byte[] info)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream fs = File.Create(path))
                {
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
