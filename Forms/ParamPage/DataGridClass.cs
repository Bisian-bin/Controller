using AntdUI;
using Newtonsoft.Json;
using QMBaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Forms
{
    class DataGridClass
    {
        #region 点位管理

        #region 初始化控件

        /// <summary>
        /// 禁止更改宽高 
        /// </summary>
        /// <param name="dataGridView1"></param>
        public void NotChangeListRow(DataGridView dataGridView1)
        {
            // 禁止用户改变DataGridView的所有列的列宽   
            dataGridView1.AllowUserToResizeColumns = false;
            //禁止用户改变DataGridView所有行的行高   
            dataGridView1.AllowUserToResizeRows = false;
            // 禁止用户改变列头的高度   
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 禁止用户改变列头的宽度   
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="dataGridView"></param>
        public void DataGridViewInit(DataGridView dataGridView)
        {
            //添加列
            dataGridView.Columns.Add((1).ToString(), "序列号");
            dataGridView.Columns.Add((2).ToString(), "点位名称");
            dataGridView.Columns.Add((3).ToString(), "当前坐标");
            dataGridView.Columns.Add((4).ToString(), "移动到点位");
            dataGridView.Columns.Add((5).ToString(), "点位保存");

            //添加行
            for (int i = 0; i < frmDebug.AxisRowCount; i++)
            {
                dataGridView.Rows.Add();
            }
            //修改高度
            for (int i = 0; i < frmDebug.AxisRowCount; i++)
            {
                dataGridView.Rows[i].Height = 29;
            }
            //修改宽度
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    dataGridView.Columns[i].Width = 50;
                }
                else if (i == 1)
                {
                    dataGridView.Columns[i].Width = 220;
                }
                else if (i == 2)
                {
                    dataGridView.Columns[i].Width = 90;
                }
                else if (i == 3)
                {
                    dataGridView.Columns[i].Width = 87;
                }
                else if (i == 4)
                {
                    dataGridView.Columns[i].Width = 87;
                }
            }
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题居中
            dataGridView.AllowUserToAddRows = false;//取消第一行
            dataGridView.RowHeadersVisible = false;//取消第一列
            dataGridView.ClearSelection(); //取消默认选中  
        }



        #endregion

        #region 添加参数控件

        public System.Windows.Forms.Label[] labAxis = new System.Windows.Forms.Label[frmDebug.AxisRowCount];
        public System.Windows.Forms.Label[] labname = new System.Windows.Forms.Label[frmDebug.AxisRowCount];
        AntdUI.Button[] btn1 = new AntdUI.Button[frmDebug.AxisRowCount];
        AntdUI.Button[] btn2 = new AntdUI.Button[frmDebug.AxisRowCount];


        public void AddControl_Parameter(DataGridView dataGridView, int num, int cycle)
        {
            //轴号
            for (int i = 0; i < cycle; i++)
            {
                labAxis[i] = new System.Windows.Forms.Label();
                labAxis[i].Text = (i + 1 + num).ToString().PadLeft(2, '0');
                labAxis[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                labAxis[i].AutoSize = false;
                labAxis[i].TextAlign = ContentAlignment.MiddleCenter;
                labAxis[i].BackColor = Color.LightSteelBlue;
                dataGridView.Controls.Add(labAxis[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(0, i, false);
                labAxis[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                labAxis[i].Location = new Point(rect.Left + 3, rect.Top + 3);
            }
            //轴名称          
            for (int i = 0; i < cycle; i++)
            {
                labname[i] = new System.Windows.Forms.Label();
                labname[i].Name = "AxisNum" + (i + 1 + num).ToString().PadLeft(2, '0');
                labname[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                labname[i].AutoSize = false;
                labname[i].TextAlign = ContentAlignment.MiddleCenter;
                labname[i].BackColor = Color.LightSteelBlue;
                dataGridView.Controls.Add(labname[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(1, i, false);
                labname[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                labname[i].Location = new Point(rect.Left + 3, rect.Top + 3);
            }
            //移动  
            for (int i = 0; i < cycle; i++)
            {
                Rectangle rect = dataGridView.GetCellDisplayRectangle(3, i, false);
                btn1[i] = new AntdUI.Button();
                btn1[i].Name = "Move" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn1[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                btn1[i].Text = "移动";
                btn1[i].AutoSize = false;
                btn1[i].TextAlign = ContentAlignment.MiddleCenter;
                btn1[i].DefaultBack = Color.FromArgb(187, 211, 239);
                btn1[i].Shape = TShape.Round;
                dataGridView.Controls.Add(btn1[i]);
                btn1[i].Size = new Size(rect.Width, rect.Height);
                btn1[i].Location = new Point(rect.Left + 1, rect.Top);
                btn1[i].Shape = TShape.Round;
                btn1[i].Tag = i;
                btn1[i].Click += new EventHandler(MoveBtn_Click);
            }
            //保存          
            for (int i = 0; i < cycle; i++)
            {
                btn2[i] = new AntdUI.Button();
                btn2[i].Name = "Save" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn2[i].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                btn2[i].Text = "保存";
                btn2[i].AutoSize = false;
                btn2[i].TextAlign = ContentAlignment.MiddleCenter;
                btn2[i].DefaultBack = Color.FromArgb(187, 211, 239);
                btn2[i].Shape = TShape.Round;
                dataGridView.Controls.Add(btn2[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(4, i, false);
                btn2[i].Size = new Size(rect.Width, rect.Height);
                btn2[i].Location = new Point(rect.Left + 1, rect.Top);
                btn2[i].Shape = TShape.Round;
                btn2[i].Tag = i;
                btn2[i].Click += new EventHandler(SaveBtn_Click);
            }
        }

        #endregion

        #region 控件方法
        /// <summary>
        /// 轴运动到对应位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveBtn_Click(object sender, EventArgs e)
        {
            AntdUI.Button btn = sender as AntdUI.Button;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);
            short DifName = (short)Convert.ToUInt16(name);
            string PosName = labname[DifName - 1].Text;
            double Doublepos = 0.00;
            FlowFactory.AxisContorl.Axis.PosObjData.TryGetValue(frmDebug.Current.AxisNo, out var pos);
            for (int i = 0; i < pos.PosDif.Count; i++)
            {
                if (labname[DifName - 1].Text == pos.PosDif[i].PonitLocation)
                {
                    Doublepos = pos.PosDif[i].Pos;
                }
            }
            FlowFactory.AxisContorl.MoveAxis(frmDebug.Current.AxisNo, Doublepos, 1);
        }

        /// <summary>
        /// 保存JSON文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {         
            try
            {
                if (frmDebug.AxisPosite == "" || frmDebug.AxisPosite == null) { MessageBox.Show("当前坐标数值不能为空!"); return; }
                AntdUI.Button btn = sender as AntdUI.Button;
                string name = btn.Name.Substring(4, btn.Name.Length - 4);
                short DifName = (short)Convert.ToUInt16(name);
                FlowFactory.AxisContorl.Axis.PosObjData.TryGetValue(frmDebug.Current.AxisNo, out var pos);
                for (int i = 0; i < pos.PosDif.Count; i++)
                {
                    if (labname[DifName - 1].Text == pos.PosDif[i].PonitLocation)
                    {
                        pos.PosDif[i].Pos = Convert.ToDouble((frmDebug.AxisPosite));
                    }
                }
                string json = JsonConvert.SerializeObject(FlowFactory.AxisContorl.Axis.PosList, Formatting.Indented);
                File.WriteAllText(FlowFactory.AxisContorl.Axis.JsonFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存JSON文件失败: " + ex.Message);
            }
        }

        /// <summary>
        /// Labname名称发生变化
        /// </summary>
        public void ChangeLabname(QMPosExcelModel name, DataGridView dataGridView)
        {
            FlowFactory.AxisContorl.Axis.PosObjData.TryGetValue(name.AxisNo, out var pos);
            for (int a = 0; a < frmDebug.AxisRowCount; a++)
            {
                PropertyInfo propertyInfo = name.GetType().GetProperty($"Pos{a + 1}");
                labname[a].Text = propertyInfo.GetValue(name)?.ToString();
                if (labname[a].Text == "")
                {
                    btn1[a].Visible = false; btn2[a].Visible = false;
                    dataGridView.Rows[a].Cells[2].Value = "";
                }
                else
                {
                    dataGridView.Rows[a].Cells[2].Value = "";
                    btn1[a].Visible = true; btn2[a].Visible = true;
                    for (int i = 0; i < pos.PosDif.Count; i++)
                    {
                        if (labname[a].Text == pos.PosDif[i].PonitLocation)
                        {
                            dataGridView.Rows[a].Cells[2].Value = pos.PosDif[i].Pos;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 显示轴图片
        /// </summary>
        /// <param name="pictureBox">图片</param>
        /// <param name="AxisNo">轴号</param>
        public void PicImage(PictureBox pictureBox, int AxisNo)
        {
            pictureBox.Image = Image.FromFile(Application.StartupPath + "\\ico\\" + "轴" + (AxisNo + 1).ToString() + ".png");
        }


        #endregion


        #endregion
    }
}
