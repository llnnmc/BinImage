using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// 增加对binImg类库的引用
using binImg;

namespace binTest
{
    class Program
    {
        public static void ShowTip()
        {
            Console.WriteLine("\r\n图像二值化测试程序V1.0，上海因致信息科技有限公司，2018/06。");
            Console.WriteLine("\r\n命令行格式：");
            Console.WriteLine("binTest <输入图像文件名> <输出图像文件名> <-[g][b][r][t] [x] [y] [width] [height]>");
            Console.WriteLine("-g：灰度化处理");
            Console.WriteLine("-b：二值化处理");
            Console.WriteLine("-r：反转处理");
            Console.WriteLine("-t：裁切处理");
            Console.WriteLine("x，y，width，height：裁切起始位置坐标和宽高度");
        }

        static void Main(string[] args)
        {
            // 命令行检查和提示
            if (args.Length != 3 && args.Length != 7)
            {
                ShowTip();
                return;
            }

            if (args[2] == "-gbrt" && args.Length != 7)
            {
                ShowTip();
                return;
            }

            try
            {
                // 构造二值化图像对象
                BinImg bi = new BinImg(args[0]);
                switch (args[2])
                {
                    case "-g":
                        // 灰度化
                        bi.ToGray();
                        break;
                    case "-gb":
                        // 灰度化
                        bi.ToGray();
                        // 二值化
                        bi.Binaryzation();
                        break;
                    case "-gbr":
                        // 灰度化
                        bi.ToGray();
                        // 二值化
                        bi.Binaryzation();
                        // 反转
                        bi.Reverse();
                        break;
                    case "-gbrt":
                        // 灰度化
                        bi.ToGray();
                        // 二值化
                        bi.Binaryzation();
                        // 反转
                        bi.Reverse();
                        // 裁切
                        Rectangle rect = new Rectangle(Int32.Parse(args[3]), Int32.Parse(args[4]), Int32.Parse(args[5]), Int32.Parse(args[6]));
                        bi.Tailor(rect);
                        break;
                    case "-b":
                        // 二值化
                        bi.Binaryzation();
                        break;
                    case "-br":
                        // 二值化
                        bi.Binaryzation();
                        // 反转
                        bi.Reverse();
                        break;
                    case "-brt":
                        // 二值化
                        bi.Binaryzation();
                        // 反转
                        bi.Reverse();
                        // 裁切
                        rect = new Rectangle(Int32.Parse(args[3]), Int32.Parse(args[4]), Int32.Parse(args[5]), Int32.Parse(args[6]));
                        bi.Tailor(rect);
                        break;
                    case "-r":
                        // 反转
                        bi.Reverse();
                        break;
                    case "-rt":
                        // 反转
                        bi.Reverse();
                        // 裁切
                        rect = new Rectangle(Int32.Parse(args[3]), Int32.Parse(args[4]), Int32.Parse(args[5]), Int32.Parse(args[6]));
                        bi.Tailor(rect);
                        break;
                    case "-t":
                        // 裁切
                        rect = new Rectangle(Int32.Parse(args[3]), Int32.Parse(args[4]), Int32.Parse(args[5]), Int32.Parse(args[6]));
                        bi.Tailor(rect);
                        break;
                    default:
                        ShowTip();
                        return;
                }

                //写出位图文件
                bi.SaveImg(args[1]);

                Console.WriteLine("\r\n图像二值化测试程序V1.0，上海因致信息科技有限公司，2018/06。");
                Console.WriteLine("\r\nCompleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
