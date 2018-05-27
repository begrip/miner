using Buzzilio.Begrip.Miner.Interfaces;

namespace Buzzilio.Begrip.Miner.Matchers
{
    public class BaseOutputParser
    {
        /// <summary>
        /// 
        /// </summary>
        protected string[] _tokens = null;
        protected string[] _subTokens = null;
        protected bool _isValid = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="matchToken"></param>
        /// <returns></returns>
        protected bool IsMatch(string input, string matchToken) { return input.Contains(matchToken); }

        /// <summary>
        /// 
        /// </summary>
        public void ResetParser()
        {
            _tokens = null;
            _subTokens = null;
            _isValid = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="matchTokens"></param>
        /// <returns></returns>
        protected bool IsMatchAnd(string input, params string[] matchTokens)
        {
            foreach (string token in matchTokens)
            {
                if (input.Contains(token) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="matchTokens"></param>
        /// <returns></returns>
        protected bool IsMatchOr(string input, params string[] matchTokens)
        {
            foreach (string token in matchTokens)
            {
                if (input.Contains(token) == true)
                    return true;
            }
            return false;
        }
    }
}
