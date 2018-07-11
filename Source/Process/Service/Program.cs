using System;
using Service.Container;

namespace Process.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var task = new NotesProcess().RunAsync(args);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}