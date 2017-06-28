using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenameFiles
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string[] files = null;
            string path = null;
            Console.WriteLine("Bienvenido a \"Renombrar PDFs\".");
            Console.WriteLine("Para generar el CSV(Excel Delimitador \";\") seleccione la opción 1.");
            Console.WriteLine("Para ingresar el CSV(Excel) para renombrar seleccione la opción 2");
            Console.Write("Opción: ");
            var opcion = Console.Read();
            if (opcion == 49)
            {
                Console.WriteLine("Por favor seleccione la carpeta donde se encuentran sus PDFs a renombrar.");
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        path = fbd.SelectedPath;
                        files = Directory.GetFiles(fbd.SelectedPath, "*.pdf");
                        Console.WriteLine("Cantidad de PDFs encontrados: " + files.Length.ToString(), "Message");
                    }
                    else
                        Environment.Exit(0);
                    
                }
                if (files != null && files.Count() > 0)
                {
                    
                    StreamWriter file = new System.IO.StreamWriter(path + @"\test.csv");
                    foreach (var item in files)
                    {
                        file.WriteLine(item + ";");
                    }
                    file.Close();
                    Console.WriteLine("Archivo generado con exito. Ubicación: " + path);
                    

                }
                else
                    Console.WriteLine("No se encontraron PDFs.", "Message");
            }
            if (opcion == 50)
            {
                Console.WriteLine("Por favor seleccione el CSV.");
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = File.OpenRead(openFileDialog1.FileName))
                    using (var reader = new StreamReader(fs))
                    {
                        while (!reader.EndOfStream)
                        {

                            var line = reader.ReadLine();
                            var values = line.Split(';');
                            try
                            {
                                File.Move(values[0], values[1]);
                                Console.WriteLine("Se puedo renombrar " + values[0] + " a " + values[1] + ".");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("ERROR :: NO se puedo renombrar " + values[0] + " a " + values[1] + ".");
                            }
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
