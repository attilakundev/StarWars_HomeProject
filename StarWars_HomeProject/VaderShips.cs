using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    class VaderShips<T,K> :IEnumerable where K: IComparable
    {
        public event ListDisplay vaderEvent;
        private int length = 0;
        public int Length { get { return length; } }
        public enum Traversal {PreOrder,InOrder,PostOrder}
        class TreeItem
        {
            public T data;
            public K key;
            public TreeItem left, right;
            public TreeItem(T data, K key)
            {
                this.key = key; this.data = data;
            }
        }
        //This binary search tree will contain the ships that is owned by Vader
        private TreeItem root;
        public Traversal traversal;
        public IEnumerable TraverseNodes // i create a linked list in where I store the nodes, so I can return them to the foreach
        {

            get {
                ChainedList<T> TraverseNodes = new ChainedList<T>();
                switch (traversal)// this gets the traversal method, which I predefined in the Logic class which I'm gonna run
                {
                    case Traversal.PreOrder: PreOrder(TraverseNodes, root); break;
                    case Traversal.InOrder: InOrder(TraverseNodes, root); break;
                    case Traversal.PostOrder: PostOrder(TraverseNodes, root); break;
                }
                return TraverseNodes;
            }
        }
        public void InsertShip(K key, T data)
        {
            TreeItem newNode = new TreeItem(data, key);
            RecInsertShip(ref root, newNode);
        }
        private void RecInsertShip(ref TreeItem root, TreeItem newNode)
        {

            if (root == null)
            {
                root = newNode;
                length++;
            }
            else if (root.key.CompareTo(newNode.key) > 0)
            {
                // we insert into the left branch
                RecInsertShip(ref root.left, newNode);
            }
            else if (root.key.CompareTo(newNode.key) < 0)
            {
                // we insert into the right branch
                RecInsertShip(ref root.right, newNode);
            }
            else
            {
                throw new AlreadyExistsException("This ship cost already exists in the tree");
            }
        } // The key will be the cost of the ship, i have to take this in mind
        public void TraversalList() // This will be called when Vader buys ships
        {
            if (length==0)
            {
                throw new DoesNotExistException("There's no ships in Vader's fleet"); // since it's a generic list, it could throw a more generic error, but let it be just this, ok? :)
            }
            traversal = Traversal.InOrder;
            int tmp = 0;
            foreach(T item in this) // Referring here to the IEnumerator stuff
            {
                vaderEvent?.Invoke((item as Spaceship), tmp);
                tmp++;
            }
        }
        void PreOrder(ChainedList<T> list, TreeItem pointer)
        {
            if (pointer != null)
            {
                list.ReallyAdd(pointer.data);
                PreOrder(list,pointer.left);
                PreOrder(list,pointer.right);
            }
        }
        void InOrder(ChainedList<T> list, TreeItem pointer)
        {
            if (pointer != null)
            {
                InOrder(list, pointer.left);
                list.ReallyAdd(pointer.data);
                InOrder(list, pointer.right);
            }
        }
        void PostOrder(ChainedList<T> list, TreeItem pointer)
        {
            if (pointer != null)
            {
                PostOrder(list, pointer.left);
                PostOrder(list, pointer.right);
                list.ReallyAdd(pointer.data);
            }
        }
        public void Remove(K key) // to be honest, we don't delete ships from Vader's ship, just i wanted to demonstrate that i can do this :D
        {
            Remove(ref root, key); // I need this because C# doesn't like when private classes are used and thus their visibility is not that great...
        }
        private void Remove(ref TreeItem pointer, K key)
        {
            if (pointer != null)
            {
                if (pointer.key.CompareTo(key) > 0)
                {
                    Remove(ref pointer.left, key);
                }
                else if (pointer.key.CompareTo(key) < 0)
                {
                    Remove(ref pointer.right, key);
                }
                else if(pointer.left == null)
                {
                    pointer = pointer.right;
                    length--;
                }
                else if (pointer.right == null)
                {
                    pointer = pointer.left;
                    length--;
                }
                else
                {
                    RemoveTwoChild(pointer,ref pointer.left);
                }
            }
            else
            {
                throw new DoesNotExistException("This ship does not exist in Vader's fleet.");
            }
        }
        void RemoveTwoChild(TreeItem item, ref TreeItem right)
        {
            if (item != null)
            {
                RemoveTwoChild(item, ref right.right);
            }
            else
            {
                item.data = right.data;
                item.key = right.key;
                right = right.left;
                length--;
                TraversalList();
            }
        }
        private T Search(TreeItem item, K key)
        {
            if (item != null)
            {
                if (item.key.CompareTo(key) > 0)
                {
                    return Search(item.left, key);
                }
                else if (item.key.CompareTo(key) < 0)
                {
                    return Search(item.right, key);
                }
                else
                {
                    return item.data;
                }
            }
            else
            {
                throw new DoesNotExistException("This ship does not exist in Vader's collection");
            }
        }

        public IEnumerator GetEnumerator() // I need this to use foreach to traverse the list.
        {
            return TraverseNodes.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return TraverseNodes.GetEnumerator();
        }
        public VaderShips()
        {
            vaderEvent += ConsoleOutput.DisplayShips;
        }
    }
}
