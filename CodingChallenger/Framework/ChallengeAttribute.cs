using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework {
    [AttributeUsage(AttributeTargets.Class)]
    public class ChallengeAttribute : Attribute {

        public Challenge ChallengeStatus;

        public ChallengeAttribute(Challenge status) {
            ChallengeStatus = status;
        }
    }

    public enum Challenge { Done, NotDone, DoNotRun, AttributeNotUsed };
}
