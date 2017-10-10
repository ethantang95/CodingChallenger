using CodingChallenger.Framework;
using CodingChallenger.GenericDataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.Challenges {
    /// <summary>
    /// The take away for this is a lot of front and back referencing dictionaries and use a balanced tree
    /// to keep track of the min value. This would make it work in log(n) time. Could use min heap for
    /// potentially faster performance
    /// </summary>
    [Challenge(Challenge.NotDone)]
    class MergeKLists : IChallenge<List<ChallengeLinkNode>, ChallengeLinkNode> {
        public ChallengeLinkNode ExpectedOutput() {
            var first = new ChallengeLinkNode(1);
            var output = first;
            output.next = new ChallengeLinkNode(1);
            output = output.next;
            output.next = new ChallengeLinkNode(2);
            output = output.next;
            output.next = new ChallengeLinkNode(2);
            output = output.next;
            return first;
        }

        public List<ChallengeLinkNode> Input() {
            var l1 = new ChallengeLinkNode(1);
            l1.next = new ChallengeLinkNode(2);
            var l2 = new ChallengeLinkNode(1);
            l2.next = new ChallengeLinkNode(2);
            return new List<ChallengeLinkNode>() { l1, l2 };
        }

        public ChallengeLinkNode Run(List<ChallengeLinkNode> input) {
            var indexToListDict = new Dictionary<int, ChallengeLinkNode>();
            var valueToIndicesDict = new Dictionary<int, Queue<int>>();
            var valueTree = new SortedSet<int>();
            var count = 0;
            var firstNode = new ChallengeLinkNode(-1);
            var nodePointer = firstNode;

            foreach (var list in input) {
                if (list == null) {
                    continue;
                }
                indexToListDict.Add(count, list);
                if (!valueToIndicesDict.ContainsKey(list.val)) {
                    valueToIndicesDict.Add(list.val, new Queue<int>(new List<int> { count }));
                } else {
                    valueToIndicesDict[list.val].Enqueue(count);
                }

                if (!valueTree.Contains(list.val)) {
                    valueTree.Add(list.val);
                }
                count++;
            }

            while (indexToListDict.Count > 0) {
                var min = valueTree.Min;

                //add to the result list
                nodePointer.next = new ChallengeLinkNode(min);
                nodePointer = nodePointer.next;

                //remove from the value to index dictionary
                var index = valueToIndicesDict[min].Dequeue();
                if (valueToIndicesDict[min].Count == 0) {
                    valueToIndicesDict.Remove(min);
                }

                //remove from the tree
                if (!valueToIndicesDict.ContainsKey(min)) {
                    valueTree.Remove(min);
                }

                //increment the index to list dictionary to the next value
                //if not empty, else remove it
                if (indexToListDict[index].next != null) {
                    indexToListDict[index] = indexToListDict[index].next;
                    var nextVal = indexToListDict[index].val;

                    //add the next value to the value to index dictionary
                    if (!valueToIndicesDict.ContainsKey(nextVal)) {
                        valueToIndicesDict.Add(nextVal, new Queue<int>(new List<int> { index }));
                    } else {
                        valueToIndicesDict[nextVal].Enqueue(index);
                    }

                    //add this to the tree
                    if (!valueTree.Contains(nextVal)) {
                        valueTree.Add(nextVal);
                    }

                } else {
                    indexToListDict.Remove(index);
                }
            }

            return firstNode.next;
        }
    }
}
