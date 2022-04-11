using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace Lesson1
{
	class Post
	{
		public int userId { get; set; }

		public int id { get; set; }

		public string title { get; set; }

		public string body { get; set; }

		// Name of file to store posts
		private static string fileName;

		// Flag if this post is not first in the file, to add empty line between posts
		private static bool notFirst;

		public static void Init(string _fileName)
		{
			fileName = _fileName; notFirst = false;
			if (File.Exists(fileName)) File.Delete(fileName);
		}

		public void SaveToFile()
		{
			if (notFirst) File.AppendAllText(fileName, "\n"); else notFirst = true;
			File.AppendAllText(fileName, $"{userId}\n{id}\n{title}\n{body}\n");
		}
	}

	class Program
	{
		static readonly HttpClient client = new HttpClient();

		static async Task<Post> GetPost(int id)
		{
			HttpResponseMessage response = await client.GetAsync($"/posts/{id}");
			using var responseStream = response.Content.ReadAsStreamAsync().Result;
			Post post = JsonSerializer.DeserializeAsync<Post>(responseStream).Result;
			while (true)
			{
				try
				{
					post.SaveToFile(); break;
				}
				catch (Exception e) { }
			}
			return post;
		}

		public static async Task Main(string[] args)
		{
			int n;
			Task<Post>[] tasks = new Task<Post>[10];
			// First results are saved asynchronously and will appear in this file in random order
			Post.Init("async.txt");
			for (n = 0; n < 10; n++) tasks[n] = GetPost(n + 4);
			await Task.WhenAll(tasks);
			// Now that all results are obtained, we save them all in order
			Post.Init("sync.txt");
			for (n = 0; n < 10; n++) tasks[n].Result.SaveToFile();
		}
	}
}
