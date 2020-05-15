using BrotliSharpLib;
using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSite.Services
{
    public class BrotliCompressionProvider : ICompressionProvider
    {
        public string EncodingName => "br";

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream) => new BrotliStream(outputStream, System.IO.Compression.CompressionMode.Compress);
    }
}
