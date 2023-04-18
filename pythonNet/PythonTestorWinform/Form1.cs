using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Python.Runtime;
using System.IO;

namespace PythonTestorWinform
{

    class PythonTestor
    {
        public PythonTestor()
        {
            Runtime.PythonDLL = @"C:\Users\jongh\Anaconda3\envs\MetaVersePythonEnv\python38.dll";
            var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"C:\Users\jongh\Anaconda3\envs\MetaVersePythonEnv");
            PythonEngine.PythonHome = PYTHON_HOME;
            PythonEngine.PythonPath = string.Join
            (
                Path.PathSeparator.ToString(),
                new string[]
                {
                  PythonEngine.PythonPath,
                  Path.Combine(PYTHON_HOME, @"Lib\site-packages"),
                  @"C:\Users\jongh\Anaconda3\envs\MetaVersePythonEnv\SJHLib"
                }
            );
            PythonEngine.Initialize();
        }

        public void Test()
        {
            using (Py.GIL())
            {
                dynamic os = Py.Import("os");
                dynamic pycwd = os.getcwd();
                string cwd = (string)pycwd;
                Console.WriteLine(cwd);
                //dynamic test = Py.Import("test");
                //dynamic f = test.Calculator(1, 2);
                //Console.WriteLine(f.Add());
            }
        }

    }


    public partial class Form1 : Form
    {
        public Form1()
        {
 
            InitializeComponent();
            PythonTestor pythonTestor = new PythonTestor();
            pythonTestor.Test();
        }
    }
}
