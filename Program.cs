using CommandLine;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BulkM3u8Downloader
{
	class Program
	{
		const string FFmpegExe = @"ffmpeg-4.3.1-2020-10-01-full_build\bin\ffmpeg.exe";
		/// <summary>
		/// Downloads the videos from the m3u8 urls list and saves to current location
		/// </summary>
		static void Main(string[] args)
		{
			Parser.Default.ParseArguments<ArgvOptions>(args).WithParsed(options =>
			{
				// cacellation thread
				new Thread(new ThreadStart(() =>
				{
					Console.WriteLine("Press q to quit and stop all running processes.");
					while (Console.ReadKey().KeyChar.ToString().ToUpper() != "Q") ;
					Environment.Exit(0);
				})).Start();
				Thread.Sleep(1500);
				var urls = File.ReadAllLines(options.FileName);
				int indx = 0;
				foreach (var url in urls)
				{
					if (!String.IsNullOrWhiteSpace(url))
					{
						DownloadVideo(url, indx++, options);
					}
				}
			}).WithNotParsed(errs =>
			{

			});
			Console.WriteLine(File.Exists(FFmpegExe));


		}
		/// <summary>
		/// Downloads a single video
		/// </summary>
		/// <param name="url">URL of m3u8 file to download from</param>
		/// <param name="indx">The current file index</param>
		private static void DownloadVideo(string url, int indx, ArgvOptions options)
		{
			if (!String.IsNullOrWhiteSpace(Path.GetDirectoryName(options.FilePrefix)) && !Directory.Exists(Path.GetDirectoryName(options.FilePrefix)))
				Directory.CreateDirectory(Path.GetDirectoryName(options.FilePrefix));

			string fname = options.FilePrefix + "" + indx + "." + options.FileExtension;
			Console.WriteLine("**** Downloading: " + url + " ---> " + fname + "****");

			Process cmd = new Process();
			cmd.StartInfo.FileName = FFmpegExe;
			cmd.StartInfo.Arguments = "-i \"" + url + "\" -c copy \"" + fname + "\"";
			cmd.StartInfo.RedirectStandardOutput = true;
			//cmd.StartInfo.CreateNoWindow = true;
			cmd.Start();

			cmd.WaitForExit();
			Console.WriteLine("**** Done: " + url + " ---> " + fname + "****");
		}
	}
}
