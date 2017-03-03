using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Testify.Assertions;

namespace Testify
{
    /// <summary>
    /// Base class for contract verifiers.
    /// </summary>
    public abstract class ContractVerifier
    {
        /// <summary>
        /// Verifies the contract.
        /// </summary>
        public void Verify()
        {
            var verifierName = GetType().Name;
            var index = verifierName.IndexOf("`", StringComparison.Ordinal);
            if (index >= 0)
            {
                verifierName = verifierName.Substring(0, index);
            }

            try
            {
                VerifyConfiguration();
            }
            catch (Exception e)
            {
                Throw(verifierName, e.Message, null);
            }

            var tests = GetTests();
            AssertAll($"{verifierName} failed.", tests);
        }

        /// <summary>
        /// Verifies the configuration of this <see cref="ContractVerifier"/>.
        /// </summary>
        protected virtual void VerifyConfiguration()
        {
        }

        /// <summary>
        /// Gets the collection of test methods to run when verifying the contract.
        /// </summary>
        /// <returns>A collection of test methods to run.</returns>
        protected abstract IEnumerable<Action> GetTests();
    }
}
