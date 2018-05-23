using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Schotland.Models
{
    public partial class Places
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public byte[] Picture { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

    }

}
