using Microsoft.AspNetCore.Mvc;

namespace MSIA.WebFresher032023.Demo
{
    public class Calculator
    {
        /// <summary>
        /// Hàm cộng 2 số nguyên
        /// </summary>
        /// <param name="a">Số nguyên a</param>
        /// <param name="b">Số nguyên b</param>
        /// <returns></returns>
        /// Created by: LDTUAN (06/05/2023)
        public long Add(int a, int b)
        {
            return a + (long)b;
        }
        /// <summary>
        /// Hàm trừ 2 số nguyên
        /// </summary>
        /// <param name="a">Số nguyên a</param>
        /// <param name="b">Số nguyên b</param>
        /// <returns></returns>
        /// Created by: LDTUAN (06/05/2023)
        public long Sub(int a, int b)
        {
            return a - (long)b;
        }
        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="a">Số nguyên a</param>
        /// <param name="b">Số nguyên b</param>
        /// <returns></returns>
        public long Mul(int a, int b)
        {
            return (long)a * b;
        }
        /// <summary>
        /// Hàm chia 2 số nguyên
        /// </summary>
        /// <param name="a">Số nguyên a</param>
        /// <param name="b">Số nguyên b</param>
        /// <returns></returns>
        public double Div(int a, int b)
        {
            if(b == 0)
                throw new Exception("Không chia được cho 0");
            return (double)a / b;
        }
    }
}
