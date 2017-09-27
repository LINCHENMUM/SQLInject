using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SQL_DATAFLOW_SOURCE
    {
        public int Id { set; get; }
        public string Destinationoperand { set; get; }
        public string Opcode { set; get; }
        public string Sourceoperand { set; get; }
        public int Treelevel { set; get; }
        public int Codelineno { set; get; }
        public string Filename { set; get; }
        public string Treename { set; get; }
    }
}
