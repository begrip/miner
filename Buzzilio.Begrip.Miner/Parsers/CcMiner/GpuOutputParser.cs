using Buzzilio.Begrip.Miner.Interfaces;
using System;

namespace Buzzilio.Begrip.Miner.Matchers
{
    public class GpuOutputParser : BaseOutputParser, IOutputMatcher, IDecimalOutputParser
    {
        /// <summary>
        /// GpuOutputParser implementation
        /// 
        /// ----------------------------------------------------------
        /// Sample outputs:
        /// ----------------------------------------------------------
        /// [0]                                             [    1    ]     
        ///                                                 [0]     [1]        
        /// [2018-03-15 23:48:06] GPU #0: MSI GTX 1060 3GB, 5999.51 kH/s
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Parse(string input)
        {
            try
            {
                _tokens = input.Split(',');
                if (_tokens.Length >= 2 &&
                    !string.IsNullOrEmpty(_tokens[0]) &&
                    !string.IsNullOrEmpty(_tokens[1]))
                {
                    _subTokens = _tokens[1].Split(' ');
                    _isValid = _subTokens.Length > 0 &&
                        !string.IsNullOrEmpty(_subTokens[0]) &&
                        !string.IsNullOrEmpty(_subTokens[1]);
                }
            }
            catch
            {
                _isValid = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalHashrateInKhs()
        {
            if (_tokens == null) { throw new Exception("Output was not parsed prior"); }
            var result = 0.0M;
            if (_isValid)
            {
                try
                {
                    var unit = _subTokens[1];
                    result = decimal.Parse(_subTokens[0]);
                    if (unit == "MH/s")
                    {
                        result = result / 1000;
                    }
                }
                catch
                {
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsMatch(string input)
        {
            if (IsMatch(input, "GPU#"))
            {
                if (IsMatch(input, "kH/s") || IsMatch(input, "MH/s"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
