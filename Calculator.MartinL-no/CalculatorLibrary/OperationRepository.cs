using System;
using System.IO;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
	public class OperationRepository
	{
        private string _filepath;

        public OperationRepository()
		{
			_filepath = "calculatorlog.json";
			File.Create(_filepath).Close();
        }

		public void Create(OperationRecord operation)
		{
			var operations = All();
			operations.Add(operation);
			var output = new Dictionary<string, IEnumerable<OperationRecord>>();
			output["Operations"] = operations;

            File.WriteAllText(_filepath, JsonConvert.SerializeObject(output));
		}

		public List<OperationRecord> All()
		{
			var json = File.ReadAllText(_filepath);
            var operations = JsonConvert.DeserializeObject<Dictionary<string, List<OperationRecord>>>(json);

			if (operations == null) return new List<OperationRecord>();

			else return operations["Operations"];
        }

        public void DeleteAll()
        {
            File.Create(_filepath).Close();
        }
    }
}

