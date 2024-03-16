using System.IO;

namespace demo2
{
    /// <summary>
    /// 时间复杂度是 O(N)。
    /// 空间复杂度是 O(N)。
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 4, 2,2,3,1,4,7,8,6,9};
            int result = solution(ints);
            Console.WriteLine(result);
            Console.ReadLine();
        }
        public static int solution(int[] A)
        {
            int n = A.Length;
            if (n == 0)
            {
                return -1;
            }
            int maxLeft = A[0];
            int[] maxRight = new int[n];

            // 计算每个位置右侧的最大值
            maxRight[n - 1] = A[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                maxRight[i] = Math.Max(A[i], maxRight[i + 1]);
            }
            // 找到满足条件的极值点
            for (int i = 1; i < n - 1; i++)
            {
                if (A[i] >= maxLeft && A[i] <= maxRight[i + 1])
                {
                    return i;
                }
                maxLeft = Math.Max(maxLeft, A[i]);
            }
            // 检查第一个和最后一个元素
            if (A[0] >= maxRight[1])
            {
                return 0;
            }
            if (A[n - 1] >= maxLeft)
            {
                return n - 1;
            }
            return -1; // 没有找到极值点
        }
    }
}
