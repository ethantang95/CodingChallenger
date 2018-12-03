using CodingChallenger.Framework.ChallengeExecutor;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Framework {
    class ChallengerRunner {

        public ChallengerRunner() { }

        public void Run() {
            var challengeTypes = TypesImplementingInterface(typeof(IChallenge));
            foreach (var challengeType in challengeTypes) {
                var challengeAttribute = GetChallengeAttribute(challengeType);
                if (challengeAttribute.ChallengeStatus == Challenge.NotDone) {
                    RunChallenge(challengeType);
                } else if (challengeAttribute.ChallengeStatus == Challenge.DoNotRun) {
                    Console.WriteLine($"Challenge {challengeType.Name} is set to do not run");
                }
            }
            Console.WriteLine("All challenges has finished running");
            Console.ReadLine();
        }

        private IEnumerable<Type> TypesImplementingInterface(Type desiredType) {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => DoesTypeSupportInterface(type, desiredType));
        }

        private bool DoesTypeSupportInterface(Type type, Type inter) {
            if (inter.IsAssignableFrom(type)) {
                return true;
            }
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == inter)) {
                return true;
            }
            return false;
        }

        private ChallengeAttribute GetChallengeAttribute(Type type) {
            var challengeAttribute = type.GetCustomAttributes(typeof(ChallengeAttribute), true);
            return challengeAttribute.Length > 0 ? challengeAttribute[0] as ChallengeAttribute: new ChallengeAttribute(Challenge.AttributeNotUsed);
        }

        private void RunChallenge(Type challengeType) {
            if (DoesTypeSupportInterface(challengeType, typeof(IChallengeModifyInput<,>))) {
                var executor = new ModifyInputChallengeExecutor(challengeType);
                executor.Run();
            } else {
                var executor = new SimpleChallengeExecutor(challengeType);
                executor.Run();
            }
        }
    }
}
