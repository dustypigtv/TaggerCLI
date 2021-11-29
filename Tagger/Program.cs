using CommandLine;
using libmp4.net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tagger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await Parser.Default.ParseArguments<Options>(args).WithParsedAsync(RunAsync).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        static async Task RunAsync(Options opts)
        {
            Metadata metadata = JsonConvert.DeserializeObject<Metadata>(File.ReadAllText(opts.Metadata));

            int lastMark = 0;
            IProgress<FileProgress> progress = new Progress<FileProgress>(p =>
            {
                int curMark = (int)(p.Percent * 100) % 10;
                if (curMark > lastMark)
                    Console.Write("*");
            });

            Console.WriteLine("Writing metadata: ");
            await MP4File.WriteMetadaAsync(opts.Mp4, null, metadata, true, progress).ConfigureAwait(false);
            Console.WriteLine();
            Console.WriteLine("Done");
        }
    }
}
