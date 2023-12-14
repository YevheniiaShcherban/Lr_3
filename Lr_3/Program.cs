using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lr_3
{
    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Об’єднати два відсортовані списки
            ListNode list1 = new ListNode(1, new ListNode(2, new ListNode(4)));
            ListNode list2 = new ListNode(1, new ListNode(3, new ListNode(4)));
            Console.WriteLine($"Task 1\nList1:");
            PrintList(list1);
            Console.WriteLine($"\nList2:");
            PrintList(list2);
            Console.WriteLine($"\nResult");
            MergeTwoLists(list1, list2);
            PrintList(list1);
            //Видалити дублікати з відсортованого списку
            ListNode list = new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(3)))));
            Console.WriteLine($"\nTask 2\nList:");
            PrintList(list);
            DeleteDuplicates(list);
            Console.WriteLine($"\nResult");
            PrintList(list);
            //Цикл пов’язаного списку
            list = new ListNode(3, new ListNode(2, new ListNode(0, new ListNode(-4, list))));
            Console.WriteLine($"\nTask 3\nList:");
            PrintList(list);
            bool hasCycle = HasCycle(list, 1);
            Console.WriteLine($"\nResult: {hasCycle}");
            //Перевпорядкувати список
            list = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            Console.WriteLine($"\nTask 4\nList:");
            PrintList(list);
            ReverseList(list);
            Console.WriteLine($"\nResult");
            PrintList(list);
            //Видалити вузол у зв’язаному списку
            list = new ListNode(4, new ListNode(5, new ListNode(1, new ListNode(9))));
            Console.WriteLine($"\nTask 5\nList:");
            PrintList(list);
            ListNode node = list.next.next;
            DeleteNode(node);
            Console.WriteLine($"\nResult");
            PrintList(list);
            //Подвоїти число, представлене у вигляді зв’язаного списку
            list = new ListNode(8, new ListNode(8, new ListNode(9)));
            ListNode Copy_list = CopyList(list);
            Console.WriteLine($"\nTask 6\nList:");
            PrintList(list);
            if (!Check(list, Copy_list)) 
            {
                DoubleList(list); 
            }
            else
            {
                list = ReverseAllList(list);
                DoubleListDifficult(list);
                list = ReverseAllList(list);
            }
            Console.WriteLine($"\nResult");
            PrintList(list);
            //Об’єднати k відсортованих списків
            ListNode[] lists = new ListNode[3];
            lists[0] = new ListNode(1, new ListNode(4, new ListNode(5)));
            lists[1] = new ListNode(1, new ListNode(3, new ListNode(4)));
            lists[2] = new ListNode(2, new ListNode(6));
            Console.WriteLine($"\nTask 7");
            for (int i = 0; i < lists.Length; i++)
            {
                Console.WriteLine($"\nList{i+1}: ");
                PrintList(lists[i]);
            }
            ListNode result = MergeKLists(lists);
            Console.WriteLine($"\nResult");
            PrintList(result);
            //Розвернути вузли в k-групі
            list = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            int k = 3;
            Console.WriteLine($"\nTask 8\nK = {k}\nList:");
            PrintList(list);
            list = ReverseKGroup(list, k);
            Console.WriteLine($"\nResult");
            PrintList(list);
            //Розділити список
            list = new ListNode(1, new ListNode(4, new ListNode(3, new ListNode(2, new ListNode(5, new ListNode(2))))));
            int x = 3;
            Console.WriteLine($"\nTask 9\nX = {x}\nList:");
            PrintList(list);
            SplitList(list, x);
            Console.WriteLine($"\nResult");
            PrintList(list);
        }
        static void PrintList(ListNode head)
        {
            while (head != null)
            {
                Console.Write(head.val + " ");
                head = head.next;
            }
        }
        //Об’єднати два відсортовані списки
        static ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null) { return list2; }
            if (list2 == null) { return list1; }
            if (list1.val <= list2.val)
            {
                list1.next = MergeTwoLists(list1.next, list2);
                return list1;
            }
            else
            {
                list2.next = MergeTwoLists(list1, list2.next);
                return list2;
            }
        }
        //Видалити дублікати з відсортованого списку
        static ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null) { return null; }
            if (head.next == null) { return head; }
            ListNode prev = head;
            for (ListNode node = head.next; node != null; node = node.next)
            {
                if (node.val == prev.val) { prev.next = node.next; }
                else { prev = node; }
            }
            return head;
        }
        //Цикл пов’язаного списку
        static bool HasCycle(ListNode head, int pos)
        {
            ListNode slow = head;
            ListNode fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (slow == fast) { return true; }
            }
            return false;
        }
        //Перевпорядкувати список
        static void ReverseList(ListNode head)
        {
            var slow = head;
            var fast = head.next;
            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next?.next;
            }
            ListNode reverse = null;
            while (slow != null)
            {
                var next = slow.next;
                slow.next = reverse;
                reverse = slow;
                slow = next;
            }
            var lead = head;
            var tail = reverse;
            while (tail.next != null)
            {
                var leadNext = lead.next;
                var tailNext = tail.next;
                lead.next = tail;
                tail.next = leadNext;
                lead = leadNext;
                tail = tailNext;
            }
        }
        //Видалити вузол у зв’язаному списку
        static void DeleteNode(ListNode node)
        {
            node.val = node.next.val;
            node.next = node.next.next;
        }
        //Подвоїти число, представлене у вигляді зв’язаного списку
        static bool Check(ListNode list, ListNode Copy_list)
        {
            int originalCount = CountListLength(list);
            int modifiedCount = CombineAndDouble(Copy_list);
            return originalCount != modifiedCount;
        }
        static int CountListLength(ListNode head)
        {
            int count = 0;
            while (head != null)
            {
                count++;
                head = head.next;
            }
            return count;
        }
        static int CombineAndDouble(ListNode head)
        {
            int value = 0;
            int multiplier = 1;
            head = ReverseAllList(head);
            while (head != null)
            {
                value += head.val * multiplier;
                multiplier *= 10;
                head = head.next;
            }
            int count = 0;
            value *= 2;
            while (value > 0)
            {
                value /= 10;
                count++;
            }
            return count;
        }
        static ListNode DoubleListDifficult(ListNode head)
        {
            if (head == null) return null;
            int carry = 0;
            ListNode curr = head;
            ListNode prev = null;
            while (curr != null)
            {
                int sum = curr.val * 2 + carry;
                carry = sum / 10;
                curr.val = sum % 10;
                prev = curr;
                curr = curr.next;
            }
            if (carry == 1) { prev.next = new ListNode(carry); }
            return head;
        }
        static ListNode DoubleList(ListNode head)
        {
            var rem = RecursiveforDoubleList(head);
            if (rem > 0) return new ListNode(rem, head);
            return head;
        }
        static int RecursiveforDoubleList(ListNode head)
        {
            if (head == null) { return -1; }
            var rem = RecursiveforDoubleList(head.next);
            var x = head.val * 2 + (rem > 0 ? rem : 0);
            head.val = x % 10;
            return x / 10;
        }
        static ListNode ReverseAllList(ListNode head)
        {
            if (head == null) { return head; }
            if (head.next == null) { return head; }
            ListNode current = head;
            List<ListNode> list = new List<ListNode>();
            list.Add(current);
            while (current.next != null)
            {
                current = current.next;
                list.Add(current);
            }
            for (int i = list.Count - 1; i > 0; i--)
            {
                list[i].next = list[i - 1];
            }
            list[0].next = null;
            return list[list.Count - 1];
        }
        static ListNode CopyList(ListNode head)
        {
            if (head == null) { return null; }
            ListNode newHead = new ListNode(head.val);
            ListNode current = newHead;
            while (head.next != null)
            {
                head = head.next;
                current.next = new ListNode(head.val);
                current = current.next;
            }
            return newHead;
        }
        //Об’єднати k відсортованих списків
        static ListNode MergeKLists(ListNode[] lists)
        {
            if (lists.Length == 0) { return null; }
            var newNode = new ListNode();
            var ansNode = newNode;
            var data = new List<int>();
            foreach (var node in lists)
            {
                var tempNode = node;
                while (tempNode != null)
                {
                    data.Add(tempNode.val);
                    tempNode = tempNode.next;
                }
            }
            if (data.Count == 0) { return null; }
            data.Sort();
            for (int i = 0; i < data.Count; i++)
            {
                newNode.val = data[i];
                if (i != data.Count - 1) { newNode.next = new ListNode(); }
                newNode = newNode.next;
            }
            return ansNode;
        }
        //Розвернути вузли в k-групі
        static ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode cursor = head;
            for (int i = 0; i < k; i++)
            {
                if (cursor == null) { return head; }
                cursor = cursor.next;
            }
            ListNode curr = head;
            ListNode prev = null;
            ListNode next = null;
            for (int i = 0; i < k; i++)
            {
                next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            head.next = ReverseKGroup(curr, k);
            return prev;
        }
        //Розділити список
        static ListNode SplitList(ListNode head, int x)
        {
            if (head == null || head.next == null) { return head; }
            var curr = head;
            var list = new List<ListNode>();
            while (curr != null)
            {
                if (curr.val < x) { list.Add(curr); }
                curr = curr.next;
            }
            curr = head;
            while (curr != null)
            {
                if (curr.val >= x) { list.Add(curr); }
                curr = curr.next;
            }
            var n = list.Count;
            for (var i = 0; i < n - 1; i++)
            {
                list[i].next = list[i + 1];
            }
            list[n - 1].next = null;
            return list[0];
        }
    }
}


