using System.IO;

namespace VUtils.Services.Extensions.Strings
{
	public static class StringPathValidationExt
	{

		public static bool IsFile(this string path) => File.Exists(path);

		public static bool IsDir(this string path) => Directory.Exists(path);

		public static bool Exists(this string path) => path.IsFile() || path.IsDir();

		public static bool HasExtension(this string path) => path.IsFile() && Path.HasExtension(path);

	}
}
