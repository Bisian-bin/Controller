using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections.Concurrent;

namespace Forms
{
    public class MovData
    {
        const string CONN = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=note.mdb;Jet OLEDB:Database Password=1353060";//数据库配置参数
        public int passWord = 0; //权限密码 1是初级密码，2是中级密码，3是高级密码
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);//写入ini配置文件
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);//读取ini配置文件

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string lpAppName, StringBuilder lpReturnedString, int nSize, string lpFileName);



        /// <summary>
        /// 读取节点下所有的Key和Value
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public List<string> ReadSection(string path, string section)
        {
            //var iniContent = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
            bool find_Flag = false;//找到节点标志
            List<string> keyLists = new List<string>();
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader reader = new StreamReader(fileStream, Encoding.GetEncoding("gb2312")))
            {
                string currentSection = null;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#"))
                    {
                        continue; // Skip empty lines or comments
                    }
                    if (find_Flag && line.StartsWith("[") && line.EndsWith("]"))
                    {
                        break;
                    }
                    if (line.StartsWith("[") && line.EndsWith("]") && line.Contains(section))
                    {
                        currentSection = line.Substring(1, line.Length - 2).Trim();
                        find_Flag = true;
                        //iniContent.TryAdd(currentSection, new ConcurrentDictionary<string, string>());
                    }
                    else if (currentSection != null)
                    {
                        int separatorIndex = line.IndexOf('=');
                        if (separatorIndex > 0)
                        {

                            string key = line.Substring(0, separatorIndex).Trim();
                            string value = line.Substring(separatorIndex + 1).Trim();

                            //iniContent[currentSection].TryAdd(key, value);
                            keyLists.Add(key + "=");
                            keyLists.Add(value + ";");
                        }
                    }
                }
            }

            return keyLists;
        }

        /// <summary>
        /// 将整数转换二进制
        /// </summary>
        /// <param name="data">整数值</param>
        /// <param name="a">截取二进制位置</param>
        /// <returns></returns>
        public string convert_Int_to_2(int data, int a)
        {
            string cnt;//记录转换过后二进制值
            cnt = Convert.ToString(data, 2);
            if (cnt.Length < 32)
            {
                int c = cnt.Length;
                for (int b = 0; b < (32 - c); b++)
                {
                    cnt = "0" + cnt;
                }
            }
            return cnt.Substring(31 - a, 1);
        }

        /// <summary>
        /// 将数据写入ini配置文件里面
        /// </summary>
        /// <param name="section">段落名</param>
        /// <param name="key">键名</param>
        /// <param name="val">写入值</param>
        /// <param name="filename">ini文件完整路径</param>
        /// <returns></returns>
        public void writeIni(string section, string key, string val, string filename)
        {
            WritePrivateProfileString(section, key, val, filename);
        }

        /// <summary>
        /// 读取ini配置文件里面数据
        /// </summary>
        /// <param name="section">段落名</param>
        /// <param name="key">键名</param>
        /// <param name="def">写入值</param>
        /// <param name="filename">ini文件完整路径</param>
        /// <returns></returns>
        public string getIni(string section, string key, string def, string filename)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, filename);
            return sb.ToString();
        }

        /// <summary>
        /// 将文本内容写入至TcpData文档里面
        /// </summary>
        /// <param name="TcpData"></param>
        public void writeTxt(string Alarm)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = Application.StartupPath + "//Data//TcpData//" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(Alarm);
                }
            }
            catch
            {

            }
        }

        public string convert_16_to_2(string data)
        {
            string cnt = "";
            if (data == "0")
            {
                cnt = "0000";
            }
            else if (data == "1")
            {
                cnt = "0001";
            }
            else if (data == "2")
            {
                cnt = "0010";
            }
            else if (data == "3")
            {
                cnt = "0011";
            }
            else if (data == "4")
            {
                cnt = "0100";
            }
            else if (data == "5")
            {
                cnt = "0101";
            }
            else if (data == "6")
            {
                cnt = "0110";
            }
            else if (data == "7")
            {
                cnt = "0111";
            }
            else if (data == "8")
            {
                cnt = "1000";
            }
            else if (data == "9")
            {
                cnt = "1001";
            }
            else if (data == "A")
            {
                cnt = "1010";
            }
            else if (data == "B")
            {
                cnt = "1011";
            }
            else if (data == "C")
            {
                cnt = "1100";
            }
            else if (data == "D")
            {
                cnt = "1101";
            }
            else if (data == "E")
            {
                cnt = "1110";
            }
            else if (data == "F")
            {
                cnt = "1111";
            }
            return cnt;
        }


        /// <summary>
        /// 将文本内容写入至ProData文档里面
        /// </summary>
        /// <param name="ProData"></param>
        public void WritePro_SL(string Alarm)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = Application.StartupPath + "//Data//ProData//SL//" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_BIN(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(Alarm);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 结批时保存文本内容
        /// </summary>
        /// <param name="txt">批号</param>
        /// <param name="txt1">保存名称</param>
        /// <param name="txt2">保存数值</param>
        public void Write_Complete(string path, string txt)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(txt);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        public void File(string path)
        {
            if (!Directory.Exists(path))
            { Directory.CreateDirectory(path); }
        }



        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_HJ(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WriteOP(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WriteSM(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(Alarm);
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WriteHJ_1(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(Alarm);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WriteReal(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(Alarm);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_Temp(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_SL(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_Value(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_Alarm1(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_TEST(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_Alarm(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_FS(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_XL(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_TCP(string Alarm, string path)
        {
            try
            {
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至ProData文档里面
        /// </summary>
        /// <param name="ProData"></param>
        public void WritePro_XL(string Alarm)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = Application.StartupPath + "//Data//ProData//XL//" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 将文本内容写入至ProData文档里面
        /// </summary>
        /// <param name="ProData"></param>
        public void WritePro_FS(string Alarm)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = Application.StartupPath + "//Data//ProData//FS//" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }



        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void WritePro_TEST(string Alarm)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = Application.StartupPath + "//Data//ProData//TEST//" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    writer.WriteLine(now + " " + Alarm);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将文本内容写入至TXT1文档里面
        /// </summary>
        /// <param name="set"></param>
        public void writeTxt1(string set)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = Application.StartupPath + "//set//" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(set);
                }
                long size = 0;
                //获取文件大小
                using (FileStream file = System.IO.File.OpenRead(path))
                {
                    size = file.Length;//文件大小。byte
                }

                //判断日志文件大于2M，自动删除。
                if (size > (1024 * 4 * 512))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 改变DataGridView 表头颜色
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="backColor"></param>
        /// <param name="fontColor"></param>
        public void DataGridViewchangeColor(DataGridView dataGridView1, Color backColor, Color fontColor)//改变DataGridView 表头颜色
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = backColor;
            columnHeaderStyle.ForeColor = fontColor;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

        /// <summary>
        /// 自动适应列宽
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)//自动适应列宽
        {
            int width = 0;
            //对于DataGridView的每一个列都调整
            //将每一列都调整为自动适应模式
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {  //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }

        /// <summary>
        /// 初始化dataGridView
        /// </summary>
        /// <param name="dataGridView1"></param>
        public void Initialize_DataGridView(DataGridView dataGridView1)
        {
            dataGridView1.Rows.Add(60);//添加行数
            dataGridView1.RowHeadersVisible = false;//行头隐藏
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//头部自动换行
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//头部自动换行
            dataGridView1.RowHeadersVisible = false;
            for (int a = 0; a < 60; a++)//添加序号
            {
                dataGridView1.Rows[a].Cells[0].Value = a + 1;
                dataGridView1.Rows[a].Cells[5].Value = a + 61;
            }
            dataGridView1.ClearSelection(); //取消默認選中      
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//單元格顏色填充
            {
                dataGridView1.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        /// <summary>
        ///  load区初始化设置
        /// </summary>
        /// <param name="dataGridView1">LOAD</param>
        /// <param name="textBox1">X方向数量</param>
        /// <param name="textBox2">Y方向数量</param>
        public void Initialize_LOAD(DataGridView dataGridView1, NumericUpDown textBox1, NumericUpDown textBox2)
        {
            try
            {    //先控件X方向数量；Y方向数量
                dataGridView1.RowCount = Convert.ToInt16(textBox2.Text.Trim());
                dataGridView1.ColumnCount = Convert.ToInt16(textBox1.Text.Trim());
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)//X方向
                {
                    for (int b = 0; b < Convert.ToInt16(textBox2.Text.Trim()); b++)//Y方向
                    {
                        dataGridView1.Rows[b].Cells[a].Style.BackColor = Color.Green;
                        if (a == 0)
                        {
                            dataGridView1.Rows[b].Cells[a].Value = b + 1;
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        if (b == 0)
                        {
                            dataGridView1.Rows[b].Cells[a].Value = a + 1;
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                //设置控件高度
                for (int a = 0; a < Convert.ToInt16(textBox2.Text.Trim()); a++)
                {
                    if (Convert.ToInt16(textBox2.Text.Trim()) >= 15)
                    {
                        if (Convert.ToInt16(textBox2.Text.Trim()) == 15)
                        {
                            dataGridView1.Rows[a].Height = 34;//高度
                        }
                        else if (Convert.ToInt16(textBox2.Text.Trim()) == 16)
                        {
                            dataGridView1.Rows[a].Height = 32;//高度
                        }
                        else if (Convert.ToInt16(textBox2.Text.Trim()) == 20)
                        {
                            dataGridView1.Rows[a].Height = 25;//高度
                        }
                        else
                        {
                            dataGridView1.Rows[a].Height = 24;//高度
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[a].Height = 38;//高度
                    }
                }
                //设置控件宽度
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)
                {
                    if (Convert.ToInt16(textBox1.Text.Trim()) > 11)
                    {
                        dataGridView1.Columns[a].Width = 10;
                    }
                    else
                    {
                        if (Convert.ToInt16(textBox1.Text.Trim()) == 6)
                        {
                            dataGridView1.Columns[a].Width = 60;
                        }
                        else
                        {
                            dataGridView1.Columns[a].Width = 40;
                        }
                    }
                }
                //取消取消标记
                dataGridView1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOAD区初始化，异常信息如下：" + ex.Message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        ///BIN区初始化设置
        /// </summary>
        /// <param name="dataGridView1">LOAD</param>
        /// <param name="textBox1">X方向数量param>
        /// <param name="textBox2">Y方向数量</param>
        public void Initialize_BIN(DataGridView dataGridView1, NumericUpDown textBox1, NumericUpDown textBox2)
        {
            try
            {    //先控件X方向数量；Y方向数量
                dataGridView1.RowCount = Convert.ToInt16(textBox2.Text.Trim());
                dataGridView1.ColumnCount = Convert.ToInt16(textBox1.Text.Trim());
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)//X方向
                {
                    for (int b = 0; b < Convert.ToInt16(textBox2.Text.Trim()); b++)//Y方向
                    {
                        dataGridView1.Rows[b].Cells[a].Style.BackColor = Color.White;
                        dataGridView1.Rows[b].Cells[a].Value = "";
                        if (a == 0)
                        {
                            //dataGridView1.Rows[b].Cells[a].Value = b + 1;
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        if (b == 0)
                        {
                            //dataGridView1.Rows[b].Cells[a].Value = a + 1;
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                //设置控件高度
                for (int a = 0; a < Convert.ToInt16(textBox2.Text.Trim()); a++)
                {
                    if (Convert.ToInt16(textBox2.Text.Trim()) >= 15)
                    {
                        if (Convert.ToInt16(textBox2.Text.Trim()) == 15)
                        {
                            dataGridView1.Rows[a].Height = 34;//高度
                        }
                        else if (Convert.ToInt16(textBox2.Text.Trim()) == 16)
                        {
                            dataGridView1.Rows[a].Height = 32;//高度
                        }
                        else if (Convert.ToInt16(textBox2.Text.Trim()) == 20)
                        {
                            dataGridView1.Rows[a].Height = 25;//高度
                        }
                        else
                        {
                            dataGridView1.Rows[a].Height = 24;//高度
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[a].Height = 38;//高度
                    }
                }
                //设置控件宽度
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)
                {
                    if (Convert.ToInt16(textBox1.Text.Trim()) > 11)
                    {
                        dataGridView1.Columns[a].Width = 14;
                    }
                    else
                    {
                        if (Convert.ToInt16(textBox1.Text.Trim()) == 6)
                        {
                            dataGridView1.Columns[a].Width = 60;
                        }
                        else
                        {
                            dataGridView1.Columns[a].Width = 40;
                        }
                    }
                }
                //取消取消标记
                dataGridView1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOAD区初始化，异常信息如下：" + ex.Message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        ///BIN区初始化设置
        /// </summary>
        /// <param name="dataGridView1">LOAD</param>
        /// <param name="textBox1">X方向数量param>
        /// <param name="textBox2">Y方向数量</param>
        public void Initialize_BIN1(DataGridView dataGridView1, NumericUpDown textBox1, NumericUpDown textBox2)
        {
            try
            {    //先控件X方向数量；Y方向数量
                dataGridView1.RowCount = Convert.ToInt16(textBox2.Text.Trim());
                dataGridView1.ColumnCount = Convert.ToInt16(textBox1.Text.Trim());
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)//X方向
                {
                    for (int b = 0; b < Convert.ToInt16(textBox2.Text.Trim()); b++)//Y方向
                    {
                        dataGridView1.Rows[b].Cells[a].Style.BackColor = Color.White;
                        dataGridView1.Rows[b].Cells[a].Value = "";
                        if (a == 0)
                        {
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        if (b == 0)
                        {
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                //设置控件高度
                for (int a = 0; a < Convert.ToInt16(textBox2.Text.Trim()); a++)
                {
                    if (Convert.ToInt16(textBox2.Text.Trim()) >= 15)
                    {
                        if (Convert.ToInt16(textBox2.Text.Trim()) == 15)
                        {
                            dataGridView1.Rows[a].Height = 34;//高度
                        }
                        else if (Convert.ToInt16(textBox2.Text.Trim()) == 16)
                        {
                            dataGridView1.Rows[a].Height = 32;//高度
                        }
                        else if (Convert.ToInt16(textBox2.Text.Trim()) == 20)
                        {
                            dataGridView1.Rows[a].Height = 25;//高度
                        }
                        else
                        {
                            dataGridView1.Rows[a].Height = 24;//高度
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[a].Height = 38;//高度
                    }
                }
                //设置控件宽度
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)
                {
                    if (Convert.ToInt16(textBox1.Text.Trim()) > 11)
                    {
                        dataGridView1.Columns[a].Width = 14;
                    }
                    else
                    {
                        if (Convert.ToInt16(textBox1.Text.Trim()) == 6)
                        {
                            dataGridView1.Columns[a].Width = 90;
                        }
                        else
                        {
                            dataGridView1.Columns[a].Width = 60;
                        }
                    }
                }
                //取消取消标记
                dataGridView1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOAD区初始化，异常信息如下：" + ex.Message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        ///BIN区初始化设置
        /// </summary>
        /// <param name="dataGridView1">LOAD</param>
        /// <param name="textBox1">X方向数量param>
        /// <param name="textBox2">Y方向数量</param>
        public void Initialize_BIN(DataGridView dataGridView1, int BIN_X, int BIN_Y)
        {
            try
            {    //先控件X方向数量；Y方向数量
                dataGridView1.RowCount = Convert.ToInt16(BIN_Y);
                dataGridView1.ColumnCount = Convert.ToInt16(BIN_X);
                for (int a = 0; a < Convert.ToInt16(BIN_X); a++)//X方向
                {
                    for (int b = 0; b < Convert.ToInt16(BIN_Y); b++)//Y方向
                    {
                        dataGridView1.Rows[b].Cells[a].Style.BackColor = Color.White;
                        dataGridView1.Rows[b].Cells[a].Value = "";
                        if (a == 0)
                        {
                            //dataGridView1.Rows[b].Cells[a].Value = b + 1;
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        if (b == 0)
                        {
                            //dataGridView1.Rows[b].Cells[a].Value = a + 1;
                            dataGridView1.Columns[a].DefaultCellStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));/*New Font("宋体", 8.75)*/
                            dataGridView1.Columns[a].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                //设置控件高度
                for (int a = 0; a < Convert.ToInt16(BIN_Y); a++)
                {
                    if (Convert.ToInt16(BIN_Y) == 16)
                    {
                        dataGridView1.Rows[a].Height = 32;//高度
                    }
                    else if (Convert.ToInt16(BIN_Y) == 17)
                    {
                        dataGridView1.Rows[a].Height = 30;//高度
                    }
                    else
                    {
                        dataGridView1.Rows[a].Height = 32;//高度
                    }
                }
                //设置控件宽度
                for (int a = 0; a < Convert.ToInt16(BIN_X); a++)
                {
                    if (Convert.ToInt16(BIN_X) > 11)
                    {
                        dataGridView1.Columns[a].Width = 14;
                    }
                    else
                    {
                        dataGridView1.Columns[a].Width = 40;
                    }
                }
                //取消取消标记
                dataGridView1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOAD区初始化，异常信息如下：" + ex.Message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 初始化BIB板信息
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dataGridView2"></param>
        /// <param name="textBox1"></param>
        /// <param name="textBox2"></param>
        public void Initialize_BIB(DataGridView dataGridView1, DataGridView dataGridView2, TextBox textBox1, TextBox textBox2)
        {
            try
            {
                //先控件X方向数量；Y方向数量
                dataGridView1.RowCount = Convert.ToInt16(textBox2.Text.Trim()); dataGridView2.RowCount = Convert.ToInt16(textBox2.Text.Trim());
                dataGridView1.ColumnCount = Convert.ToInt16(textBox1.Text.Trim()); dataGridView2.ColumnCount = Convert.ToInt16(textBox1.Text.Trim());
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)
                {
                    for (int b = 0; b < Convert.ToInt16(textBox2.Text.Trim()); b++)
                    {
                        dataGridView1.Rows[b].Cells[a].Style.BackColor = Color.White;
                        dataGridView2.Rows[b].Cells[a].Style.BackColor = Color.White;
                    }
                }
                //设置高度
                for (int a = 0; a < Convert.ToInt16(textBox2.Text.Trim()); a++)
                {
                    if (dataGridView1.RowCount >= 5)

                    {
                        dataGridView1.Rows[a].Height = 28;
                        dataGridView2.Rows[a].Height = 28;
                    }
                    else
                    {
                        dataGridView1.Rows[a].Height = 50;
                        dataGridView2.Rows[a].Height = 50;
                    }
                }
                //设置宽度
                for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)
                {
                    if (dataGridView1.ColumnCount >= 8)
                    {
                        dataGridView1.Columns[a].Width = 25;
                        dataGridView2.Columns[a].Width = 25;
                    }
                    else
                    {
                        dataGridView1.Columns[a].Width = 60;
                        dataGridView2.Columns[a].Width = 60;
                    }
                }
                dataGridView1.CurrentCell = null;
                dataGridView2.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("BIB板初始化失败，异常信息如下：" + ex.Message);
            }
        }

        /// <summary>
        /// DataGridView点击行或者列颜色发生变化
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="textBox1"></param>
        /// <param name="textBox2"></param>
        public void SelectedMycount(DataGridView dataGridView1, NumericUpDown textBox1, NumericUpDown textBox2, Label lab)
        {
            int MyCount = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);//被选中单元格数
            if (MyCount > 0)
            {
                if (dataGridView1.AreAllCellsSelected(true))//判断是否选中多个
                {
                    for (int a = 0; a < Convert.ToInt16(textBox1.Text.Trim()); a++)
                    {
                        for (int b = 0; b < Convert.ToInt16(textBox2.Text.Trim()); b++)
                        {
                            if (dataGridView1.Rows[a].Cells[b].Style.BackColor != lab.BackColor)
                            {
                                dataGridView1.Rows[a].Cells[b].Style.BackColor = lab.BackColor;
                            }
                            else
                            {
                                dataGridView1.Rows[a].Cells[b].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < MyCount; i++)
                    {
                        if (dataGridView1.SelectedCells[i].Style.BackColor != lab.BackColor)
                        {
                            dataGridView1.SelectedCells[i].Style.BackColor = lab.BackColor;
                        }
                        else
                        {
                            dataGridView1.SelectedCells[i].Style.BackColor = Color.White;
                        }
                    }
                }
                dataGridView1.CurrentCell = null;
            }
        }

        /// <summary>
        ///  Load及BIN区数据初始化（XY方向）
        /// </summary>
        /// <param name="txt">X或者Y方向数量</param>
        /// <param name="txt1">缺角位置X/Y方向：</param>
        /// <param name="txt2">X/Y方向第一颗产品到边缘</param>
        /// <param name="txt3">X/Y方向产品间隔</param>
        /// <param name="txt4">X导程</param>
        /// <param name="Data">数据输出</param>
        public void InitializeData_X(NumericUpDown txt, NumericUpDown txt1, NumericUpDown txt2, NumericUpDown txt3, TextBox txt4, out string[] Data)
        {
            Data = new string[Convert.ToInt16(txt.Text.Trim())];
            for (int a = 0; a < Convert.ToInt16(txt.Text.Trim()); a++)
            {
                Data[a] = (Convert.ToInt32(txt1.Text.Trim()) + Convert.ToInt32(Convert.ToDouble(txt2.Text.Trim()) * Convert.ToDouble(txt4.Text.Trim())) + (a) * Convert.ToInt32(Convert.ToDouble(txt3.Text.Trim()) * Convert.ToDouble(txt4.Text.Trim()))).ToString();
            }
        }

        /// <summary>
        ///  Load及BIN区数据初始化（XY方向）
        /// </summary>
        /// <param name="txt">X或者Y方向数量</param>
        /// <param name="txt1">缺角位置X/Y方向：</param>
        /// <param name="txt2">X/Y方向第一颗产品到边缘</param>
        /// <param name="txt3">X/Y方向产品间隔</param>
        /// <param name="Data">数据输出</param>
        public void InitializeData_Y(NumericUpDown txt, NumericUpDown txt1, NumericUpDown txt2, NumericUpDown txt3, TextBox txt4, out string[] Data)
        {
            Data = new string[Convert.ToInt16(txt.Text.Trim())];
            for (int a = 0; a < Convert.ToInt16(txt.Text.Trim()); a++)
            {
                Data[a] = (Convert.ToInt32(txt1.Text.Trim()) - Convert.ToInt32(Convert.ToDouble(txt2.Text.Trim()) * Convert.ToDouble(txt4.Text.Trim())) - Convert.ToInt32((a + 1) * Convert.ToDouble(txt3.Text.Trim()) * Convert.ToDouble(txt4.Text.Trim()))).ToString();
            }
        }

        /// <summary>
        /// BIB板数据初始化（XY方向）
        /// </summary>
        /// <param name="txt">X方向数量</param>
        /// <param name="txt1">Y方向数量</param>
        /// <param name="txt2">X/Y起始位置</param>
        /// <param name="txt3">X/Y间隔</param>
        /// <param name="Data">输出数据</param>
        public void InitializeData_BIBX(TextBox txt, TextBox txt1, TextBox txt2, TextBox txt3, out string[] Data)
        {
            Data = new string[Convert.ToInt16(txt.Text.Trim()) * Convert.ToInt16(txt1.Text.Trim())];
            for (int a = 0; a < Convert.ToInt16(txt.Text.Trim()); a++)
            {
                for (int b = 0; b < Convert.ToInt16(txt1.Text.Trim()); b++)
                {
                    Data[1 + b + 8 * a] = (Convert.ToInt32(txt2.Text.Trim()) + Convert.ToInt32(a * Convert.ToDouble(txt3.Text.Trim()))).ToString();
                }
            }
        }

        /// <summary>
        /// BIB板数据初始化（XY方向）
        /// </summary>
        /// <param name="txt">X方向数量</param>
        /// <param name="txt1">Y方向数量</param>
        /// <param name="txt2">X/Y起始位置</param>
        /// <param name="txt3">X/Y间隔</param>
        /// <param name="Data">输出数据</param>
        public void InitializeData_BIBY(TextBox txt, TextBox txt1, TextBox txt2, TextBox txt3, out string[] Data)
        {
            Data = new string[Convert.ToInt16(txt.Text.Trim()) * Convert.ToInt16(txt1.Text.Trim())];
            for (int a = 0; a < Convert.ToInt16(txt.Text.Trim()); a++)
            {
                for (int b = 0; b < Convert.ToInt16(txt1.Text.Trim()); b++)
                {
                    Data[1 + b + 8 * a] = (Convert.ToInt32(txt2.Text.Trim()) - b * Convert.ToInt32(Convert.ToDouble(txt3.Text.Trim()))).ToString();
                }
            }
        }

        /// <summary>
        /// 设置列的背景色
        /// </summary>
        /// <param name="D1"></param>
        /// <param name="i"></param>
        private void setColorColum(DataGridView D1, int i)//•设置列的背景色
        {
            Color GridReadOnlyColor = Color.GreenYellow;
            D1.Columns[i].DefaultCellStyle.BackColor = GridReadOnlyColor;
        }

        /// <summary>
        /// 按照日期查询数据
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView(DataGridView dataGridView1, DateTimePicker dtBeginSelect, DateTimePicker dtOverSelect)
        {
            string dtBegin = dtBeginSelect.Value.AddDays(-0).ToString("yyyy-MM-dd");
            string dtOver = dtOverSelect.Value.AddDays(+0).ToString("yyyy-MM-dd");

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 日期,alarmTime as 开始时间,alarmMsg as 报警信息 from alarmMsg  where Aldate between #" + dtBegin + "# and #" + dtOver + "#";

                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                // manuDataTable(ds);
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                //自适应后,再指定个别列的宽度
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 180;
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.RowHeadersVisible = false;//行头隐藏

                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);

                setColorColum(dataGridView1, 0);
            }

            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查找数据库loadRecord
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView1(DataGridView dataGridView1, DateTimePicker dtBeginSelect, DateTimePicker dtOverSelect)
        {
            string dtBegin = dtBeginSelect.Value.AddDays(-0).ToString("yyyy-MM-dd");
            string dtOver = dtOverSelect.Value.AddDays(+0).ToString("yyyy-MM-dd");

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 日期,sv1 as 开始时间,sv2 as 结束时间,liaohao as 料号,pihao as 批号,sv3 as 总数,sv4 as 良品,sv5 as 良率,sv6 as bin1,sv7 as bin2,sv8 as bin3,sv9 as bin4,sv10 as 作业员  from loadRecord  where Aldate between #" + dtBegin + "# and #" + dtOver + "#";

                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                dataGridView1.RowHeadersVisible = false;//行头隐藏
                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);
                setColorColum(dataGridView1, 0);
            }

            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// List点击查找数据库loadRecord
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView(DataGridView dataGridView1, ListBox listbox)
        {
            string liao = listbox.Text;
            if (liao.Length < 1)
                return;
            //將料號名稱和制程序分開
            string name = listbox.Text.Trim();
            char[] delimiterChars_1 = { '-', '\t' };
            string[] msg = name.Split(delimiterChars_1);

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 日期,sv1 as 开始时间,sv2 as 结束时间,liaohao as 料号,pihao as 批号,sv3 as 总数,sv4 as 良品,sv5 as 良率,sv6 as bin1,sv7 as bin2,sv8 as bin3,sv9 as bin4,sv10 as 作业员  from loadRecord  where sv2 ='" + msg[0] + "'";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                dataGridView1.RowHeadersVisible = false;//行头隐藏
                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);
                setColorColum(dataGridView1, 0);
            }
            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// 查找数据库MAP1
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView2(DataGridView dataGridView1, DateTimePicker dtBeginSelect, DateTimePicker dtOverSelect)
        {
            string dtBegin = dtBeginSelect.Value.AddDays(-0).ToString("yyyy-MM-dd");
            string dtOver = dtOverSelect.Value.AddDays(+0).ToString("yyyy-MM-dd");

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 日期,sv1 as 开始时间,liaohao as 料号,pihao as 批号,sv2 as SET信息  from MAP1  where Aldate between #" + dtBegin + "# and #" + dtOver + "#";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                dataGridView1.RowHeadersVisible = false;//行头隐藏
                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);
                setColorColum(dataGridView1, 0);
            }

            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查找数据库MAP
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView3(DataGridView dataGridView1, DateTimePicker dtBeginSelect, DateTimePicker dtOverSelect)
        {
            string dtBegin = dtBeginSelect.Value.AddDays(-0).ToString("yyyy-MM-dd");
            string dtOver = dtOverSelect.Value.AddDays(+0).ToString("yyyy-MM-dd");

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 日期,sv1 as 开始时间,liaohao as 料号,pihao as 批号,sv2 as MAP信息  from MAP  where Aldate between #" + dtBegin + "# and #" + dtOver + "#";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                dataGridView1.RowHeadersVisible = false;//行头隐藏
                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);
                setColorColum(dataGridView1, 0);
            }

            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// List点击查找数据库loadRecord
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView4(DataGridView dataGridView1, string pihao)
        {

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 批次开始时间,sv1 as 获取MAP时间,liaohao as 料号,pihao as 批号,sv2 as MAP信息  from MAP  where liaohao ='" + pihao + "'";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                dataGridView1.RowHeadersVisible = false;//行头隐藏
                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);
                setColorColum(dataGridView1, 0);
            }

            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// List点击查找数据库loadRecord
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="dtBeginSelect"></param>
        /// <param name="dtOverSelect"></param>
        public void findAlarmMsg_to_DatagridView5(DataGridView dataGridView1, string pihao)
        {

            try
            {
                OleDbConnection conn = new OleDbConnection(CONN);
                string cmdText = "select Aldate as 日期,sv1 as Setting发送时间,liaohao as 料号,pihao as 批号,sv2 as SET信息  from MAP1  where liaohao ='" + pihao + "'";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                AutoSizeColumn(dataGridView1);
                dataGridView1.RowHeadersVisible = false;//行头隐藏
                DataGridViewchangeColor(dataGridView1, Color.White, Color.Blue);
                setColorColum(dataGridView1, 0);
            }

            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// //另存新档按钮   导出成Excel
        /// </summary>
        /// <param name="dgvAgeWeekSex"></param>
        public void SaveAs(DataGridView dgvAgeWeekSex)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";

            saveFileDialog.FilterIndex = 0;

            saveFileDialog.RestoreDirectory = true;

            saveFileDialog.CreatePrompt = true;

            saveFileDialog.Title = "Export Excel File To";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            Stream myStream;

            myStream = saveFileDialog.OpenFile();

            //StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));

            // StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            StreamWriter sw = new StreamWriter(myStream, System.Text.ASCIIEncoding.Unicode);//这样不会出现乱码

            string str = "";

            try
            {

                //写标题

                for (int i = 0; i < dgvAgeWeekSex.ColumnCount; i++)
                {

                    if (i > 0)
                    {

                        str += "\t";

                    }

                    str += dgvAgeWeekSex.Columns[i].HeaderText;

                }
                sw.WriteLine(str);
                //写内容

                for (int j = 0; j < dgvAgeWeekSex.Rows.Count; j++)
                {

                    string tempStr = "";

                    for (int k = 0; k < dgvAgeWeekSex.Columns.Count; k++)
                    {

                        if (k > 0)
                        {

                            tempStr += "\t";

                        }

                        tempStr += dgvAgeWeekSex.Rows[j].Cells[k].Value.ToString();

                    }



                    sw.WriteLine(tempStr);

                }

                MessageBox.Show("导出成功!", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                sw.Close();

                myStream.Close();

            }

            catch (Exception e)
            {

                MessageBox.Show(e.ToString(), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            }

            finally
            {

                sw.Close();

                myStream.Close();

            }

        }

        /// <summary>
        /// //将数据库中料号选出在ListBox 中显现
        /// </summary>
        /// <param name="listBox_DB"></param>
        public void loadData(ListBox listBox_DB)
        {
            using (OleDbConnection conn = new OleDbConnection(CONN))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from loadRecord order by Aldate desc";

                    OleDbDataReader reader = cmd.ExecuteReader();
                    listBox_DB.Items.Clear();

                    try
                    {
                        while (reader.Read())
                        {
                            listBox_DB.Items.Add(reader["Aldate"].ToString() + "-" + reader["pihao"].ToString());
                        }
                    }
                    catch
                    {
                        MessageBox.Show("沒有需求的信息!", "提示");
                    }
                }
            }
        }

        /// <summary>
        /// //将数据库中料号选出在ListBox 中显现
        /// </summary>
        /// <param name="listBox_DB"></param>
        public void loadData1(ListBox listBox_DB)
        {
            using (OleDbConnection conn = new OleDbConnection(CONN))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from loadRecord  order by Aldate desc";
                    OleDbDataReader reader = cmd.ExecuteReader();
                    listBox_DB.Items.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            listBox_DB.Items.Add(reader["sv7"].ToString() + "-" + reader["pihao"].ToString() + "-" + reader["sv1"].ToString());
                        }
                    }
                    catch
                    {
                        MessageBox.Show("沒有需求的信息!", "提示");
                    }
                }
            }
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        /// <param name="D1"></param>
        public void loadGrid(DataGridView D1, int sum)
        {
            //表格填充
            D1.Rows.Add(sum); //这样就添加了一行数据 ,行数
            D1.RowHeadersVisible = false;//行头隐藏
            D1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//头部自动换行
            D1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//头部自动换行
            D1.RowHeadersVisible = false;
            DataGridViewchangeColor(D1, Color.Blue, Color.White);//头部颜色变化
            if (sum != 40)
            {
                for (int a = 0; a < sum; a++)
                {
                    D1.Rows[a].Cells[0].Value = "Site" + (a + 1).ToString();
                }
            }
            else
            {
                for (int a = 0; a < sum; a++)
                {
                    D1.Rows[a].Cells[0].Value = (a + 1).ToString();
                }
            }
            D1.ClearSelection(); //取消默認選中      
            for (int i = 0; i < D1.Rows.Count; i++)//單元格顏色填充
            {
                D1.Rows[i].Cells[0].Style.BackColor = Color.LightSkyBlue;//天蓝色
                D1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            D1.CurrentCell = null;//取消选中
            D1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//头部居中
            if (sum == 40)//列出栏位名称
            {
                D1.Rows[0].Cells[1].Value = "MO";
                D1.Rows[1].Cells[1].Value = "LOT";
                D1.Rows[2].Cells[1].Value = "Test mode";
                D1.Rows[3].Cells[1].Value = "工号";
                D1.Rows[4].Cells[1].Value = "总数量";
                D1.Rows[5].Cells[1].Value = "良品数量";
                D1.Rows[6].Cells[1].Value = "良率";
                D1.Rows[7].Cells[1].Value = "投入数量";
                D1.Rows[8].Cells[1].Value = "开始时间";
                D1.Rows[9].Cells[1].Value = "结束时间";
                D1.Rows[10].Cells[1].Value = "工作时间";
                D1.Rows[11].Cells[1].Value = "档案名称";
                D1.Rows[12].Cells[1].Value = "UPH";
                D1.Rows[13].Cells[1].Value = "BINA名称";
                D1.Rows[14].Cells[1].Value = "BINB名称";
                D1.Rows[15].Cells[1].Value = "BINC名称";
                D1.Rows[16].Cells[1].Value = "BIND名称";
                D1.Rows[17].Cells[1].Value = "BINA数量";
                D1.Rows[18].Cells[1].Value = "BINB数量";
                D1.Rows[19].Cells[1].Value = "BINC数量";
                D1.Rows[20].Cells[1].Value = "BIND数量";
                D1.Rows[21].Cells[1].Value = "BINA Error Code";
                D1.Rows[22].Cells[1].Value = "BINB Error Code";
                D1.Rows[23].Cells[1].Value = "BINC Error Code";
                D1.Rows[24].Cells[1].Value = "BIND Error Code";
                D1.Rows[25].Cells[1].Value = "端口使用数量";
                D1.Rows[26].Cells[1].Value = "生产端口关闭数量";
                D1.Rows[27].Cells[1].Value = "报警次数";
                D1.Rows[28].Cells[1].Value = "加热类型";
                D1.Rows[29].Cells[1].Value = "加热设定值";
                D1.Rows[30].Cells[1].Value = "加热误差范围";
                D1.Rows[31].Cells[1].Value = "生产速度";
                D1.Rows[32].Cells[1].Value = "吸嘴模式";
                D1.Rows[33].Cells[1].Value = "BINA盘数";
                D1.Rows[34].Cells[1].Value = "BINB盘数";
                D1.Rows[35].Cells[1].Value = "BINC盘数";
                D1.Rows[36].Cells[1].Value = "BIND盘数";
            }
        }


        public void loadGrid3(DataGridView D1, int sum)
        {
            //表格填充
            D1.Rows.Add(sum); //这样就添加了一行数据 ,行数
            D1.RowHeadersVisible = false;//行头隐藏
            D1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//头部自动换行
            D1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//头部自动换行
            D1.RowHeadersVisible = false;
            DataGridViewchangeColor(D1, Color.Blue, Color.White);//头部颜色变化
            if (sum != 40)
            {
                for (int a = 0; a < sum; a++)
                {
                    D1.Rows[a].Cells[0].Value = "Site" + (a + 17).ToString();
                }
            }
            else
            {
                for (int a = 0; a < sum; a++)
                {
                    D1.Rows[a].Cells[0].Value = (a + 1).ToString();
                }
            }
            D1.ClearSelection(); //取消默認選中      
            for (int i = 0; i < D1.Rows.Count; i++)//單元格顏色填充
            {
                D1.Rows[i].Cells[0].Style.BackColor = Color.LightSkyBlue;//天蓝色
                D1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            D1.CurrentCell = null;//取消选中
            D1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//头部居中
        }

        /// <summary>
        /// 切割字符串
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string Space(int a, int b)
        {
            string data = "";
            for (int m = 0; m < (b - a); m++)
            {
                data = data + " ";
            }
            return data;
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        /// <param name="D1"></param>
        public void loadGrid1(DataGridView D1, int sum)
        {
            //表格填充
            D1.Rows.Add(sum); //这样就添加了一行数据 ,行数
            D1.RowHeadersVisible = false;//行头隐藏
            D1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//头部自动换行
            D1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//头部自动换行
            D1.RowHeadersVisible = false;
            DataGridViewchangeColor(D1, Color.Blue, Color.White);//头部颜色变化
            for (int a = 0; a < sum; a++)
            {
                D1.Rows[a].Cells[0].Value = "Site" + (a + 1).ToString();
                D1.Rows[a].Cells[6].Value = "Site" + (a + 17).ToString();
            }
            D1.ClearSelection(); //取消默認選中      
            for (int i = 0; i < D1.Rows.Count; i++)//單元格顏色填充
            {
                D1.Rows[i].Cells[0].Style.BackColor = Color.LightSkyBlue;//天蓝色
                D1.Rows[i].Cells[6].Style.BackColor = Color.LightSkyBlue;//天蓝色
                D1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            D1.CurrentCell = null;//取消选中
            D1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//头部居中              
        }


        /// 初始化DataGridView
        /// </summary>
        /// <param name="D1"></param>
        public void loadGrid2(DataGridView D1, int sum)
        {
            //表格填充
            D1.Rows.Add(sum); //这样就添加了一行数据 ,行数
            D1.RowHeadersVisible = false;//行头隐藏
            D1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//头部自动换行
            D1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;//头部自动换行
            D1.RowHeadersVisible = false;
            DataGridViewchangeColor(D1, Color.Blue, Color.White);//头部颜色变化
            for (int a = 0; a < sum; a++)
            {
                D1.Rows[a].Cells[0].Value = "Site" + (a + 1).ToString();
                D1.Rows[a].Cells[2].Value = "Site" + (a + 17).ToString();
            }
            D1.ClearSelection(); //取消默認選中      
            for (int i = 0; i < D1.Rows.Count; i++)//單元格顏色填充
            {
                D1.Rows[i].Cells[0].Style.BackColor = Color.LightSkyBlue;//天蓝色
                D1.Rows[i].Cells[2].Style.BackColor = Color.LightSkyBlue;//天蓝色
                D1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            D1.CurrentCell = null;//取消选中
            D1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//头部居中              
        }

        /// <summary>
        /// 端口良率显示
        /// </summary>
        /// <param name="D2"></param>
        public void Dut(DataGridView D2, DataGridView D4, DataGridView D10, DataGridView D100, DataGridView D110, DataGridView D120, DataGridView D130, out int[,] dut_ok, out int[,] dut_cnt)
        {
            dut_ok = new int[32, 8]; dut_cnt = new int[32, 8];
            try
            {
                using (OleDbConnection conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from DutLiabary where liaoHao = @liaohao and pihao=@m and mark=@m1";
                        cmd.Parameters.AddWithValue("liaohao", "ZJC0010A1367");//料號
                        cmd.Parameters.AddWithValue("m", "LCS21090948");//批号
                        cmd.Parameters.AddWithValue("m1", "87.50%");//良l
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string[] svValue = new string[36];
                                for (int a = 0; a < 32; a++)
                                {
                                    //前四个
                                    svValue[a] = reader["sv" + (a + 1).ToString()].ToString();
                                    string[] svValue1 = svValue[a].Split(',');
                                    for (int b = 0; b < 8; b++)
                                    {
                                        D2.Rows[a].Cells[8 - b].Value = svValue1[b];
                                        dut_ok[a, b] = Convert.ToInt32(svValue1[b]);
                                    }
                                    //后四个
                                    svValue[a] = reader["sv" + (a + 33).ToString()].ToString();
                                    string[] svValue2 = svValue[a].Split(',');
                                    for (int b = 0; b < 8; b++)
                                    {
                                        dut_cnt[a, b] = Convert.ToInt32(svValue2[b]);
                                        D2.Rows[a].Cells[8 - b].Value = (Convert.ToDouble(D2.Rows[a].Cells[8 - b].Value) / Convert.ToDouble(svValue2[b]) * 100).ToString("0.00") + "%";
                                        if (D2.Rows[a].Cells[8 - b].Value.ToString() == "非数字%")
                                        {
                                            D2.Rows[a].Cells[8 - b].Value = "0";
                                        }
                                    }
                                }
                                //site使用记录
                                string[] svValue3 = new string[4];
                                for (int a = 32; a < 36; a++)
                                {
                                    svValue[a] = reader["sv" + (a + 33).ToString()].ToString();
                                    svValue3 = svValue[a].Split(',');
                                    for (int b = 0; b < 8; b++)
                                    {
                                        D4.Rows[8 * (a - 32) + b].Cells[1].Value = Convert.ToInt32(svValue3[b]);
                                    }
                                }
                            }
                        }
                    }
                }
                //dut使用次数
                using (OleDbConnection conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from Dut where liaoHao = @liaohao and pihao=@m and mark=@m1";
                        cmd.Parameters.AddWithValue("liaohao", "ZJC0010A1367");//料號
                        cmd.Parameters.AddWithValue("m", "LCS21090948");//批号
                        cmd.Parameters.AddWithValue("m1", "87.50%");//良l
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string[] svValue = new string[36];
                                for (int a = 0; a < 32; a++)
                                {
                                    //前四个
                                    svValue[a] = reader["sv" + (a + 1).ToString()].ToString();
                                    string[] svValue1 = svValue[a].Split(',');
                                    for (int b = 0; b < 8; b++)
                                    {
                                        D10.Rows[a].Cells[8 - b].Value = svValue1[b];
                                    }
                                }
                            }
                        }
                    }
                }
                //将数值赋值到相应地方-次数监控
                for (int a = 0; a < 32; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        if (a < 16)
                        {
                            D100.Rows[a].Cells[8 - b].Value = D10.Rows[a].Cells[8 - b].Value;
                        }
                        else
                        {
                            D110.Rows[a - 16].Cells[8 - b].Value = D10.Rows[a].Cells[8 - b].Value;
                        }
                    }
                }
                //将数值赋值到相应地方-端口良率监控
                for (int a = 0; a < 32; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        if (a < 16)
                        {
                            D120.Rows[a].Cells[8 - b].Value = D2.Rows[a].Cells[8 - b].Value;
                        }
                        else
                        {
                            D130.Rows[a - 16].Cells[8 - b].Value = D2.Rows[a].Cells[8 - b].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取料號庫異常->" + ex.Message);
            }
        }
    }
}

