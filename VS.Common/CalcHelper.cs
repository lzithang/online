using NCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Common
{
    public class CalcHelper
    {
        /// <summary>
        /// 计算公式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CalcExpression(string str)
        {
            Expression expression = new Expression(str, EvaluateOptions.None);
            string result = expression.Evaluate().ToString();
            return result;
        }
    }
}
