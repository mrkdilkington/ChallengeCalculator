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
      const string testInput = "1,2,3,4,5,6,7,8,9,10,11,12";
      int calcValue = StringCalculator(testInput);
      Assert.IsTrue(calcValue == 78, string.Format("{0} input should equal 78.", testInput));
    }
    public int StringCalculator(string input) {
      // Split the string by comma, traverse the IEnumerable and add the parsed numbers to a list
      IEnumerable<string> sNumbers = input.Split(',');
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
