using System;
using System.Linq;

public class Program
{
    public void Proc() {

    }


    public class Reader {
        public static bool IsDebug = true;
        private static System.IO.StringReader SReader;
        private static string InitText = @"
        
put input text

";
        public static string ReadLine() {
            if(IsDebug) {
                if(SReader == null) {
                    SReader = new System.IO.StringReader(InitText.Trim());
                }
                return SReader.ReadLine();
            } else {
                return Console.ReadLine();
            }
        }
    }
    public static void Main(string[] args)
    {
        Program prg = new Program();
        prg.Proc();
    }
}
