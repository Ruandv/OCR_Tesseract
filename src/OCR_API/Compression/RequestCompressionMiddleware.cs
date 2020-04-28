using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace OCR_API.Compression
{
    public class RequestCompressionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        private const string HEADER_VALUE = "Content-Encoding";
        private const string GZIP_ENCODING = "GZIP";
        private const string DEFLATE_ENCODING = "DEFLATE";

        public RequestCompressionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(HEADER_VALUE))
            {
                Stream decompressor;
                if (context.Request.Headers[HEADER_VALUE] == GZIP_ENCODING)
                {
                    decompressor = new GZipStream(context.Request.Body, CompressionMode.Decompress, true);
                }
                else if (context.Request.Headers[HEADER_VALUE] == DEFLATE_ENCODING)
                {
                    decompressor = new DeflateStream(context.Request.Body, CompressionMode.Decompress, true);
                }
                else
                {
                    throw new NotSupportedException($"{HEADER_VALUE}:{context.Request.Headers[HEADER_VALUE]}");
                }

                context.Request.Body = decompressor;
            }

            await _requestDelegate(context);
        }
    }
}
