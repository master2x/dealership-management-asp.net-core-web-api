using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DealershipApp.Console.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string CorreoCliente { get; set; }
        public int TelefonoCliente { get; set; }
    }
}