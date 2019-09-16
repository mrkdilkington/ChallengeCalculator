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
      const string testInput = "-1,5";
      bool exceptionThrowOnNegative = false;
      try {
        int calcValue = StringCalculator(testInput);
      }
      catch (ArgumentException) {
        exceptionThrowOnNegative = true;
      }
      Assert.IsTrue(exceptionThrowOnNegative, "Exception should be thrown on negative number.");
    }
    public int StringCalculator(string input) {
      // Split the string by comma, traverse the IEnumerable and add the parsed numbers to a list
      IEnumerable<string> sNumbers = input.Split(new string[2] { ",", "\\n" }, StringSplitOptions.None);
      List<int> numbers = new List<int>();
      foreach (string sNumber in sNumbers) {
        int number = 0;
        int.TryParse(sNumber, out number);
        numbers.Add(number);
      }
      if (numbers.Any(n => n < 0))
        throw new ArgumentException("Negative numbers are not allowed.");
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
