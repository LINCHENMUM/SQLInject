using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SQL_SCANRESULT
    {
        public int Id { set; get; }
        public string Sqlinjectsource { set; get; }
        public int Codelineno { set; get; }
        public string Filename { set; get; }
        //public DateTime Scandate { set; get; }

        public string Scanresult { set; get; }
        public string Dataflow { set; get; }
        public int Batch { set; get; }
    }
}
