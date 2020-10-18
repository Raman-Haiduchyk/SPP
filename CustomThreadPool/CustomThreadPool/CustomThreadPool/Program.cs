using System;
using CustomThreadPool.FileService;

namespace CustomThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            int threadsCount;
            if (!(args.Length == 3))
            {
                Console.WriteLine("Wrong parameters count");
                return;
            }
            if (!int.TryParse(args[2], out threadsCount))
            {
                Console.WriteLine("Wrong threads count");
                return;
            }
            try
            {
                CopyService copyService = new CopyService(args[0], args[1], threadsCount);
                copyService.StartCopy();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }                       
        }
    }
}
