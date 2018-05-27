using Buzzilio.Begrip.Miner.Interfaces;
using Buzzilio.Begrip.Miner.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Miner.Parsers
{
    public class ShareOutputParser : BaseOutputParser, IOutputMatcher, IDecimalOutputParser
    {
        /// <summary>
        /// ShareOutputParser implementation
        /// 
        /// ----------------------------------------------------------
        /// Sample outputs:
        /// ----------------------------------------------------------   
        /// [0]         [1]       [2]       [  3  ] [4]   [5]    [6]     [7]  [8]
        ///                                 [0] [1]
        /// [2018-03-16 00:15:28] accepted: 428/432 (diff 0.068), 5979.37 kH/s yes!
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Parse(string input)
        {
            try
            {
                _tokens = input.Split(' ');
                if (_tokens.Length >= 8 &&
                    !string.IsNullOrEmpty(_tokens[3]) &&
                    !string.IsNullOrEmpty(_tokens[5]) &&
                    !string.IsNullOrEmpty(_tokens[6]))
                {
                    _subTokens = _tokens[3].Split('/');
                    _isValid = _subTokens.Length > 1 &&
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
        public int GetTotalShares()
        {
            if (_tokens == null) { throw new Exception("Output was not parsed prior"); }
            var result = 0;
            if (_isValid)
            {
                try
                {
                    result = int.Parse(_subTokens[1]);
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
        /// <returns></returns>
        public int GetAcceptedShares()
        {
            if (_tokens == null) { throw new Exception("Output was not parsed prior"); }
            var result = 0;
            if (_isValid)
            {
                try
                {
                    result = int.Parse(_subTokens[0]);
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
        /// <returns></returns>
        public int GetStaleShares()
        {
            if (_tokens == null) { throw new Exception("Output was not parsed prior"); }
            var result = 0;
            if (_isValid)
            {
                try
                {
                    result = int.Parse(_subTokens[1]) - int.Parse(_subTokens[0]);
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
        /// <returns></returns>
        public decimal GetTotalHashrateInKhs()
        {
            if (_tokens == null) { throw new Exception("Output was not parsed prior"); }
            var result = 0.0M;
            if (_isValid)
            {
                try
                {
                    var unit = _tokens[7];
                    result = decimal.Parse(_tokens[6]);
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
        public bool IsMatch(string input) { return IsMatch(input, "accepted:"); }
    }
}
