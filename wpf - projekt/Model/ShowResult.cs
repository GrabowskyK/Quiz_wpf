using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf___projekt.Model
{
    public class ShowResult
    {
        public int Id { get; set; }
        public string NazwaPytania { get; set; }
        public string TwojaOdp { get; set; }
        public string CorrectOdp { get; set; }

        public ShowResult(int id, string nazwaPytania, string twojaOdp, string correctOdp)
        {
            Id = id;
            NazwaPytania = nazwaPytania;
            TwojaOdp = twojaOdp;
            CorrectOdp = correctOdp;
        }
    }
}
