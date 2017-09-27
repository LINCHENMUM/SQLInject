using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLInjectionSCAN
{
    public partial class MUMExam : Form
    {
        public MUMExam()
        {
            InitializeComponent();
            int[] Numbers1 = new int[] { 1, 2, 3, 4};
            int[] Numbers2= new int[] { 4,1, 2, 3 };
            int[] Numbers3 = new int[] { 1, 1, 2, 2 };
            int[] Numbers4 = new int[] { 1, 1 };
            int[] Numbers5 = new int[] { 1 };
            int[] Numbers6 =new int[]{};
        }
        
    }
}
