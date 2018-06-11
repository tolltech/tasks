using System;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
        {
            try
            {
                string input = Console.In.ReadToEnd();
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("expression", string.Empty.GetType(), input);
                System.Data.DataRow row = table.NewRow();
                table.Rows.Add(row);
                Console.WriteLine("{0}", double.Parse((string)row["expression"]));
            }
            catch
            {
                Console.WriteLine("Wrong expression");
            }
        }
	}
}
