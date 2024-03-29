﻿using System;
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
    [TestMethod]
    public void StringCalculatorTestStep5() {
      const string testInput = "2,1001,6";
      int calcValue = StringCalculator(testInput);
      Assert.IsTrue(calcValue == 8, "Value should equal 8. Ignore any number greater than 1000");
    }
    [TestMethod]
    public void StringCalculatorTestStep6() {
      const string testInput = "//;\n2;5";
      int calcValue = StringCalculator(testInput);
      Assert.IsTrue(calcValue == 7, "Value should equal 7.");
    }
    [TestMethod]
    public void StringCalculatorTestStep7() {
      const string testInput = "//[***]\n11***22***33";
      int calcValue = StringCalculator(testInput);
      Assert.IsTrue(calcValue == 66, "Value should equal 66.");
    }
    [TestMethod]
    public void StringCalculatorTestStep8() {
      const string testInput = "//[*][!!][r9r]\n11r9r22*33!!44";
      int calcValue = StringCalculator(testInput);
      Assert.IsTrue(calcValue == 110, "Value should equal 110.");
    }
    public int StringCalculator(string input) {
      // Split the string by comma, traverse the IEnumerable and add the parsed numbers to a list
      input = input ?? "";
      if (input.Substring(0, 2) == "//") {
        // Split on double backslash and new line, with added slash for console strings
        List<string> cInput = input.Split(new string[2] { "//", "\\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        if (cInput.Count == 2 && cInput[0].Length == 1) {
          // Join the second element with the first element as the delimiter
          input = string.Join(",", cInput[1].Split(new string[1] { cInput[0] }, StringSplitOptions.RemoveEmptyEntries));
        }
        // If the split count is two and the first element contains one of each bracket.
        else if (cInput.Count == 2 && cInput[0].Count(f => f == '[') == 1 && cInput[0].Count(f => f == ']') == 1) {
          // Use the first element, minus the brackets, as a delimiter for the second element.
          input = string.Join(",", cInput[1].Split(new string[1] { cInput[0].Trim('[', ']') }, StringSplitOptions.RemoveEmptyEntries));
        }
        // If the split count is two and the first element contains more than one of each bracket alone, and ][ but not []
        else if (cInput.Count == 2 && cInput[0].Count(f => f == '[') > 1 && cInput[0].Count(f => f == ']') > 1 &&
            cInput[0].Contains("][") && !cInput[0].Contains("[]")) {
          // Split out the delimiters and use them in the subsequent step.
          string[] delims = cInput[0].Trim('[', ']').Split(new string[1] { "][" }, StringSplitOptions.RemoveEmptyEntries);
          input = string.Join(",", cInput[1].Split(delims, StringSplitOptions.RemoveEmptyEntries));
        }
      }
      IEnumerable<string> sNumbers = input.Split(new string[2] { ",", "\\n" }, StringSplitOptions.None);
      List<int> numbers = new List<int>();
      foreach (string sNumber in sNumbers) {
        int number = 0;
        int.TryParse(sNumber, out number);
        if (number > 1000)
          continue;
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
