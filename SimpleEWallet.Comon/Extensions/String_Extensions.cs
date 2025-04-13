using SimpleEWallet.Comon.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SimpleEWallet.Comon.Extensions
{
	/// <summary>
	/// Provides a set of extension methods for <see cref="string"/> that extend its functionality.
	/// </summary>
	/// <remarks>
	/// The <c>String_Extensions</c> class includes various utility methods for working with strings,
	/// such as handling null or whitespace checks, and returning default values.
	/// All methods in this class are defined as static extension methods for ease of use on <see cref="string"/> instances.
	/// </remarks>
	public static class String_Extensions
	{
		/// <summary>
		/// Indicates whether the string is <see langword="null"/> or a <see cref="string.Empty"/> ("").
		/// </summary>
		/// <param name="strval">The string to check.</param>
		/// <returns>
		/// <see langword="true"/> if the <paramref name="strval"/> parameter is <see langword="null"/> or a <see cref="string.Empty"/> (""); 
		/// otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.IsNullOrEmpty(); // returns true
		///     
		///     myStringValue = "";
		///     myStringValue.IsNullOrEmpty(); // returns true
		///     
		///     myStringValue = "any value";
		///     myStringValue.IsNullOrEmpty(); // returns false
		/// </code>
		/// </remarks>
		public static bool IsNullOrEmpty([NotNullWhen(false)] this string? strval) =>
			string.IsNullOrEmpty(strval);

		/// <summary>
		/// Indicates whether the string is neither <see langword="null"/> nor a <see cref="string.Empty"/> ("").
		/// </summary>
		/// <param name="strval">The string to check.</param>
		/// <returns>
		/// <see langword="false"/> if the <paramref name="strval"/> parameter is <see langword="null"/> or a <see cref="string.Empty"/> (""); 
		/// otherwise, <see langword="true"/>.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.IsNotNullOrEmpty(); // returns false
		///     
		///     myStringValue = "";
		///     myStringValue.IsNotNullOrEmpty(); // returns false
		///     
		///     myStringValue = "any value";
		///     myStringValue.IsNotNullOrEmpty(); // returns true
		/// </code>
		/// </remarks>	
		public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? strval) =>
			!strval.IsNullOrEmpty();

		/// <summary>
		/// Indicates whether the specified string is <see langword="null"/>, a <see cref="string.Empty"/> (""), or consists only of white-space characters.
		/// </summary>
		/// <param name="strval">The string to check.</param>
		/// <returns>
		/// <see langword="true"/> if the <paramref name="strval"/> parameter is <see langword="null"/>, a <see cref="string.Empty"/> (""), 
		/// or consists exclusively of white-space characters; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.IsNullOrWhiteSpace() // return true
		///     
		///     myStringValue = "";
		///     myStringValue.IsNullOrWhiteSpace() // return true
		///
		///     myStringValue = "   ";
		///     myStringValue.IsNullOrWhiteSpace() // return true
		///     
		///     myStringValue = "not empty";
		///     myStringValue.IsNullOrWhiteSpace() // return false
		/// </code>
		/// </remarks>
		public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? strval) =>
			string.IsNullOrWhiteSpace(strval);

		/// <summary>
		/// Indicates whether the specified string is neither <see langword="null"/>, a <see cref="string.Empty"/> (""), nor consists only of white-space characters.
		/// </summary>
		/// <param name="strval">The string to check.</param>
		/// <returns>
		/// <see langword="false"/> if the <paramref name="strval"/> parameter is <see langword="null"/>, a <see cref="string.Empty"/> (""), or consists only of white-space characters; 
		/// otherwise, <see langword="true"/>.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.IsNullOrWhiteSpace() // return false
		///     
		///     myStringValue = "";
		///     myStringValue.IsNullOrWhiteSpace() // return false
		///
		///     myStringValue = "   ";
		///     myStringValue.IsNullOrWhiteSpace() // return false
		///     
		///     myStringValue = "not empty";
		///     myStringValue.IsNullOrWhiteSpace() // return true
		/// </code>
		/// </remarks>
		public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? strval) =>
			!strval.IsNullOrWhiteSpace();

		/// <summary>
		/// Replaces a <see langword="null"/> string with a specified default value.
		/// </summary>
		/// <param name="strVal">The string to check for <see langword="null"/>.</param>
		/// <param name="defaultValue">The default value to return if the string is <see langword="null"/>.</param>
		/// <returns>
		/// The specified <paramref name="defaultValue"/> if the <paramref name="strVal"/> parameter is <see langword="null"/>; 
		/// otherwise, the original <paramref name="strVal"/> string.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.DefaultIfNull("Hello") // return "Hello"
		///     
		///     myStringValue = "";
		///     myStringValue.DefaultIfNull("Hello") // return ""
		///     
		///     myStringValue = "not empty";
		///     myStringValue.DefaultIfNull("Hello") // return "not empty"
		/// </code>
		/// </remarks>
		public static string DefaultIfNull([NotNullWhen(true)] this string? strVal, string defaultValue) =>
			strVal ?? defaultValue;

		/// <summary>
		/// Replaces a <see langword="null"/> or a <see cref="string.Empty"/> ("") with a specified default value.
		/// </summary>
		/// <param name="strval">The string to check for <see langword="null"/> or <see cref="string.Empty"/> ("").</param>
		/// <param name="defaultValue">The default value to return if the string is <see langword="null"/> or a <see cref="string.Empty"/> ("").</param>
		/// <returns>
		/// The specified <paramref name="defaultValue"/> if the <paramref name="strval"/> parameter is <see langword="null"/> 
		/// or a <see cref="string.Empty"/> (""); otherwise, the original <paramref name="strval"/> string.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.DefaultIfNullOrEmpty("Hello") // return "Hello"
		///     
		///     myStringValue = "";
		///     myStringValue.DefaultIfNullOrEmpty("Hello") // return "Hello"
		///     
		///     myStringValue = "not empty";
		///     myStringValue.DefaultIfNullOrEmpty("Hello") // return "not empty"
		/// </code>
		/// </remarks>
		public static string DefaultIfNullOrEmpty([NotNullWhen(true)] this string? strval, string defaultValue) =>
			strval.IsNullOrEmpty() ? defaultValue : strval;

		/// <summary>
		/// Replaces a <see langword="null"/> string, a <see cref="string.Empty"/> (""), or a string that consists only of white-space characters with a specified default value.
		/// </summary>
		/// <param name="strval">The string to check for <see langword="null"/>, a <see cref="string.Empty"/> (""), or white-space characters.</param>
		/// <param name="defaultValue">The default value to return if the string is <see langword="null"/>, a <see cref="string.Empty"/> (""), or consists only of white-space characters.</param>
		/// <returns>
		/// The specified <paramref name="defaultValue"/> if the <paramref name="strval"/> parameter is <see langword="null"/>, 
		/// a <see cref="string.Empty"/> (""), or consists exclusively of white-space characters; otherwise, the original <paramref name="strval"/> string.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.DefaultIfNullOrWhiteSpace("Hello") // return "Hello"
		///     
		///     myStringValue = "";
		///     myStringValue.DefaultIfNullOrWhiteSpace("Hello") // return "Hello"
		///
		///     myStringValue = "    ";
		///     myStringValue.DefaultIfNullOrWhiteSpace("Hello") // return "Hello"
		///     
		///     myStringValue = "not empty";
		///     myStringValue.DefaultIfNullOrWhiteSpace("Hello") // return "not empty"
		/// </code>
		/// </remarks>
		public static string DefaultIfNullOrWhiteSpace([NotNullWhen(true)] this string? strval, string defaultValue) =>
			strval.IsNullOrWhiteSpace() ? defaultValue : strval;

		/// <summary>
		/// Concatenates the specified string with the given array of additional strings.
		/// </summary>
		/// <param name="strval">
		/// The base string to start the concatenation with. If <paramref name="strval"/> is <see langword="null"/>, it will be treated as a <see cref="string.Empty"/> ("").
		/// </param>
		/// <param name="concatValues">
		/// An array of strings to concatenate to the <paramref name="strval"/> string. If any element in <paramref name="concatValues"/> is <see langword="null"/>,
		/// it will be treated as a <see cref="string.Empty"/> ("") during concatenation.
		/// </param>
		/// <returns>
		/// A concatenated string that starts with the original <paramref name="strval"/> value, followed by each value in <paramref name="concatValues"/>.
		/// </returns>
		/// <remarks>
		/// <para>Example:</para>
		/// <code>
		///     string myStringValue = null;
		///     myStringValue.Concat("A", "B", "C") // return "ABC"
		///     
		///     myStringValue = "";
		///     myStringValue.Concat("A", null, "C") // return "AC"
		///
		///     myStringValue = "A";
		///     myStringValue.Concat("A", "B", "C") // return "AABC"
		/// </code>
		/// </remarks>
		public static string Concat([NotNullWhen(true)] this string? strval, params string?[] concatValues)
		{
			// Create an array to hold the original string and the additional values
			string?[] concatParams = new string?[concatValues.Length + 1];
			concatParams[0] = strval;

			// Copy each value from concatValues into the new array, starting at index 1
			for (int i = 0 ; i < concatValues.Length ; i++)
			{
				concatParams[1 + i] = concatValues[i];
			}

			// Concatenate all the values into a single string
			return string.Concat(concatParams);
		}

		/// <summary>
		/// Replaces occurrences of a specified sign in the input string with a replacement string, accounting for different variations of the keyword.
		/// </summary>
		/// <param name="strval">
		/// The original string in which to replace the sign.
		/// </param>
		/// <param name="sign">
		/// The sign to be replaced.
		/// </param>
		/// <param name="replacement">
		/// The string to replace the sign with.
		/// </param>
		/// <returns>
		/// A new string where the sign have been replaced with the <paramref name="replacement"/> string.
		/// </returns>
		/// <remarks>
		/// This method is deprecated. Use <see cref="ReplaceKeyword(string, string, string)"/> instead for better functionality.
		/// </remarks>
		[Obsolete("This method is deprecated. Use ReplaceKeyword instead.")]
		public static string ReplaceSign(this string strval, string sign, string replacement)
		{
			return strval.ReplaceKeyword(sign, replacement);
		}

		/// <summary>
		/// Replaces occurrences of a specified keyword in the input string with a replacement string, accounting for different variations of the keyword.
		/// </summary>
		/// <param name="strval">
		/// The original string in which to replace the keyword.
		/// </param>
		/// <param name="keyword">
		/// The keyword to be replaced.
		/// </param>
		/// <param name="replacement">
		/// The string to replace the keyword with.
		/// </param>
		/// <returns>
		/// A new string where the keyword have been replaced with the <paramref name="replacement"/> string.
		/// </returns>
		/// <remarks>
		/// This method performs case-insensitive replacements of different formats of the keyword.
		/// <para>
		/// This method performs 4 types of replacement:
		/// <list type="bullet">
		/// <item>
		/// <description>Replaces any exact case-insensitive keyword.</description>
		/// </item>
		/// <item>
		/// <description>Replaces any case-insensitive keyword with underscores replaced by spaces.</description>
		/// </item>
		/// <item>
		/// <description>Replaces any case-insensitive keyword with spaces replaced by underscores.</description>
		/// </item>
		/// <item>
		/// <description>Replaces any case-insensitive keyword with all spaces and underscores removed.</description>
		/// </item>
		/// </list>
		/// </para>
		/// <para>
		/// Example:
		/// <code>
		///     string originalString = "Original First My key_word, Second mykeyword, Third my key word, Fourth My_Key_word ";
		///     originalString.ReplaceKeyword("my key_word", "MyVal") // return "Original First MyVal, Second MyVal, Third MyVal, Fourth MyVal"
		///     
		///     originalString = "Original First My key_word, Second mykeyword, Third my key word, Fourth My_Key_word ";
		///     originalString.ReplaceKeyword("my_key word", "MyVal") // return "Original First My key_word, Second MyVal, Third MyVal, Fourth MyVal"
		/// </code>
		/// </para>
		/// </remarks>
		public static string ReplaceKeyword(this string strval, string keyword, string replacement)
		{
			string keywordWithUnderscore = keyword.Replace(" ", "_");
			string keywordWithoutUnderscore = keyword.Replace("_", " ");
			string keywordWithoutSpace = keyword.Replace("_", "").Replace(" ", "");

			strval = strval.Replace(keyword, replacement, StringComparison.OrdinalIgnoreCase);
			strval = strval.Replace(keywordWithUnderscore, replacement, StringComparison.OrdinalIgnoreCase);
			strval = strval.Replace(keywordWithoutSpace, replacement, StringComparison.OrdinalIgnoreCase);
			strval = strval.Replace(keywordWithoutUnderscore, replacement, StringComparison.OrdinalIgnoreCase);
			return strval;
		}

		/// <summary>
		/// Adjusts a file path to match the format used by the current operating system.
		/// </summary>
		/// <param name="path">
		/// The file path to adjust. If the path is <see langword="null"/>, empty, or consists only of white-space characters, it will be returned as-is.
		/// </param>
		/// <returns>
		/// The adjusted file path formatted according to the current operating system's conventions:
		/// <list type="bullet">
		/// <item>
		/// <description>On Linux, backslashes ("\") are replaced with forward slashes ("/").</description>
		/// </item>
		/// <item>
		/// <description>On Windows, forward slashes ("/") are replaced with backslashes ("\"), and paths starting with a slash are adjusted to include the drive letter.</description>
		/// </item>
		/// <item>
		/// On other operating systems, the path is returned unchanged.
		/// </item>
		/// </list>
		/// </returns>
		/// <remarks>
		/// This method checks the current operating system and formats the path accordingly. If the path contains a drive letter (on Windows), the drive letter is preserved.
		/// <para>
		/// Example:
		/// <code>
		/// string originalPath = null;
		/// originalPath.AdjustPathToOS() // return null
		/// 
		/// originalPath = "";
		/// originalPath.AdjustPathToOS() // return ""
		/// 
		/// originalPath = "    ";
		/// originalPath.AdjustPathToOS() // return "    "
		/// 
		/// originalPath = "folder";
		/// originalPath.AdjustPathToOS() // return "folder"
		/// 
		/// originalPath = "/folder/subfolder";
		/// originalPath.AdjustPathToOS() // return "c:\folder\subfolder" on Windows; otherwise return "/folder/subfolder"
		/// 
		/// originalPath = "c:\folder\subfolder";
		/// originalPath.AdjustPathToOS() // return "/folder/subfolder" on Linux; otherwise return "c:\folder\subfolder"
		/// </code>
		/// </para>
		/// </remarks>
		public static string AdjustPathToOS(this string path)
		{
			string result = string.Empty;

			// Return the original path if it is null, empty, or consists only of white-space characters
			if (path.IsNullOrWhiteSpace())
			{
				return path;
			}

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				// Remove the drive letter if any.
				string[] splitByDrive = path.Split(":", StringSplitOptions.RemoveEmptyEntries);
				string pathWithoutDrive = splitByDrive.Length > 1 ? splitByDrive[1] : splitByDrive[0];

				// Replace any backslashes ("\") with forward slashes ("/").
				result = pathWithoutDrive.Replace("\\", "/");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				// If the path starts with a slash, prepend the drive letter from the current directory.
				result = path.Substring(0, 1) == "/" ? Path.GetPathRoot(Directory.GetCurrentDirectory()).Concat(path.Substring(1)) : path;

				// Replace any forward slashes ("/") with backslashes ("\").
				result = result.Replace("/", "\\");
			}
			else
			{
				return path;
			}
			return result;
		}

		/// <summary>
		/// Concatenates the elements of a list of strings into a single string, with an optional separator between elements.
		/// </summary>
		/// <param name="list">
		/// The list of strings to flatten. If the list is empty, an empty string is returned.
		/// </param>
		/// <param name="separator">
		/// The string to use as a separator between elements. The default value is an empty string, meaning no separator.
		/// </param>
		/// <param name="trimElement">
		/// A boolean value indicating whether to trim each element before concatenation. The default is <see langword="true"/>.
		/// </param>
		/// <returns>
		/// A single string that represents the concatenated elements of the list, separated by the specified separator.
		/// If all elements are empty or consist only of white-space characters, an empty string is returned.
		/// </returns>
		/// <remarks>
		/// This method iterates through the list of strings and concatenates non-empty elements. 
		/// If <paramref name="trimElement"/> is set to <see langword="true"/>, leading and trailing white-space characters are removed from each element before concatenation.
		/// <para>
		/// Example:
		/// <code>
		/// List&lt;string&gt; list = new List&lt;string&gt;() { "A", "B", " ", "C"," D ", "E" };
		/// list.Flatten(); // returns "ABCDE";
		/// list.Flatten("-"); // returns "A-B-C-D-E";
		/// list.Flatten("",false); // returns "AB C D E";
		/// list.Flatten("-",false); // returns "A-B- -C- D -E";
		/// </code>
		/// </para>
		/// </remarks>
		public static string Flatten(this List<string> list, string separator = "", bool trimElement = true)
		{
			string result = string.Empty;
			foreach (string element in list)
			{
				// Trim the element if trimElement is true, otherwise use the original element.
				string usedElement = trimElement ? element.Trim() : element;

				// Only process non-empty elements.
				if (usedElement.IsNotNullOrEmpty())
				{
					// If the result is not empty, add the separator before the next element.
					if (result.IsNotNullOrEmpty())
					{
						// If the separator is null, use string.Empty as separator isntead.
						result += separator.DefaultIfNull(string.Empty);
					}

					// Add the current element to the result
					result += element;
				}
			}
			return result;
		}


		/// <summary>
		/// Concatenates the elements of a list of email addresses into a single string.
		/// </summary>
		/// <param name="emailAddresses">
		/// The list of email addresses to flatten. If the list is empty, an empty string is returned.
		/// </param>
		/// <returns>
		/// A single string that represents the concatenated elements of the email addresse, separated by comma (,).
		/// If all elements are empty or consist only of white-space characters, an empty string is returned.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Example:
		/// <code>
		/// List&lt;string&gt; list = new List&lt;string&gt;() { "A@domain.com", "B@domain.com", " ", "C@domain.com"," D@domain.com ", "E@domain.com" };
		/// list.Flatten(); // returns "A@domain.com,B@domain.com,C@domain.com,D@domain.com,E@domain.com";
		/// </code>
		/// </para>
		/// </remarks>
		//public static string FlattenEmailAddress(this List<string> emailAddresses)
		//	=> emailAddresses.Flatten(Constants.Email.AddressSeparator, true);

		//public static string ConvertUrlToHyperLink(this string url)
		//{
		//	string pattern = @"(?<url>(https?|ftp)://[^\s/$.?#].[^\s]*)";

		//	// Create a regex object
		//	Regex regex = new Regex(pattern);

		//	// Match the pattern in the text
		//	MatchCollection matches = regex.Matches(url);

		//	// Check if any match is a valid URL using Uri.TryCreate
		//	bool containsValidUrl = matches.Cast<Match>().Any(match =>
		//		Uri.TryCreate(match.Groups["url"].Value, UriKind.Absolute, out Uri? uriResult) &&
		//		(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeFtp)
		//	);

		//	return containsValidUrl ? url : string.Empty;
		//}

		/// <summary>
		/// Combines the specified path with an additional path segment, using a specified separator.
		/// </summary>
		/// <param name="path">
		/// The base path to which the additional path segment will be appended. If <paramref name="path"/> is <see langword="null"/>,
		/// it will be treated as an empty string.
		/// </param>
		/// <param name="separator">
		/// The separator to use between the base path and the additional path segment. If <paramref name="separator"/> is <see langword="null"/>,
		/// it will be treated as an empty string.
		/// </param>
		/// <param name="additionalPath">
		/// The additional path segment to add to the base path. If <paramref name="additionalPath"/> is <see langword="null"/>,
		/// it will be treated as an empty string.
		/// </param>
		/// <returns>
		/// The combined path string, including the base path, separator, and additional path segment. 
		/// If the separator is not provided or the additional path segment is empty, the separator will not be added.
		/// </returns>
		/// <remarks>		
		/// This method ensures that the separator is added between the base path and the additional path segment only if needed.
		/// It also handles cases where the base path or additional path might already include the separator.
		/// <para>
		/// Example:
		/// <code>
		/// string original = "C:\\folder"
		/// original.AddPath("\\", "subfolder"); // returns "C:\\folder\\subfolder"
		/// original.AddPath("\\", "\\subfolder"); // returns "C:\\folder\\subfolder"
		/// original.AddPath("\\", "subfolder\\"); // returns "C:\\folder\\subfolder\\"
		/// 
		/// original = "C:\\folder\\"
		/// original.AddPath("\\", "subfolder"); // returns "C:\\folder\\subfolder"
		/// original.AddPath("\\", "\\subfolder"); // returns "C:\\folder\\subfolder"
		/// </code>
		/// </para>
		/// </remarks>
		public static string AddPath(this string path, string separator, string additionalPath)
		{
			// If any of the parameters are null, treat them as empty strings
			path ??= string.Empty;
			separator ??= string.Empty;
			additionalPath ??= string.Empty;

			// Start with the original path
			string result = path;

			// Check if a separator is provided
			if (separator.Length > 0)
			{
				// If the base path is not empty
				if (!path.IsNullOrEmpty())
				{
					// Add the separator if the base path does not already end with it
					if (!path.EndsWith(separator))
					{
						// Add the separator only if the additional path is not empty
						if (!additionalPath.IsNullOrEmpty())
						{
							result = path + separator;
						}
					}
				}

				// Add the additional path, ensuring no duplicate separator
				if (!additionalPath.IsNullOrEmpty())
				{
					result += additionalPath.StartsWith(separator) ? additionalPath.Substring(separator.Length) : additionalPath;
				}
			}
			// If no separator is provided, simply concatenate the paths
			else
			{
				result = path + additionalPath;
			}
			return result;
		}

		/// <summary>
		/// Determines whether the specified string is equal to any of the allowed values, using the specified comparison type.
		/// </summary>
		/// <param name="value">
		/// The string value to compare against the allowed values.
		/// </param>
		/// <param name="comparisonType">
		/// The type of string comparison to use, such as <see cref="StringComparison.OrdinalIgnoreCase"/> or <see cref="StringComparison.CurrentCulture"/>.
		/// </param>
		/// <param name="allowedValues">
		/// An array of strings representing the allowed values to compare against.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the specified <paramref name="value"/> matches any of the <paramref name="allowedValues"/> using the specified comparison type;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// This method checks whether the given string matches any value in the list of allowed values using the specified comparison type.
		/// It returns <see langword="true"/> upon finding the first match, and stops further comparison.
		/// </remarks>
		public static bool IsAmongAllowedValues(this string value, StringComparison comparisonType, params string[] allowedValues)
		{
			bool isvalid = false;

			// Iterate through the list of allowed values
			foreach (string allowedValue in allowedValues)
			{
				// Compare the current value with the allowed value using the specified comparison type
				if (value.Equals(allowedValue, comparisonType))
				{
					// If a match is found, set isValid to true and exit the loop
					isvalid = true;
					break;
				}
			}

			return isvalid;
		}

		/// <summary>
		/// Determines whether the specified string represents a valid date and time format.
		/// </summary>
		/// <param name="strval">
		/// The string to check if it can be parsed as a valid date and time.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the <paramref name="strval"/> can be parsed as a valid <see cref="DateTime"/>; 
		/// otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// This method uses <see cref="DateTime.TryParse(string, out DateTime)"/> to check if the input string can be converted to a valid <see cref="DateTime"/>.
		/// It returns <see langword="true"/> if the parsing is successful, and <see langword="false"/> otherwise.
		/// <para>
		/// Example:
		/// <code>
		/// string myString = "";
		/// myString.IsValidDateTime() // return false
		/// 
		/// myString = "2024-10-26";
		/// myString.IsValidDateTime() // return true
		/// 
		/// myString = "26/10/2024";
		/// myString.IsValidDateTime() // return true
		/// 
		/// myString = "Invalid Date";
		/// myString.IsValidDateTime() // return false
		/// </code>
		/// </para>
		/// </remarks>
		public static bool IsValidDateTime(this string strval)
		{
			DateTime dateTime;
			return DateTime.TryParse(strval, out dateTime);
		}

		/// <summary>
		/// Converts URLs in the specified string to HTML hyperlinks.
		/// </summary>
		/// <param name="url">
		/// The string that may contain one or more URLs to convert into hyperlinks.
		/// </param>
		/// <returns>
		/// The original string with any valid URLs converted into HTML anchor (<see langword="a"/>) tags.
		/// If no valid URL is found, the original string is returned.
		/// </returns>
		/// <remarks>
		/// This method uses a regular expression to find valid URLs in the string and validates them using 
		/// <see cref="Uri.TryCreate(string, UriKind, out Uri)"/>. Only URLs with HTTP, HTTPS, or FTP schemes 
		/// are considered valid.
		/// <para>
		/// Example:
		/// <code>
		/// string myString = "Visit https://example.com for details.";
		/// myString.ConvertUrlToHyperLink(); 
		/// // returns "Visit <a href=\"https://example.com\">https://example.com</a> for details."
		///
		/// myString = "This string has no valid URL.";
		/// myString.ConvertUrlToHyperLink(); 
		/// // returns "This string has no valid URL."
		/// </code>
		/// </para>
		/// </remarks>
		public static string ConvertUrlToHyperLink(this string url)
		{
			string pattern = @"(?<url>(https?|ftp)://[^\s/$.?#].[^\s]*)";

			// Create a regex object
			Regex regex = new Regex(pattern);

			// Match the pattern in the text
			MatchCollection matches = regex.Matches(url);

			// Check if any match is a valid URL using Uri.TryCreate
			bool containsValidUrl = matches.Cast<Match>().Any(match =>
				Uri.TryCreate(match.Groups["url"].Value, UriKind.Absolute, out Uri uriResult) &&
				(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeFtp)
			);

			return containsValidUrl ? url : string.Empty;
		}

		/// <summary>
		/// Generate a full name from first name and last name.
		/// </summary>
		/// <param name="firstName">The first name of the person.</param>
		/// <param name="lastName">The last name of the person.</param>
		/// <returns>The full name of the person.</returns>
		public static string AsFullName(this string firstName, string? lastName) => firstName + (lastName.IsNullOrWhiteSpace() ? string.Empty : " " + lastName);
	}
}
