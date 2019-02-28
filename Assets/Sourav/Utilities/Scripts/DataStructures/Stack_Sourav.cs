using System.Collections;
using System.Collections.Generic;

namespace Sourav.Utilities.Scripts.DataStructures
{
    public class StackSourav<T> 
    {
        private List<T> stack;

        public StackSourav()
        {
            stack = new List<T>();
        }

        public void Push(T t)
        {
            stack.Add(t);
        }
        
        public T Pop()
        {
            T t = default(T);

            if(!IsStackEmpty())
            {
                t = Peek();
                
                stack.RemoveAt(stack.Count - 1);
            }

            return t;
        }
        
        public T Peek()
        {
            T t = default(T);

            if (!IsStackEmpty())
            {
                t = stack[stack.Count - 1];
            }

            return t;
        }

        public bool IsStackEmpty()
        {
            return (stack.Count <= 0);
        }

        public void ClearStack()
        {
            stack.Clear();
        }

        public bool Contains(T t)
        {
            return stack.Contains(t);
        }
    }
}
