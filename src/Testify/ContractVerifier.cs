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
            var verifierName = this.GetType().Name;
            var index = verifierName.IndexOf("`");
            if (index >= 0)
            {
                verifierName = verifierName.Substring(0, index);
            }

            try
            {
                this.VerifyConfiguration();
            }
            catch (Exception e)
            {
                HandleFail(verifierName, e.Message);
            }

            var tests = this.GetTests();
            AssertAll($"{verifierName} failed.", tests.ToArray());
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
