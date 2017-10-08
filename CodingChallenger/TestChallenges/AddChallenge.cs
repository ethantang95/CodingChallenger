﻿using CodingChallenger.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.TestChallenges {
    [Challenge(Challenge.Done)]
    class AddChallenge : IChallenge<int[], int> {
        public int[] Input() {
            return new int[] { 1, 3 };
        }

        public int ExpectedOutput() {
            return 4;
        }

        public int Run(int[] input) {
            return input.Sum();
        }
    }
}
