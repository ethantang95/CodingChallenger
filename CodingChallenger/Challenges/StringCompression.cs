using CodingChallenger.Framework;
using CodingChallenger.Framework.ChallengeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// Two pointers, one for writing, the other for reading. A lot of other rules with strings
    /// </summary>
    [Challenge(Challenge.Done)]
    class StringCompression : IChallengeModifyInput<char[], int> {
        public bool AssertResult(char[] expected, char[] actual, int output) {
            for (var i = 0; i < output; i++) {
                if (expected[i] != actual[i]) {
                    return false;
                }
            }
            return true;
        }

        public char[] ExpectedModifiedInput() {
            return new char[] { 'a' };
        }

        public int ExpectedOutput() {
            return 1;
        }

        public char[] Input() {
            return new char[] { 'a' };
        }

        public int Run(char[] input) {
            var chars = input;

            var writePointer = 0;
            var readPointer = 0;
            var lastChar = (char)0;
            var charCount = 0;

            while (readPointer < chars.Length) {
                if (chars[readPointer] != lastChar) {
                    if (lastChar != (char)0) {
                        chars[writePointer] = lastChar;
                        writePointer++;
                        if (charCount > 1) {
                            writePointer = WriteCharCount(writePointer, charCount, chars);
                        }
                    }
                    lastChar = chars[readPointer];
                    charCount = 1;
                } else {
                    charCount++;
                }
                readPointer++;
            }

            // writing the last bit
            if (lastChar != (char)0) {
                chars[writePointer] = lastChar;
                writePointer++;
                if (charCount > 1) {
                    writePointer = WriteCharCount(writePointer, charCount, chars);
                }
            }

            return writePointer;
        }

        private int WriteCharCount(int writePointer, int num, char[] chars) {
            var numString = num.ToString();
            for (var i = 0; i < numString.Length; i++) {
                chars[writePointer] = numString[i];
                writePointer++;
            }

            return writePointer;
        }
    }
}
