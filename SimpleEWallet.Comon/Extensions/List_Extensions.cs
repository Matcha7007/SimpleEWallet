namespace SimpleEWallet.Comon.Extensions
{
	public static class List_Extensions
	{
		public static List<T> AddIfNotExists<T>(this List<T> list, T element)
		{
			if (!list.Contains(element))
			{
				list.Add(element);
			}

			return list;
		}
	}
}
