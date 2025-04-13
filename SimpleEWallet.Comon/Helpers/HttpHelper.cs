using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace SimpleEWallet.Comon.Helpers
{
	public static class HttpHelper
	{
		public static async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string bearerToken, TRequest parameters, string authURL) where TResponse : new()
		{
			using HttpClient _httpClient = new();
			TResponse result = new();

			string jsonContent = JsonSerializer.Serialize(parameters);
			Console.WriteLine($"Sending JSON to API: {jsonContent}");

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

			using HttpResponseMessage response = await _httpClient.PostAsync(
				authURL,
				new StringContent(jsonContent, Encoding.UTF8, "application/json")
			);

			if (response == null)
			{
				return result;
			}

			string responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"API response failure: StatusCode = {response.StatusCode}, Content = {responseContent}");
				return result;
			}

			try
			{
				JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
				result = JsonSerializer.Deserialize<TResponse>(responseContent, options) ?? new TResponse();
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"JSON Parsing Error: {ex.Message}");
			}

			return result;
		}

		public static async Task<TResponse> PostJsonAsync<TResponse>(string bearerToken, string authURL) where TResponse : new()
		{
			using HttpClient _httpClient = new();
			TResponse result = new();

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

			using HttpResponseMessage response = await _httpClient.PostAsync(authURL, null);

			if (response == null)
			{
				return result;
			}

			string responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"API response failure: StatusCode = {response.StatusCode}, Content = {responseContent}");
				return result;
			}

			try
			{
				JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
				result = JsonSerializer.Deserialize<TResponse>(responseContent, options) ?? new TResponse();
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"JSON Parsing Error: {ex.Message}");
			}

			return result;
		}

		public static string? ExtractBearerToken(ControllerBase controllerBase)
		{
			string authorizationHeader = controllerBase.Request.Headers["Authorization"].ToString();
			return authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
				? authorizationHeader.Substring("Bearer ".Length).Trim()
				: null;
		}
	}
}
