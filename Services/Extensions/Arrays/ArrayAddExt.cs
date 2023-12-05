using System;

namespace VUtils.Services.Extensions.Arrays
{
	public static class ArrayAddExt
	{

		public static T[] Add<T>(this T[] source, params T[] values)
		{
			source??=Array.Empty<T>();
			int startIndex=source.Length;
			int len=source.Length + values.Length;
			Array.Resize(ref source, len);
			for(int i=startIndex; i<len;i++)
				source[i]=values[i];
			return source;
		}

	}
}
