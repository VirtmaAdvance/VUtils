using System.Security.AccessControl;
using VUtils.Services.FileSystem;

namespace VUtils.Services.Extensions.Strings
{
	public static class StringPathAccessExt
	{

		private static readonly PathAccess s_pathAccess = new();
		/// <summary>
		/// Determines if the given <paramref name="path"/> can be accessed.
		/// </summary>
		/// <param name="path">The path of the file or directory to check.</param>
		/// <param name="rights">The type of right(s) to check.</param>
		/// <returns>a <see cref="bool">boolean</see> value representing wheather the path can be accessed.</returns>
		public static bool IsAccessible(this string path, FileSystemRights rights) => s_pathAccess.HasAccess(path, rights);
		/// <inheritdoc cref="IsAccessible(string, FileSystemRights)"/>
		public static bool IsAccessible(this string path) => s_pathAccess.HasAccess(path);

	}
}
