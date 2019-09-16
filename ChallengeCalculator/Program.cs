using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ChallengeCalculator {
  class ChallengeCalculatorProgram {
    [TestMethod]
    public void StringCalculatorTest() {
      const string testInput = "5,3";
      int calcValue = StringCalculator(testInput);
      Assert.IsTrue(calcValue == 8, string.Format("{0} input should equal 8.", testInput));
    }
    public int StringCalculator(string input) {
      // Split the string by comma, traverse the IEnumerable and add the parsed numbers to a list
      IEnumerable<string> sNumbers = input.Split(',').Take(2);
      List<int> numbers = new List<int>();
      foreach (string sNumber in sNumbers) {
        int number = 0;
        int.TryParse(sNumber, out number);
        numbers.Add(number);
      }
      // Sum the numbers in the list, and print out the addition of the numbers.
      int sum = numbers.Sum();
      string printOut = string.Format("{0} = {1}", string.Join("+", numbers), sum);
      Console.WriteLine(printOut);
      return sum;
    }
    static void Main(string[] args) {
      ChallengeCalculatorProgram ccp = new ChallengeCalculatorProgram();
      while (true) {
        Console.WriteLine("Please enter a string to calculate.");
        string input = Console.ReadLine();
        ccp.StringCalculator(input);
      }
    }
  }
}
