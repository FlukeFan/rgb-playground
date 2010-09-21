using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace testResultComparer
{
    public class Result
    {
        public string Name { get; protected set; }
        public bool Executed { get; protected set; }
        public bool Success { get; protected set; }

        private static Result Parse(XmlNode testCase)
        {
            var result = new Result();

            result.Name = testCase.Attributes["name"].Value;
            result.Executed = bool.Parse(testCase.Attributes["executed"].Value);

            if (result.Executed)
                result.Success = bool.Parse(testCase.Attributes["success"].Value);

            return result;
        }

        public static IList<Result> ParseFile(string file)
        {
            XmlDocument run1 = new XmlDocument();
            run1.Load(file);

            var results = new List<Result>();
            foreach (XmlNode testCase in run1.SelectNodes("//test-case"))
            {
                var result = Result.Parse(testCase);
                results.Add(result);
            }
            return results;
        }
    }

	public class Program
	{
		static void Main(string[] args)
		{
            var before = Result.ParseFile(@"C:\work\mesh\NhNotes\OracleTestResult_5197.xml");
            var after = Result.ParseFile(@"C:\work\mesh\NhNotes\OracleTestResult_5198.xml");

            var beforeTestNames = before.Select(r => r.Name).ToList();
            var afterTestNames = before.Select(r => r.Name).ToList();

            var afterExistingTests = after.Where(r => beforeTestNames.Contains(r.Name));

            var fixedTests =
                afterExistingTests
                    .Where(ar => !before.Where(br => br.Name == ar.Name).Single().Success)
                    .Where(ar => ar.Success)
                    .ToList();

            if (fixedTests.Count > 0)
            {
                Console.WriteLine("*** Fixed ***");
                foreach (var result in fixedTests)
                    Console.WriteLine(result.Name);
            }

            var brokenTests =
                afterExistingTests
                    .Where(ar => before.Where(br => br.Name == ar.Name).Single().Success)
                    .Where(ar => !ar.Success)
                    .ToList();

            if (brokenTests.Count > 0)
            {
                Console.WriteLine("*** BROKEN ***");
                foreach (var result in brokenTests)
                    Console.WriteLine(result.Name);
            }
		}
	}
}
