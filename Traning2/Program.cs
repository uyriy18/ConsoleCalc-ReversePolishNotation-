using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Traning2
{


   

    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Enter the expression : ");
            while (true)
            {
                Console.WriteLine($"Result : {RPN.Calculate(Console.ReadLine())}");
            }
        }
    }
    class RPN
    {
        public static double Calculate(string input)
        {
            string checkedValue = CheckValue(input);
            string output = GetExpression(checkedValue);
            double result = Counting(output);
            return result;
        }
        static private string CheckValue(string input)
        {
            string output = string.Empty;
            try
            {
                for(int i = 0; i < input.Length; i++)
                {

                    if (IsDelimeter(input[i]) || IsOperator(input[i]) || char.IsDigit(input[i]))
                        output += input[i];
                    else continue;
                }
            }
            catch (FormatException) { }
            return output;
        }
        static private string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;
                if (char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    output += " ";
                    i--;
                }
                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operStack.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        char s = operStack.Pop();
                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";
                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }
            while (operStack.Count > 0)
            {
                output += operStack.Pop() + " ";
            }
            return output;
        }
        static private double Counting(string input)
        {
            double result = 0;
            Stack<double> temp = new Stack<double>();
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsDigit(input[i]))
                    {
                        string a = string.Empty;

                        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                        {
                            a += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                        temp.Push(double.Parse(a));
                        i--;
                    }
                    else if (IsOperator(input[i]))
                    {
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i])
                        {
                            case '+':
                                result = b + a;
                                break;
                            case '-':
                                result = b - a;
                                break;
                            case '*':
                                result = b * a;
                                break;
                            case '/':
                                result = b / a;
                                break;
                            case '^':
                                result = Math.Pow(b, a);
                                break;
                        }
                        temp.Push(result);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
                return 0;
            }
            return temp.Peek();
        }

        private static bool IsDelimeter(char c)
        {
            if (" =".IndexOf(c) != -1) return true;
            return false;
        }
        private static bool IsOperator(char c)
        {
            if ("+-*/^()".IndexOf(c) != -1) return true;
            else return false;
        }
        private static byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;  
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 6;

            }
        }
    }
    
}