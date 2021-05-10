using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarWars_HomeProject
{
    class ChainedList<T> : IEnumerator, IEnumerable
    {
        protected ListItem head;
        protected ListItem head_pointer;
        protected class ListItem
        {
            public ListItem Next;
            public T content;
        }
        protected int length = 0;
       
        public int Length
        {
            get
            {
                return length;
            }
        }

        public T this[int i]
        {
            get { return SearchItem(i); }
            set { ModItem(i, value); }
        }
        private void ModItem(int index, T newValue)
        {
            ListItem p = head;
            int count = 0;
            while (p != null && count < index)
            {
                p = p.Next;
                count++;
            }
            if (p.content != null && count == index)
            {
                p.content = newValue;
            }
            else
            {
                throw new DoesNotExistException("No item was found");
            }
        }
        public T SearchItem(int index)
        {
            int count = 0;
            ListItem pointer = head;
            while (pointer != null && count < index)
            {
                pointer = pointer.Next;
                count++;
            }
            if (pointer != null && count == index)
            {
                return pointer.content;

            }
            else throw new DoesNotExistException("The item you want to search does not exist");
        }
        
        public virtual void Remove(int index)
        {
            ReallyRemove(index);
        }
        protected void ReallyRemove(int index)
        {
            int count = 0;
            ListItem pointer = head, elem = null;
            while (pointer != null && count != index)
            {
                count++;
                elem = pointer;
                pointer = pointer.Next;
            }
            if (pointer != null)
            {
                if (elem == null) head = pointer.Next;
                else elem.Next = pointer.Next;
                length--;
            }
            else throw new DoesNotExistException("This item you want to remove doesn't exist");
        }
        public virtual void AddToList(T content, ref int wouldSpend)
        {
            ReallyAdd(content);
        }
        public void ReallyAdd(T content)
        {
            ListItem prev = null;
            ListItem ListItem = new ListItem();
            ListItem.content = content;
            ListItem.Next = null;
            if (head == null)
            {
                head = ListItem;
            }
            else
            {
                prev = head;
                while (prev.Next != null)
                {
                    prev = prev.Next;
                }
                prev.Next = ListItem;
            }
            length++;
        }
        public virtual void DeleteList()
        {
            ReallyDeleteList(ref head);
        }
        protected void ReallyDeleteList(ref ListItem head)
        {
            head = null; // funny but garbage collector will collect anything that is not referenced
            length = 0;
        }

        public object Current
        {
            get { return head_pointer.content; } // i need this so i don't override the head.
        }
        public bool MoveNext()
        {
            if (head_pointer == null)
            {
                head_pointer = head;
                return true;
            }
            else if (head_pointer.Next != null)
            {
                head_pointer = head_pointer.Next;
                return true;
            }
            else
            {
                Reset(); // this will null the head pointer, that's why we need the other variable.
                return false;
            }
        }

        public void Reset()
        {
            head_pointer = null;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
