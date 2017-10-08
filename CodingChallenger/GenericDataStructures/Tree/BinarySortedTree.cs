using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.Tree {
    class BinarySortedTree<T> : BinaryTreeAbstract<T> where T : IComparable<T> {
        public BinarySortedTree(T value) : base(value) {
        }

        public override void AddToTree(T value) {
            RecursiveAddToTree(Root, value);
        }

        public override bool CheckIfExist(T value) {
            throw new NotImplementedException();
        }

        public override bool RemoveFromTree(T value) {
            //check parent first
            if (value.CompareTo(Root.Value) == 0) {
                var node = RightSideMin(Root);
                if (node == null) {
                    Root = Root.LeftChild;
                    Root.Parent = null;
                    return true;
                } else {
                    var children = node.RightChild;
                    node.LeftChild = Root.LeftChild;
                    node.RightChild = Root.RightChild;
                    Root = node;
                    if (!children.Value.Equals(Root.RightChild.Value)) {
                        Root.Parent.LeftChild = children;
                        children.Parent = Root.Parent;
                    }
                    Root.Parent = null;
                }
                return true;
            }

            return RecursiveRemoveFromTree(Root, value);
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != GetType()) {
                return false;
            }

            var other = obj as BinarySortedTree<T>;

            if (Size != other.Size) {
                return false;
            }

            return Root.Equals(other.Root);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        private void RecursiveAddToTree(BinaryTreeNode<T> parent, T value) {
            var comparison = value.CompareTo(parent.Value);

            if (comparison == 0) {
                throw new ArgumentException($"The value {value} already exist in this tree");
            }

            //greater, go right
            if (comparison == 1) {
                if (parent.RightChild != null) {
                    RecursiveAddToTree(parent.RightChild, value);
                } else {
                    var node = new BinaryTreeNode<T>(value);
                    parent.RightChild = node;
                    node.Parent = parent;
                }
            }

            //less, go left
            if (comparison == -1) {
                if (parent.LeftChild != null) {
                    RecursiveAddToTree(parent.LeftChild, value);
                } else {
                    var node = new BinaryTreeNode<T>(value);
                    parent.LeftChild = node;
                    node.Parent = parent;
                }
            }
        }

        private bool RecursiveRemoveFromTree(BinaryTreeNode<T> node, T value) {
            var comparison = value.CompareTo(node.Value);

            if (comparison == 0) {
                RemoveNode(node, node.Parent.LeftChild.Value.Equals(value));
            }

            throw new NotImplementedException();
        }

        private void RemoveNode(BinaryTreeNode<T> toRemove, bool isLeftChild) {
            var parent = toRemove.Parent;
            var node = RightSideMin(toRemove);
            if (node == null) {
                Root = toRemove.LeftChild;
                Root.Parent = null;
            } else {
                var children = node.RightChild;
                node.LeftChild = Root.LeftChild;
                node.RightChild = Root.RightChild;
                Root = node;
                if (!children.Value.Equals(Root.RightChild.Value)) {
                    Root.Parent.LeftChild = children;
                    children.Parent = Root.Parent;
                }
                Root.Parent = null;
            }
        }

        private BinaryTreeNode<T> RightSideMin(BinaryTreeNode<T> node) {
            if (node.RightChild == null) {
                return null;
            }

            BinaryTreeNode<T> currPointer = node.RightChild;

            while (currPointer.LeftChild != null) {
                currPointer = currPointer.LeftChild;
            }

            return currPointer;
        }
    }
}
