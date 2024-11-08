using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasigno.Models.Dtos
{
    public class ResponseDto<T> where T : new()
    {
        //[JsonPropertyName("status")]
        public bool Status { get; set; }
        //[JsonPropertyName("mensaje")]
        public string Mensaje { get; set; }
        //[JsonPropertyName("data")]
        public T Data { get; set; }

        public ResponseDto(bool status, string mensaje, T data)
        {
            Mensaje = mensaje;
            Status = status;
            Data = data;
        }
        public ResponseDto(bool status, string mensaje)
        {
            Mensaje = mensaje;
            Status = status;
            Data = new();
        }
    }
}
