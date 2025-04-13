namespace SimpleEWallet.Comon.Extensions
{
	public static class Exception_Extensions
	{
		public static string AsLogText(this Exception ex)
		{
			string result = string.Empty;
			string indent = "\t";
			if (ex != null)
			{
				result = indent + "exception : ";
				indent += "\t";
				result += Environment.NewLine + indent + "Message : " + ex.Message;
				result += Environment.NewLine + indent + "StackTrace : " + ex.StackTrace;
				Exception? innerEx = ex.InnerException;
				while (innerEx != null)
				{
					result += Environment.NewLine + indent + "InnerException : ";
					indent += "\t";
					result += Environment.NewLine + indent + "Message : " + innerEx.Message;
					result += Environment.NewLine + indent + "StackTrace : " + innerEx.StackTrace;
					innerEx = innerEx.InnerException;
				}
			}

			return result;
		}
	}
}
