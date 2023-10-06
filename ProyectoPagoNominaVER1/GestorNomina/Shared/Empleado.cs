using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorNomina.Shared
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }=string.Empty;
        public string Cargo { get; set; }=string.Empty;
        public double? SalarioBase { get; set; }

        public double? Salud()
        {
            return SalarioBase*0.04;
        }

        public double? Pension()
        {
            return SalarioBase * 0.04;
        }
        public double? NetoAPagar()
        {
            return SalarioBase -Salud()-Pension();
        }

    }
}
