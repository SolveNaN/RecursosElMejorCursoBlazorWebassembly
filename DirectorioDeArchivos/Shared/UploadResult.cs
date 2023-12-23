using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioDeArchivos.Shared
{
    public class UploadResult
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
    }
}
