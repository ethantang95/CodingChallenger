using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenger.GenericDataStructures.Tree {
    public abstract class BinaryTreeAbstract<T> {
        public BinaryTreeNode<T> Root { get; protected set; }
        public int Size { get; protected set; }

        public BinaryTreeAbstract(T value) {
            Root = new BinaryTreeNode<T>(value);
        }

        public void Insert(T value) {
            AddToTree(value);
            Size++;
        }

        public bool Remove(T value) {
            Size--;
            return RemoveFromTree(value);
        }

        public abstract void AddToTree(T value);

        public abstract bool RemoveFromTree(T value);

        public abstract bool CheckIfExist(T value);
    }
}
