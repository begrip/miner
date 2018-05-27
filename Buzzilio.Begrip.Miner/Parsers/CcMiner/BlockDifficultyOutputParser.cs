using Buzzilio.Begrip.Miner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buzzilio.Begrip.Miner.Matchers
{
    public class BlockDifficultyParser : BaseOutputParser, IOutputMatcher, IDecimalOutputParser
    {
        /// <summary>
        /// BlockDifficultyMatcher implementation
        /// 
        /// ----------------------------------------------------------
        /// Sample outputs:
        /// ----------------------------------------------------------
        /// [0]         [1]       [2] [3]   [4]      [5]  [6] 
        /// [2018-03-15 22:27:47] x17 block 1956899, diff 6247.575
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Parse(string input)
        {
            try
            {
                _tokens = input.Split(' ');
                _isValid = _tokens.Length >= 7 && !string.IsNullOrEmpty(_tokens[6]);
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
        public decimal GetBlockDifficulty()
        {
            if (_tokens == null) { throw new Exception("Output was not parsed prior"); }
            var result = 0.0M;
            if (_isValid)
            {
                try
                {
                    result = decimal.Parse(_tokens[6]);
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
        public bool IsMatch(string input) { return IsMatchAnd(input, "block", "diff"); }
    }
}
