namespace Namespace
{
    public static class Module
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Evaluate("1+1"));
        }

        public static float Evaluate(string str)
        {
            return float.Parse(EvaluateHelper(str, 0, str.Length - 1));
        }

        public static string EvaluateHelper(string str, int startIndex, int endIndex)
        {
            // startIndex and endIndex is inclusive
            if (startIndex == endIndex)
            {
                return str;
            }

            var level = 0;
            var index = -1;

            // Look for +/- operators, because we want to split left/right with +/- because this way +/- will get evaluated last.
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (str[i] == '(') level += 1;
                else if (str[i] == ')') level -= 1;
                else if (level == 0 && (str[i] == '+' || str[i] == '-'))
                {
                    index = i;
                    break;
                }
            }

            // If there are no addition or subtraction operators look for multiplication or division.
            if (index == -1)
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (str[i] == '(') level += 1;
                    else if (str[i] == ')') level -= 1;
                    else if (level == 0 && (str[i] == '*' || str[i] == '/'))
                    {
                        index = i;
                        break;
                    }
                }
            }

            // If an operator was found, split into left expression and right expression and evaluate them.
            if (index != -1)
            {
                index += startIndex;

                var left = EvaluateHelper(str, startIndex, index - 1);
                var right = EvaluateHelper(str, index + 1, endIndex);

                if (str[index] == '+')
                {
                    return left + right;
                }
                else if (str[index] == '-')
                {
                    return (float.Parse(left) - float.Parse(right)).ToString();
                }
                else if (str[index] == '*')
                {
                    return (float.Parse(left) * float.Parse(right)).ToString();
                }
                else if (str[index] == '/')
                {
                    return (float.Parse(left) / float.Parse(right)).ToString();
                }
                else
                {
                    throw new InvalidOperationException("Invalid operator");
                }
            }

            // If no operator was found, evaluate the sub-expression recursively.
            if (startIndex == '(' && endIndex == ')')
            {
                return EvaluateHelper(str, startIndex + 1, endIndex - 1);
            }

            return float.Parse(str[startIndex..(endIndex + 1)]).ToString();
        }
    }
}

