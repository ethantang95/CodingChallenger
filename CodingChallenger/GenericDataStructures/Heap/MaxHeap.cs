using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.Heap {
    class MaxHeap<T> where T : IComparable {

        private IList<T> _arr;

        public MaxHeap(IList<T> arr) {
            _arr = arr;
        }

        public IList<T> CreateHeap() {
            for (var i = _arr.Count / 2; i >= 0; i--) {
                Heapify(i, _arr.Count);
            }
            return _arr;
        }

        public IList<T> HeapSort() {
            var eleSorted = 0;
            while (eleSorted < _arr.Count) {
                var temp = _arr[0];
                _arr[0] = _arr[_arr.Count - eleSorted - 1];
                _arr[_arr.Count - eleSorted - 1] = temp;
                eleSorted++;
                CascadeHeapify(0, _arr.Count - eleSorted - 1);
            }
            return _arr;
        }

        private int Heapify(int index, int maxIdx) {
            if (((index * 2) + 1) >= maxIdx) {
                return -1;
            }

            var current = _arr[index];

            if (((index * 2) + 2) >= _arr.Count) {
                var child = _arr[(index * 2) + 1];
                if (current.CompareTo(child) < 0) { // checking if current is less than child
                    _arr[index] = child;
                    _arr[(index * 2) + 1] = current;
                    return (index * 2) + 1;
                }
                return -1;
            }

            var left = _arr[(index * 2) + 1];
            var right = _arr[(index * 2) + 2];

            if (current.CompareTo(left) < 0 && current.CompareTo(right) < 0) { // current is smaller than both children
                if (left.CompareTo(right) < 0) { // if left is less than right, meaning right is bigger
                    _arr[index] = right;
                    _arr[(index * 2) + 2] = current;
                    return (index * 2) + 2;
                } else {
                    _arr[index] = left;
                    _arr[(index * 2) + 1] = current;
                    return (index * 2) + 1;
                }
            } else if (current.CompareTo(left) < 0) {
                _arr[index] = left;
                _arr[(index * 2) + 1] = current;
                return (index * 2) + 1;
            } else if (current.CompareTo(right) < 0) {
                _arr[index] = right;
                _arr[(index * 2) + 2] = current;
                return (index * 2) + 2;
            } else {
                return -1;
            }
        }

        private void CascadeHeapify(int index, int maxIdx) {
            var currIdx = index;
            while (currIdx != -1) {
                currIdx = Heapify(currIdx, maxIdx);
            }
        }
    }
}
