using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkM3u8Downloader
{
	class ArgvOptions
	{
		[Option('f', "file", Required = true, HelpText = "Path of the input file, which is formatted by m3u8 url in each line.")]
		public string FileName { get; set; }

		[Option('p', "prefix", Required = false, HelpText = "Output Video files prefix", Default = "Data/Video - ")]
		public string FilePrefix { get; set; }

		[Option('e', "extension", Required = false, HelpText = "Output Video files extension (e.g. mp4, mkv, etc.)", Default = "mp4")]
		public string FileExtension { get; set; }
	}
}
