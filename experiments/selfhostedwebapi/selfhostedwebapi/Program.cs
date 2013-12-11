using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace selfhostedwebapi
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var config = new HttpSelfHostConfiguration("http://localhost:8111")
            {
                TransferMode = TransferMode.StreamedResponse
            };

            config.Routes.MapHttpRoute(
                "ApiDefault",
                "{controller}/");

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Trace.TraceInformation("server started");
            Console.ReadKey();
            server.CloseAsync().Wait();
            Trace.TraceInformation("server shutdown");
        }
    }

    public class MpegController : ApiController
    {
        private static IReadOnlyDictionary<string, byte[]> songs;
        private static readonly int BUFF_SIZE = 65536;

        static MpegController()
        {
            Dictionary<string, byte[]> loadSongs = new Dictionary<string, byte[]>();
            loadSongs.Add("1", GetSongsFromSubsonicLocal("http://localhost:4040/rest/download.view?u=streamer&p=streamer&v=1.1.0&c=streamer&id=160").Result);
            loadSongs.Add("2", GetSongsFromSubsonicLocal("http://localhost:4040/rest/download.view?u=streamer&p=streamer&v=1.1.0&c=streamer&id=113").Result);

            songs = loadSongs;
        }

        private static async Task<byte[]> GetSongsFromSubsonicLocal(string uri)
        {
            byte[] result;
            using (HttpClient client = new HttpClient())
            {
                result = await client.GetByteArrayAsync(uri);
            }

            return result;
        }

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var response = request.CreateResponse();

            if (!request.RequestUri.PathAndQuery.Contains("favicon.ico"))
            {
                response.Content = new PushStreamContent(Push, new MediaTypeHeaderValue("audio/mpeg"));
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        private async void Push(Stream stream, HttpContent content, TransportContext context)
        {
            try
            {
                foreach (string key in songs.Keys)
                {
                    byte[] song = songs[key];
                    int pos = 0;

                    while (pos < song.Length)
                    {
                        int next = BUFF_SIZE;
                        if (pos + next > song.Length)
                            next = song.Length - pos;

                        await stream.WriteAsync(song, pos, next);

                        pos += next;

                        Trace.WriteLine("Wrote: " + pos);
                    }

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
