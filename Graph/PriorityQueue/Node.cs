namespace PriorityQueue<T>
{
    /// <typeparam name="T">Templatized Priority Queue</typeparam>
    public class Node
    {
        public T data;
        public int priority;
        public static bool operator <(Node n1, Node n2)
        {
            return (n1.priority < n2.priority); } public static bool operator >(Node n1, Node n2)
        {
            return (n1.priority > n2.priority);
        }
        public static bool operator <=(Node n1, Node n2)
        {
            return (n1.priority <= n2.priority); } public static bool operator >=(Node n1, Node n2)
        {
            return (n1.priority >= n2.priority);
        }
        public static bool operator ==(Node n1, Node n2)
        {
            return (n1.priority == n2.priority && (dynamic)n1.data == (dynamic)n2.data);
        }
        public static bool operator !=(Node n1, Node n2)
        {
            return (n1.priority != n2.priority && (dynamic)n1.data != (dynamic)n2.data);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }
}