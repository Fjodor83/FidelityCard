using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidelityCard.Lib.Models
{
    public static class Helpers
    {
        public static Dictionary<string, char> SessoType { get; set; } = new()
        {
            { "Femmina", 'F' }, { "Maschio", 'M' }, { "Altro", 'A' }
        };
    }
}
