using System;

namespace VUtils.Services.Extensions.Arrays
{
	public static class ArrayMergeExt
	{

		public static T[] Merge<T>(this T[] source, T[] value)
		{
			source??=Array.Empty<T>();
			value??=Array.Empty<T>();
			if(source.Length==0)
				return value;
			if(value.Length==0)
				return source;
			int start=value.Length;
			Array.Resize(ref value, source.Length+value.Length);
			Array.Copy(source, 0, value, start, source.Length);
			return value;
		}

	}
}
