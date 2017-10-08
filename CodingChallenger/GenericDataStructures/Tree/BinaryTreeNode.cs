using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures {
    public class BinaryTreeNode<T> {
        public BinaryTreeNode<T> LeftChild { get; set; }
        public BinaryTreeNode<T> RightChild { get; set; }
        public BinaryTreeNode<T> Parent { get; set; }
        public BinaryTreeNode<T> LeftSibling { get; set; }
        public BinaryTreeNode<T> RightSibling { get; set; }
        public T Value { get; private set; }

        public BinaryTreeNode(T value) : this(value, null, null) { }

        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right) {
            Value = value;
            LeftChild = left;
            RightChild = right;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != GetType()) {
                return false;
            }

            var other = obj as BinaryTreeNode<T>;

            if (!Value.Equals(other.Value)) {
                return false;
            }

            if (!((Parent == null && other.Parent == null) || (Parent.Value.Equals(other.Parent.Value)))) {
                return false;
            }

            if (!((LeftSibling == null && other.LeftSibling == null) || (LeftSibling.Value.Equals(other.LeftSibling.Value)))) {
                return false;
            }

            if (!((RightSibling == null && other.RightSibling == null) || (RightSibling.Value.Equals(other.RightSibling.Value)))) {
                return false;
            }

            if ((LeftChild == null && other.LeftChild != null) || (LeftChild != null && other.LeftChild == null)) {
                return false;
            }

            if ((RightChild == null && other.RightChild != null) || (RightChild != null && other.RightChild == null)) {
                return false;
            }

            var result = true;

            if (LeftChild != null && other.LeftChild != null) {
                result &= LeftChild.Equals(other.LeftChild);
            }
            if (RightChild != null && other.RightChild != null) {
                result &= RightChild.Equals(other.RightChild);
            }

            return result;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
