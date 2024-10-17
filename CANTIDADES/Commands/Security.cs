using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CANTIDADES.Commands
{
    public class Security
    {
        public static string EncriptarCotraseña(string cotraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(cotraseña);
        }

        public static bool VerificacionCotraseña(string cotraseñaIngresada, string hashAlmacenado)
        {
            return BCrypt.Net.BCrypt.Verify(cotraseñaIngresada, hashAlmacenado);
        }
    }
}
