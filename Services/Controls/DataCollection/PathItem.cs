using System;
using System.IO;
using VUtils.Services.Extensions.Strings;

namespace VUtils.Services.Controls.DataCollection
{
	public class PathItem
	{

		private readonly string PathValue;

		public bool Selected { get; set; } = false;

		public string Name => Path.GetFileName(PathValue);

		public string Type => Path.HasExtension(PathValue) ? Path.GetExtension(PathValue).ToUpper() : "DIR";

		public long DataSize => IsFile ? PathValue.GetFileSize() : -1;

		public string Size => IsFile ? PathValue.GetFileSizeString() : "";

		public bool IsFile => PathValue.IsFile();

		public DateTime Created => IsFile ? File.GetCreationTime(PathValue) : Directory.GetCreationTime(PathValue);

		public DateTime Modified => IsFile ? File.GetLastWriteTime(PathValue) : Directory.GetLastWriteTime(PathValue);

		public DateTime Accessed => IsFile ? File.GetLastAccessTime(PathValue) : Directory.GetLastAccessTime(PathValue);


		/// <summary>
		/// Creates a new instance of the <see cref="PathItem"/> struct.
		/// </summary>
		/// <param name="path"></param>
		public PathItem(string path) => PathValue = path;

	}
}
